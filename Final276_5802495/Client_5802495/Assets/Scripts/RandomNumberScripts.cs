using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class RandomNumberScripts : MonoBehaviour
{
    [SerializeField] Button btnsend = null;
    [SerializeField] InputField numberInput = null;
    [SerializeField] Text answerText = null;

    SocketIOComponent socketIOComponent = null;


    void Awake()
    {
        socketIOComponent = GetComponent<SocketIOComponent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        socketIOComponent.On("return result", OnReturnResult);
        socketIOComponent.On("new lottery create", OnNewLotteryCreated);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendSuggestion()
    {
            int suggestNumber = 0;
            try
            {
                suggestNumber = int.Parse(numberInput.text);
            }
            catch (System.InvalidCastException)
            {
                suggestNumber = 0;
            }
            string data = JsonUtility.ToJson(new PlayerJSON(PlayerData.name, suggestNumber));
            socketIOComponent.Emit("request suggest", new JSONObject(data));
    }

    void OnReturnResult(SocketIOEvent socketIOEvent)
    {
        var result = CheckJSON.CreateFromJSON(socketIOEvent.data.ToString());

        switch (result.status)
        {
            case -1:
                answerText.text = "Too Low";
                answerText.color = Color.red;
                break;
            case 0:
                if (result.name == PlayerData.name)
                {
                    answerText.text = "You win the lottery";
                    answerText.color = Color.green;
                }
                else
                {
                    answerText.text = $"{result.name} win the lottery";
                    answerText.color = Color.yellow;
                }
                break;
            case 1:
                answerText.text = "Too High";
                answerText.color = Color.red;
                break;
        }
    }
    void OnNewLotteryCreated(SocketIOEvent socketIOEvent)
    {
        btnsend.GetComponentInChildren<Text>().text = "New Game";
    }

    public class PlayerJSON
    {
        public string name;
        public int number;

        public PlayerJSON(string name, int number)
        {
            this.name = name;
            this.number = number;
        }
        public static PlayerJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<PlayerJSON>(data);
        }
    }
    public class CheckJSON
    {
        public string name;
        public int status;

        public static CheckJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<CheckJSON>(data);
        }
    }

    public static class PlayerData
    {
        public static string name;
    }

}
