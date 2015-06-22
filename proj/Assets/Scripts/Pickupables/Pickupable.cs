using UnityEngine;
using System;
using System.Collections;

public class Pickupable : MonoBehaviour {

    public enum Axis
    {
        X,
        Y,
        Z
    }

    public Axis rotateAxis;
    public float rotateSpeed = 5f;

    public static event Action<Pickupable> OnPickup;
    private bool isActive = true;
    private Vector3 axisVector;

    public void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        isActive = false;
        OnPickup(this);

        // Destroy the object after
        StartCoroutine(destroyPickup());
    }

    public virtual GameManager.GameStats addToStats(GameManager.GameStats currentStats)
    {
        return currentStats;
    }

    void Start()
    {
        axisVector = new Vector3((rotateAxis == Axis.X) ? 1 : 0, (rotateAxis == Axis.Y) ? 1 : 0, (rotateAxis == Axis.Z) ? 1 : 0);
    }
    void Update()
    {
        gameObject.transform.Rotate(axisVector, rotateSpeed);
    }

    IEnumerator destroyPickup()
    {
        Vector3 scaleStep = new Vector3(0.2f, 0f, 0.2f);

        while (gameObject.transform.localScale.x >= 0.05f)
        {
            gameObject.transform.localScale -= scaleStep;
            yield return null;
        }

        Destroy(gameObject);
    }
}