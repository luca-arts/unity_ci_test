using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class DebugExample : MonoBehaviour
    {
        private TMP_Text debug;
        [SerializeField] bool testDebug = false;

        // Use this for initialization
        void Start()
        {
            debug = GameObject.FindGameObjectWithTag("debug").GetComponentInChildren<TMPro.TMP_Text>();

        }

        // Update is called once per frame
        void Update()
        {
            if(testDebug) debug.SetText("Debugger present");
        }
    }
}