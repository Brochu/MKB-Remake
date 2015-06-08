using UnityEngine;
using System;
using System.Collections;

public class IntroCutsceneController : MonoBehaviour {

    public static event Action onStartTimer;

    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;

    private PlayerController player = null;
    private Rigidbody playerPhysics = null;

    // Use this for initialization
    void Start () 
    {
        // Get payer controller object
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

        startCutscene();
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void animationDone(float time)
    {
        // End of the intro cutscene giving control to player
        onIntroDone();
    }

    private void startCutscene()
    {
        // Switch to the cutscene camera and disable inputs
        playerCam.enabled = false;
        introCam.enabled = true;

        player.ignoreInputs = true;
        playerCam.enabled = false;
        playerPhysics.useGravity = false;
    }

    private void onIntroDone()
    {
        playerPhysics.useGravity = true;
        StartCoroutine(givePlayerControls());

        onStartTimer();
    }

    private IEnumerator givePlayerControls()
    {
        yield return new WaitForSeconds(1);

        introCam.enabled = false;
        playerCam.enabled = true;
        player.ignoreInputs = false;
    }
}