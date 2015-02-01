using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public GameObject objectToFollow = null;
    private Vector3 lastObjectPos = Vector3.zero;

    void Start () {
        if (!objectToFollow)
        {
            Debug.Log("FollowCam: Pas d'objet a suivre ... desactivation !");
            this.enabled = false;
        }
        lastObjectPos = objectToFollow.transform.position;
    }
    
    void LateUpdate () {
        // Appliquer le mouvement
        lastObjectPos += Vector3.zero;

        lastObjectPos = objectToFollow.transform.position;
    }
}
