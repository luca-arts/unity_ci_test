using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class EOGNotificationGen : MonoBehaviour
{
    [SerializeField] private WsClient wsClient;
    public string EyeNotification = "looked at caregiver";
    public string _time;

    // Start is called before the first frame update
    void Start()
    {
        wsClient = FindObjectOfType<WsClient>();
    }

    public void SendEOGViaWs()
    {
        wsClient.ws.Send(this.GetEOGNotification());
    }

    public void setEOGNotification(string notification)
    {
        EyeNotification = notification;
    }
    public string GetEOGNotification()
    {
        _time = System.DateTime.Now.ToString("f");
        return JsonUtility.ToJson(this);
    }
}
