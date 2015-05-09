using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool invertHorizontal = false;
    public bool useKeyboardControls = false;
    public bool ignoreInputs = false;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public float getSpinInput()
    {
        if (ignoreInputs) return 0;

        return (invertHorizontal) ? Input.GetAxis("Horizontal2") : -Input.GetAxis("Horizontal2");
    }

    public float getVerticalInput()
    {
        //TODO: Add validation with float.approximately to remove value explosion
        if (ignoreInputs) return 0;

        if (useKeyboardControls)
            return Input.GetAxis("Vertical");
        else
            return Input.GetAxis("Vertical1");
    }

    public float getHorizontalInput()
    {
        //TODO: Add validation with float.approximately to remove value explosion
        if (ignoreInputs) return 0;

        if (useKeyboardControls)
            return Input.GetAxis("Horizontal");
        else
            return Input.GetAxis("Horizontal1");
    }
}
