using UnityEngine;
using System;
using System.Collections;

public class Pickupable : MonoBehaviour {

    public static event Action<Pickupable> OnPickup;

    public void OnTriggerEnter(Collider other)
    {
        OnPickup(this);
    }

    public virtual GameManager.GameStats addToStats(GameManager.GameStats currentStats)
    {
        return currentStats;
    }
}