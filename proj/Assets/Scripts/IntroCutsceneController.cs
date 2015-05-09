using UnityEngine;
using System.Collections;

public class IntroCutsceneController : MonoBehaviour {
    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;

    PlayerController player = null;

    // Use this for initialization
    void Start () {
        // Get player controller object
        if (playerObj == null)
        {
            Debug.Log("No player object given ... disabling CutsceneController");
            this.enabled = false;
            return;
        }
        player = playerObj.GetComponent<PlayerController>();

        if (player == null)
        {
            Debug.Log("No player controller object found ... disabling CutsceneController");
            this.enabled = false;
            return;
        }

        Debug.Log("Start of the cutscene intro");

        // Switch to the cutscene camera and disable inputs
        introCam.enabled = true;
        playerCam.enabled = false;
        player.ignoreInputs = true;
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
