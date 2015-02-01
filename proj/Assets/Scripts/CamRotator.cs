using UnityEngine;
using System.Collections;

public class CamRotator : MonoBehaviour
{
    public Camera cam;
    public float maxHorizontalAngle;
    public float maxVerticalAngle;

    private Quaternion defaultCamDirection;
    // private Vector3 defaultCamPosition;

    void Start()
    {
        if (!cam)
        {
            Debug.Log("CameraRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }
        defaultCamDirection = cam.transform.rotation;
        // defaultCamPosition = cam.transform.position;
    }

    void Update()
    {
        // Calcul de l'angle vertical
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float HorAngle = maxHorizontalAngle * x;
        float vertAngle = maxVerticalAngle * y;

        Quaternion toAdd = Quaternion.Euler(new Vector3(vertAngle, 0, HorAngle));

        cam.transform.rotation = defaultCamDirection * toAdd;
        // cam.transform.position = defaultCamPosition + new Vector3(0, 0, angle);
    }

    //void LateUpdate()
    //{
    //}
}
