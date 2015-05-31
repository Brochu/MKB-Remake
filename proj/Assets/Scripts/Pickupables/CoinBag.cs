using UnityEngine;
using System.Collections;

public class CoinBag : Pickupable {

    public override GameManager.GameStats addToStats(GameManager.GameStats currentStats)
    {
        currentStats.pickupCount += 10;
        return currentStats;
    }
}