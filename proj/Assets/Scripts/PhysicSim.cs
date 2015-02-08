using UnityEngine;
using System.Collections;

public class PhysicSim : MonoBehaviour {

    public GameObject player = null;
    public Camera cam = null;
    public float maxForce = 10.0f;

    private Rigidbody playerBody = null;

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
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 forward = (player.transform.position - cam.transform.position).normalized;
        forward.y = 0;
        Vector3 side = Vector3.Cross(forward, Vector3.up);
        side *= -1;

        float x = Input.GetAxis("Horizontal1") * maxForce;
        float z = Input.GetAxis("Vertical1") * maxForce;

        Vector3 force = (forward * z) + (side * x);

        playerBody.AddForce(force, ForceMode.Acceleration);
    }
}
