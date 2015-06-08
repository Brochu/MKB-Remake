using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public float startTime = 60.0f;

    public class GameStats
    {
        public int lives = 5;
        public float time = 0.0f;
        public int score = 0;
        public int pickupCount = 0;
        public int maxPickupCount = 100;
    }
    public GameStats currentStats;
    public static event Action onInitDone;
    public static event Action<GameStats> onUpdateUI;
    public static event Action onTimeOver;

    // add this to system to handle more than one player
    /*
    private int numberOfPlayer = 1;
    private int currentPlayerId = 0;
    */

    // Use this for initialization
    void Start () {
        GameUIUpdater.onUILoaded += onUILoaded;
        IntroCutsceneController.onStartTimer += onStartTimer;
        Pickupable.OnPickup += onPickup;

        currentStats = new GameStats();
        currentStats.time = startTime;

        updateUI();

        DontDestroyOnLoad(this);
        onInitDone();
    }

    // Update is called once per frame
    void Update () {
    }

    void OnDestroy()
    {
        GameUIUpdater.onUILoaded -= onUILoaded;
        IntroCutsceneController.onStartTimer -= onStartTimer;
        Pickupable.OnPickup -= onPickup;
    }

    private void onUILoaded()
    {
        updateUI();
    }

    private void onStartTimer()
    {
        StartCoroutine(stageStartTimer());
    }

    private void updateUI()
    {
        if (onUpdateUI != null)
            onUpdateUI(currentStats);
    }

    private IEnumerator stageStartTimer()
    {
        while (currentStats.time >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentStats.time -= 0.1f;
            updateUI();
        }

        // TODO: add event to signal stage failed
        onTimeOver();
    }

    private void onPickup(Pickupable item)
    {
        currentStats = item.addToStats(currentStats);
        updateUI();
    }
}