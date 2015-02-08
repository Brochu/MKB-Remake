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

    private float camLookRot = 0.0f;
    //private Vector3 playerLastPos = Vector3.zero;

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
        // Obtenir les entrees des joysticks
        Vector3 entrees = getInputAxisInfo();

        // Calcul des angles pour la camera par rapport aux mouvements
        float HorAngle = (maxHorizontalAngle * entrees.x) + defaultHAngle;
        float vertAngle = ((maxVerticalAngle * entrees.z) * -1) + defaultVAngle; // The result is way better with this inversed

        // Calcul de l'angle pour tourner la camera
        camLookRot = (camLookRot + entrees.y * camSpinSpeed) % 360;

        // Rotation finale
        Quaternion newDir = Quaternion.Euler(new Vector3(vertAngle, -camLookRot, HorAngle));
        cam.transform.rotation = newDir;

        // Le mouvement de la camera pour le mouvement
        vertAngle = 90 - vertAngle; // opposed
        vertAngle *= Mathf.Deg2Rad;

        float newy = player.transform.position.y + (Mathf.Cos(vertAngle) * distWithPlayer);
        float newz = player.transform.position.z - (Mathf.Sin(vertAngle) * distWithPlayer);

        Vector3 newpos = new Vector3(player.transform.position.x, newy, newz);

        cam.transform.position = newpos;
    }

    private Vector3 getInputAxisInfo(){
        return new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical1"));
    }
}