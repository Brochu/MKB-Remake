using UnityEngine;
using System.Collections;

public class IntroCutsceneController : MonoBehaviour {
    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;

    PlayerController player = null;
    Rigidbody playerPhysics = null;

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
        playerPhysics = playerObj.GetComponent<Rigidbody>();

        if (player == null || playerPhysics == null)
        {
            Debug.Log("No player controller object found ... disabling CutsceneController");
            this.enabled = false;
            return;
        }

        // Switch to the cutscene camera and disable inputs
        introCam.enabled = true;
        playerCam.enabled = false;

        player.ignoreInputs = true;
        playerPhysics.useGravity = false;
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void animationDone(float time)
    {
        // End of the intro cutscene giving control to player

        introCam.enabled = false;
        playerCam.enabled = true;
        
        player.ignoreInputs = false;
        playerPhysics.useGravity = true;
    }
}
