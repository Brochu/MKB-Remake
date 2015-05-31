using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;

    public float startTime = 0.0f;
    public Text timeTxt = null;
    public Text pickupTxt = null;

    private IntroCutsceneController introController = null;
    private PlayerController player = null;
    private Rigidbody playerPhysics = null;

    private string timeStr = "Time : ";
    private string pickupStr = "Ohhhh Banana : ";
    private int maxPickupCount = 100;

    public class GameStats
    {
        public float time = 0.0f;
        public int pickupCount = 0;
        public int score = 0;
    }
    public GameStats currentStats;

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
        introController.IntroDone += onIntroDone;

        Pickupable.OnPickup += onPickup;

        Debug.Log("Stage is started");
        currentStats = new GameStats();
        currentStats.time = startTime;

        startCutscene();
        updateTime();
        updatePickupCount();
    }
    
    // Update is called once per frame
    void Update () {
    }

    void OnDestroy()
    {
        introController.IntroDone -= onIntroDone;
        Pickupable.OnPickup -= onPickup;
    }

    public void updateTime()
    {
        timeTxt.text = timeStr + string.Format("{0:f1}", currentStats.time);
    }

    public void updatePickupCount()
    {
        // Check for maxPickupCount
        if (currentStats.pickupCount >= maxPickupCount)
        {
            currentStats.pickupCount = 0;
            // Maybe do something special ??
        }

        pickupTxt.text = pickupStr + currentStats.pickupCount.ToString();
    }

    private void startCutscene()
    {
        // Switch to the cutscene camera and disable inputs
        introCam.enabled = true;
        playerCam.enabled = false;

        player.ignoreInputs = true;
        playerPhysics.useGravity = false;
    }

    private void onIntroDone()
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

        // Find a way to clean this up
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

        while (currentStats.time >= 0)
        {
            updateTime();
            yield return new WaitForSeconds(0.1f);
            currentStats.time -= 0.1f;
        }

        // TODO: add event to signal stage failed
        Debug.Log("Time over !");
        playerPhysics.Sleep();
        playerCam.GetComponent<CamRotatorV2>().enabled = false;
        player.ignoreInputs = true;
    }

    private void onPickup(Pickupable item)
    {
        currentStats = item.addToStats(currentStats);
        updatePickupCount();
    }
}