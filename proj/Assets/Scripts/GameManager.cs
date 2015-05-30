using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;

    public float time = 0.0f;
    public int bananaCount = 0;
    public Text timeTxt = null;
    public Text bananaTxt = null;

    private IntroCutsceneController introController = null;
    private PlayerController player = null;
    private Rigidbody playerPhysics = null;

    private string timeStr = "Time : ";
    private string bananaStr = "Ohhhh Banana : ";
    private int maxBananaCount = 100;

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

        introController = introCam.GetComponent<IntroCutsceneController>();
        introController.IntroDone += startStage;

        Debug.Log("Stage is started");
        startCutscene();
    }
    
    // Update is called once per frame
    void Update () {
    }

    void OnDestroy()
    {
        introController.IntroDone -= startStage;
    }

    public void updateTime()
    {
        timeTxt.text = timeStr + string.Format("{0:f1}", time);
    }

    public void updateBananaCount()
    {
        bananaTxt.text = bananaStr + bananaCount.ToString();
    }

    private void startCutscene()
    {
        // Switch to the cutscene camera and disable inputs
        introCam.enabled = true;
        playerCam.enabled = false;

        player.ignoreInputs = true;
        playerPhysics.useGravity = false;
    }

    private void addBanana()
    {
        bananaCount = (bananaCount + 1) % maxBananaCount;
        updateBananaCount();
    }

    private void startStage()
    {
        playerPhysics.useGravity = true;
        StartCoroutine(givePlayerControls());
    }

    IEnumerator givePlayerControls()
    {
        yield return new WaitForSeconds(1);
        introCam.enabled = false;
        playerCam.enabled = true;
        
        player.ignoreInputs = false;

        StartCoroutine(stageStartTimer());
    }

    IEnumerator stageStartTimer()
    {
        /*
        updateTime();
        Debug.Log("Starting countdown in ...");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("GO !");
        */

        while (time >= 0)
        {
            updateTime();
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
        }

        // TODO: add event to signal stage failed
        Debug.Log("Time over !");
        player.ignoreInputs = true;
        playerPhysics.Sleep();
    }
}
