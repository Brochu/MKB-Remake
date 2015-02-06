using UnityEngine;
using System.Collections;

public class CamRotator : MonoBehaviour
{
    public Camera cam;
    public float maxHorizontalAngle;
    public float maxVerticalAngle;

    private Quaternion defaultCamDirection;

    void Start()
    {
        if (!cam)
        {
            Debug.Log("CameraRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }
        defaultCamDirection = cam.transform.rotation;
    }

    void Update()
    {
        // Calcul de l'angle vertical
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float HorAngle = maxHorizontalAngle * x;
        float vertAngle = (maxVerticalAngle * y) * -1; // The result is way better with this inversed

        Quaternion toAdd = Quaternion.Euler(new Vector3(vertAngle, 0, HorAngle));

        cam.transform.rotation = defaultCamDirection * toAdd;
    }
}
