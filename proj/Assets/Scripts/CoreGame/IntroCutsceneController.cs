using UnityEngine;
using System;
using System.Collections;

public class IntroCutsceneController : MonoBehaviour {
    [HideInInspector]
    public event Action IntroDone;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void animationDone(float time)
    {
        // End of the intro cutscene giving control to player
        IntroDone();
    }
}
