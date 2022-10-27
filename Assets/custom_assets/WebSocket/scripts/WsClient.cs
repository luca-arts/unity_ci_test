using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;


public class WsClient : MonoBehaviour
{
    public WebSocket ws;
    private bool hasWsConnection = false;
    private float timeoutLength = 2.0f;
    private float timeoutTimer = 0.0f;
    [SerializeField] private string ip = "localhost";
    [SerializeField] private string port = "8080";
    [SerializeField] private Ws_to_debug wsToDebug;
    [System.Serializable] public class WsEvent : UnityEvent<string> { }
    public WsEvent wsMsgReceived;
    public class WSHelloworld
    {
        public string connectMessage;
        public string _time;
        public WSHelloworld(string connectmsg)
        {
            connectMessage = connectmsg;
            _time = System.DateTime.Now.ToString("f");
        }
        public string HelloMessage()
        {
            return JsonUtility.ToJson(this);
        }
    }
    [SerializeField] private WSHelloworld wsHello;
    private void Start()
    {
        wsHello = new WSHelloworld("Hello from " + ip);
        wsToDebug = this.GetComponent<Ws_to_debug>();
        ws = new WebSocket("ws://" + ip + ":" + port);
        ws.ConnectAsync();

        ws.OnOpen += (sender, e) =>
        {
            hasWsConnection = true;
            var wsmsg = wsHello.HelloMessage();
            SendWSMessage(wsmsg);
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
            HandleIncomingMessage(e.Data);
        };
        ws.OnClose += (sender, e) =>
        {
            //Debug.Log("the connection did close");
            hasWsConnection = false;
        };
    }

    private void Update()
    {
        // we check for a websocket connection and otherwise try to reconnect every <timeoutLength> seconds.
        if (ws == null)
        {
            return;
        }
        if (hasWsConnection == false)
        {
            timeoutTimer += Time.deltaTime;
            if (timeoutTimer > timeoutLength)
            {
                ws.ConnectAsync();
                timeoutTimer = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ws.Send("test");
            }
        }
    }

    private void HandleIncomingMessage(string message)
    {
        Debug.Log("INVOKING "+message);
        wsToDebug.SetDebug(message);
        //wsMsgReceived.Invoke(message);
    }


    public void SendWSMessage(string message)
    {
        Debug.Log("sending " + message);
        if (hasWsConnection)
        {
            ws.Send(message);
        }
    }
    public void SetDebug(string message)
    {
        Debug.Log("printing " + message);
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }

    }

    private void OnApplicationQuit()
    {
        SendWSMessage("Android application is closing");
        ws.CloseAsync(CloseStatusCode.Normal);
    }


}

