using UnityEngine;
using System.Collections;

public class AddToStatsPickup : Pickupable {
    public int livesBonus = 0;
    public float timeBonus = 0;
    public int scoreBonus = 0;
    public int pickupAmount = 0;

    public override GameManager.GameStats addToStats(GameManager.GameStats currentStats)
    {
        currentStats.lives += livesBonus;
        currentStats.time += timeBonus;
        currentStats.score += scoreBonus;
        currentStats.pickupCount += pickupAmount;

        return currentStats;
    }
}
