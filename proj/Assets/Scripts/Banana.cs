using UnityEngine;
using System.Collections;

public class Banana : MonoBehaviour {

    public float spinSpeed = 0.0f;

    private Transform t = null;
    private float currentYRot = 0.0f;
    private bool pickupable = true;

    void Start () {
        t = gameObject.transform;
    }
    
    void Update () {
        currentYRot = (currentYRot + spinSpeed) % 360;
        t.rotation = Quaternion.Euler(0.0f, currentYRot, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!pickupable)
            return;

        pickupable = false;

        // update banana count
        StartCoroutine(pickup());
    }

    IEnumerator pickup()
    {
        for (int i = 0; i < 10; i++)
        {
            t.localScale = t.localScale * 0.7f;
            yield return null;
        }

        Destroy(gameObject);
    }
}