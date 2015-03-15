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
    public float camSpinSpeed = 1.0f;

    public bool invertHorizontal = false;

    private float camLookRot = 0.0f;
    private Rigidbody playerBody = null;

    void Start()
    {
        if (!cam)
        {
            Debug.Log("CameraRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }

        if (player != null)
        {
            playerBody = player.rigidbody;
        }

        if (!playerBody)
        {
            Debug.Log("CameraRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }
    }

    void Update()
    {
        // Obtenir les entrees des joysticks
        Vector3 entrees = getInputAxisInfo();

        // Facteur de vitesse en y
        float yspeed = -1 * (playerBody.velocity.y) * 7;
        if (yspeed < 0) yspeed = 0;
        if (yspeed > 40.0f) yspeed = 40.0f;
        Debug.Log(yspeed);

        // Calcul des angles pour la camera par rapport aux mouvements
        float horAngle = (maxHorizontalAngle * entrees.x) + defaultHAngle;
        float vertAngle = ((maxVerticalAngle * entrees.z) * -1) + yspeed + defaultVAngle; // The result is way better with this inversed

        // Calcul de l'angle pour tourner la camera
        camLookRot = (camLookRot + entrees.y * camSpinSpeed) % 360;

        // Rotation finale
        Quaternion newDir = Quaternion.Euler(new Vector3(vertAngle, -camLookRot, horAngle));
        cam.transform.rotation = newDir;

        // Le mouvement de la camera pour le mouvement
        vertAngle = 90 - vertAngle; // opposed
        vertAngle *= Mathf.Deg2Rad;
        float camLookRad = camLookRot * Mathf.Deg2Rad;

        float newx = player.transform.position.x + (Mathf.Sin(camLookRad) * distWithPlayer);
        float newy = player.transform.position.y + (Mathf.Cos(vertAngle) * distWithPlayer);
        float newz = player.transform.position.z - (Mathf.Cos(camLookRad) * distWithPlayer);

        Vector3 newpos = new Vector3(newx, newy, newz);

        cam.transform.position = newpos;
    }

    private Vector3 getInputAxisInfo(){
        return new Vector3(Input.GetAxis("Horizontal1"), (invertHorizontal) ? Input.GetAxis("Horizontal2") : -Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical1"));
    }
}