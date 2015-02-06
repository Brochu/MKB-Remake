using UnityEngine;
using System.Collections;

public class PhysicSim : MonoBehaviour {

    public GameObject player = null;
    public float maxForce = 10.0f;

    private Rigidbody playerBody = null;

    // Use this for initialization
    void Start () {

        Debug.Log("PhysicSim started !");
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(x * maxForce, 0, z * maxForce);

        playerBody.AddForce(force, ForceMode.Acceleration);
    }
}
