using UnityEngine;
using System.Collections;

public class StageRotator : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Debug.Log("Stage Rotator script started.");
    }
    
    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        string asdf = string.Format("x: {0}; \ny: {1}", x, y);

        Debug.Log(asdf);
    }
}
