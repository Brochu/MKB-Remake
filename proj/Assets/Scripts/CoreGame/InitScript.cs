using UnityEngine;
using System.Collections;

public class InitScript : MonoBehaviour {

    // Use this for initialization
    void Start () 
    {
        GameManager.onInitDone += onInitDone;    
    }

    private void onInitDone()
    {
        Application.LoadLevel("InGameUI");
    }
}
