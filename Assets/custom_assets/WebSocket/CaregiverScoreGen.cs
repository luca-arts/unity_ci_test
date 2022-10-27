using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaregiverScoreGen : MonoBehaviour
{
    public int careGiverScore = 3;
    public string _time;
    [SerializeField] private WsClient wsClient;
    // Start is called before the first frame update
    void Start()
    {
        wsClient = FindObjectOfType<WsClient>();
    }

    public void SendCaregiverScoreOverws()
    {
        wsClient.ws.Send(this.GetScore());
    }

    public void SetScore(int score)
    {
        careGiverScore = score;
    }

    public string GetScore()
    {
        _time = System.DateTime.Now.ToString("f");
        return JsonUtility.ToJson(this);
    }

}
