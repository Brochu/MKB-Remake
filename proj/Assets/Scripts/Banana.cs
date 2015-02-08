using UnityEngine;
using System.Collections;

public class Banana : MonoBehaviour {

    public float spinSpeed = 0.0f;
    public float xRotation = 0.0f;
    public float zRotation = 0.0f;

    private Transform t = null;
    private float currentYRot = 0.0f;
    private bool pickupable = true;

    void Start () {
        t = gameObject.transform;
    }
    
    void Update () {
        currentYRot = (currentYRot + spinSpeed) % 360;
        t.rotation = Quaternion.Euler(xRotation, currentYRot, zRotation);
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
            t.localScale = t.localScale * 0.6f;
            yield return null;
        }

        Destroy(gameObject);
    }
}