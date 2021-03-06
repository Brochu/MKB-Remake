﻿using UnityEngine;
using System.Collections;

public class PhysicSim : MonoBehaviour {

    public GameObject player = null;
    public Camera cam = null;
    public float maxForce = 10.0f;

    private Rigidbody playerBody = null;
    private PlayerController playerControls = null;

    // Use this for initialization
    void Start () {

        if (player == null)
        {
            Debug.Log("PhysicSim Script : NO PLAYER FOUND !");
            this.enabled = false;
        }

        playerBody = player.GetComponent<Rigidbody>();
        if (playerBody == null)
        {
            Debug.Log("PhysicSim Script : NO PLAYER RIGID BODY FOUND !");
            this.enabled = false;
        }

        playerControls = player.GetComponent<PlayerController>();
        if (playerControls== null)
        {
            Debug.Log("PhysicSim Script : NO CONTROLS FOUND !");
            this.enabled = false;
        }

        GameManager.onTimeOver += onTimeOver;
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 forward = (player.transform.position - cam.transform.position);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 side = Vector3.Cross(forward, Vector3.up);
        side.y = 0;
        side = side.normalized;

        float x = playerControls.getHorizontalInput() * maxForce;
        float z = playerControls.getVerticalInput() * maxForce;

        Vector3 force = (forward * z) + (side * x * -1);

        if (force != Vector3.zero)
            playerBody.AddForce(force, ForceMode.Acceleration);
    }

    void OnDestroy()
    {
        GameManager.onTimeOver -= onTimeOver;
    }

    void onTimeOver()
    {
        gameObject.GetComponent<Rigidbody>().Sleep();
    }
}
