using UnityEngine;
using System.Collections;

public class StageRotator : MonoBehaviour
{
    public GameObject stage = null;
    public float maxHorizontalAngle = 0.0f;
    public float maxVerticalAngle = 0.0f;

    private Quaternion defaultStageDirection;

    void Start()
    {
        Debug.Log("StageRotator script: Starting !");
        if (!stage)
        {
            Debug.Log("StageRotator: Il manque un objet de type camera. Stopping script");

            // Desactive
            this.enabled = false;
        }
        else
        {
            defaultStageDirection = stage.transform.rotation;
        }
    }

    void Update()
    {
        // Calcul de l'angle vertical
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float HorAngle = maxHorizontalAngle * x;
        float vertAngle = maxVerticalAngle * y;

        Quaternion toAdd = Quaternion.Euler(new Vector3(vertAngle, 0, -HorAngle));

        stage.transform.rotation = defaultStageDirection * toAdd;
    }
}