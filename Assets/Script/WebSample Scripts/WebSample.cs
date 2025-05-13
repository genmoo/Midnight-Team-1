using UnityEngine;

public class WebSample : MonoBehaviour
{
    public void SendHelloWorld()
    {
        Debug.Log("SendHelloWorld");
        StartCoroutine(Api_HelloWorld.Send());
    }
    
    public void SendGetJson()
    {
        Debug.Log("SendGetJson");
        StartCoroutine(Api_GetJson.Send());
    }

    public void SendGetEchoText()
    {
        Debug.Log("SendGetEchoText");
        StartCoroutine(Api_GetEchoText.Send("안녕"));
    }
}
