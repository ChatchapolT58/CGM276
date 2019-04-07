using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Game1 : MonoBehaviour
{

    static SocketIOComponent socket;

    public GameObject playerPreFab;
    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);

        socket.On("spawn", OnSpawned);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnConnected(SocketIOEvent e)
    {
        print("connected");

        socket.Emit("move");
    }

    void OnSpawned(SocketIOEvent e)
    {
        print("spawned");

        Instantiate(playerPreFab);

        //socket.Emit("spawn");
    }
}
