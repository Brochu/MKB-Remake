using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour {

    public GameObject obj = null;
    public float speed = 0.0f;
    public float force = 0.0f;

    private Transform objTansform = null;
    private float currentYRot = 0.0f;

    void Start () {

        if (obj == null)
        {
            Debug.Log("NO OBJECT FOUND!");
            this.enabled = false;
        }

        objTansform = obj.transform;
        currentYRot = objTansform.rotation.y;
    }
    
    void Update () {
        currentYRot = (currentYRot + speed) % 360;

        Quaternion currentRot = Quaternion.Euler(0, currentYRot, 0);
        objTansform.rotation = currentRot;
    }

    void OnCollisionEnter(Collision c)
    {
        Transform pachinko = objTansform;
        Transform bumpee = c.gameObject.transform;

        Vector3 dir = (bumpee.position - pachinko.position).normalized;
        dir *= force;

        c.rigidbody.AddForce(dir, ForceMode.Impulse);
    }
}
