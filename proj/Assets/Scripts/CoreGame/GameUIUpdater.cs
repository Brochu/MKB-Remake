using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIUpdater : MonoBehaviour {

    public Text timeTxt = null;
    public Text pickupTxt = null;

    private string timeStr = "Time : ";
    private string pickupStr = "Ohhhh Banana : ";

    // Use this for initialization
    void Start () {
        GameManager.onUpdateUI += updateUI;
    }
    
    void OnDestroy()
    {
        GameManager.onUpdateUI -= updateUI;
    }

    private void updateUI(GameManager.GameStats currentStats)
    {
        updateTime(currentStats);
        updatePickupCount(currentStats);
    }

    private void updateTime(GameManager.GameStats currentStats)
    {
        timeTxt.text = timeStr + string.Format("{0:f1}", currentStats.time);
    }

    private void updatePickupCount(GameManager.GameStats currentStats)
    {
        // Check for maxPickupCount
        if (currentStats.pickupCount >= currentStats.maxPickupCount)
        {
            currentStats.pickupCount = 0;
            // Maybe do something special ??
        }

        pickupTxt.text = pickupStr + currentStats.pickupCount.ToString();
    }
}
