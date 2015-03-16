using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float time;
    // Use this for initialization
    void Start () {
        Debug.Log("Stage is started");
        StartCoroutine(stageStartTimer());
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    IEnumerator stageStartTimer()
    {
        Debug.Log("Starting countdown in ...");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("GO !");

        while (time > 0)
        {
            Debug.Log(time + " sec");
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
        }

        Debug.Log("Time over !");
    }
}
