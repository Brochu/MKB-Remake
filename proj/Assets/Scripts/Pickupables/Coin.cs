using UnityEngine;
using System.Collections;

public class Coin : Pickupable {

    public override GameManager.GameStats addToStats(GameManager.GameStats currentStats)
    {
        currentStats.pickupCount++;
        return currentStats;
    }
}