using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ws_to_debug : MonoBehaviour
{
    [SerializeField] private TMP_Text debug;
    [SerializeField] private bool testDebug = false;
    [SerializeField] private float debugMessageTime = 5.0f;
    private List<string> messageList = new();
    private float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        debug = GameObject.FindGameObjectWithTag("debug").GetComponentInChildren<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testDebug)
        {
            timer += Time.deltaTime;
            // after x time we remove the debug message on the screen
            if (timer >= debugMessageTime)
            {
                timer = 0.0f;
                messageList.RemoveAt(0);
                if (messageList.Count == 0)
                {
                    testDebug = false;
                    debug.SetText("");
                }
                else
                {
                    debug.SetText(String.Join("\n", messageList));
                }
            }
        }
    }

    public void SetDebug(string _message)
    {
        Debug.Log("printing " + _message);
        if (_message is null)
        {
            throw new ArgumentNullException(nameof(_message));
        }
        if (testDebug == false)
        {
            testDebug = true;

            messageList.Add(_message);
            debug.SetText(String.Join("\n", messageList));
        }

    }

    public void PrintDebug()
    {
        Debug.LogWarning("made it here");
    }
}
