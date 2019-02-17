function myFunction(param,func){
    func(param);
}

myFunction("Say",function(param){
    console.log("do something :",param);
});