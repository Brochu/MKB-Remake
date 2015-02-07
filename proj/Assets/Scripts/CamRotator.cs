using UnityEngine;
using System.Collections;

public class CamRotator : MonoBehaviour
{
    public Camera cam = null;
    public GameObject player = null;

    public float maxHorizontalAngle = 10.0f;
    public float defaultHAngle = 0.0f;

    public float maxVerticalAngle = 10.0f;
    public float defaultVAngle = 5.0f;

    public float distWithPlayer = 0.0f;

    void Start()
    {
        if (!cam)
        {
            Debug.Log("CameraRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }

        //distWithPlayer = (player.transform.position - cam.transform.position).magnitude;
    }

    void Update()
    {
        // Calcul de l'angle vertical
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float HorAngle = (maxHorizontalAngle * x) + defaultHAngle;
        float vertAngle = ((maxVerticalAngle * y) * -1) + defaultVAngle; // The result is way better with this inversed

        Quaternion newDir = Quaternion.Euler(new Vector3(vertAngle, 0, HorAngle));

        cam.transform.rotation = newDir;

        // Vertical angle changes must move camera around player ball
        vertAngle = 90 - vertAngle; // opposed
        vertAngle *= Mathf.Deg2Rad;

        float newy = player.transform.position.y + (Mathf.Cos(vertAngle) * distWithPlayer);
        float newz = player.transform.position.z - (Mathf.Sin(vertAngle) * distWithPlayer);

        Vector3 newpos = new Vector3(player.transform.position.x, newy, newz);

        cam.transform.position = newpos;
    }
}