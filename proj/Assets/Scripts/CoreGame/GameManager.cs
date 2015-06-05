using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject playerObj = null;
    public Camera playerCam = null;
    public Camera introCam = null;
    public float startTime = 0.0f;

    private IntroCutsceneController introController = null;
    private PlayerController player = null;
    private Rigidbody playerPhysics = null;

    public class GameStats
    {
        public int lives = 5;
        public float time = 0.0f;
        public int score = 0;
        public int pickupCount = 0;
        public int maxPickupCount = 100;
    }
    public GameStats currentStats;
    public static event Action<GameStats> onUpdateUI;

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

        currentStats = new GameStats();
        currentStats.time = startTime;

        startCutscene();
        onUpdateUI(currentStats);
    }
    
    // Update is called once per frame
    void Update () {
    }

    void OnDestroy()
    {
        introController.IntroDone -= onIntroDone;
        Pickupable.OnPickup -= onPickup;
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
        StartCoroutine(stageStartTimer());
    }

    private IEnumerator givePlayerControls()
    {
        yield return new WaitForSeconds(1);
        introCam.enabled = false;
        playerCam.enabled = true;
        
        player.ignoreInputs = false;
    }

    private IEnumerator stageStartTimer()
    {
        while (currentStats.time >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentStats.time -= 0.1f;
            onUpdateUI(currentStats);
        }

        // TODO: add event to signal stage failed
        playerPhysics.Sleep();
        playerCam.GetComponent<CamRotatorV2>().enabled = false;
        player.ignoreInputs = true;
    }

    private void onPickup(Pickupable item)
    {
        currentStats = item.addToStats(currentStats);
        onUpdateUI(currentStats);
    }
}