using UnityEngine;
using System.Collections;

public class CamRotatorV2 : MonoBehaviour
{
    public Camera cam = null;
    public GameObject player = null;

    public float maxHorizontalAngle = 10.0f;
    public float maxVerticalAngle = 10.0f;
    public float defaultVerticalAngle = 10.0f;
    public float distWithPlayer = 10.0f;
    public float camSpinSpeed = 5.0f;

    public bool invertHorizontal = false;

    private float phi = 0.0f;
    private float theta = 0.0f;

    void Start()
    {
        if (!cam)
        {
            Debug.Log("No camera found!");
            this.enabled = false;
        }

        if (!player)
        {
            Debug.Log("No player found!");
            this.enabled = false;
        }
    }

    void Update()
    {
        float spinInput = (invertHorizontal) ? Input.GetAxis("Horizontal2") : -Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical1");
        float horizontalInput = Input.GetAxis("Horizontal1");

        phi = phi + (spinInput * camSpinSpeed);
        theta = (-1 * defaultVerticalAngle) + (verticalInput * maxVerticalAngle);

        Vector3 newPos = player.transform.position;
        float newx = 0.0f;
        float newy = 0.0f;
        float newz = 0.0f;

        newx = distWithPlayer * Mathf.Sin(phi * Mathf.Deg2Rad) * Mathf.Cos(theta * Mathf.Deg2Rad);
        newy = distWithPlayer * Mathf.Sin(theta * Mathf.Deg2Rad);
        newz = distWithPlayer * Mathf.Cos(phi * Mathf.Deg2Rad) * Mathf.Cos(theta * Mathf.Deg2Rad);

        newPos += new Vector3((-1 * newx), (-1 * newy), (-1 * newz));

        cam.transform.position = newPos;
        cam.transform.rotation = Quaternion.Euler(new Vector3((-1 * theta), phi, (horizontalInput * maxHorizontalAngle)));
    }
}