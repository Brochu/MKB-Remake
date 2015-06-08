using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool invertHorizontal = false;
    public bool useKeyboardControls = false;
    public bool ignoreInputs = false;

    // Use this for initialization
    void Start () {
        GameManager.onTimeOver += onTimeOver;
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnDestroy()
    {
        GameManager.onTimeOver -= onTimeOver;
    }

    public float getSpinInput()
    {
        if (ignoreInputs) return 0;

        return (invertHorizontal) ? Input.GetAxis("Horizontal2") : -Input.GetAxis("Horizontal2");
    }

    public float getVerticalInput()
    {
        if (ignoreInputs) return 0;

        if (useKeyboardControls)
        {
            return Input.GetAxis("Vertical");
        }
        else
        {
            float moveVal = Input.GetAxis("Vertical1");
            return (Mathf.Approximately(moveVal, 0)) ? 0 : moveVal;
        }
    }

    public float getHorizontalInput()
    {
        if (ignoreInputs) return 0;

        if (useKeyboardControls)
        {
            return Input.GetAxis("Horizontal");
        }
        else
        {
            float moveVal = Input.GetAxis("Horizontal1");
            return (Mathf.Approximately(moveVal, 0)) ? 0 : moveVal;
        }
    }

    private void onTimeOver()
    {
        this.ignoreInputs = true;
    }
}
