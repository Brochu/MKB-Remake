using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public float time = 0.0f;
    public int bananaCount = 0;
    public Text timeTxt = null;
    public Text bananaTxt = null;

    private string timeStr = "Time : ";
    private string bananaStr = "Ohhhh Banana : ";
    private int maxBananaCount = 100;

    // Use this for initialization
    void Start () {
        Debug.Log("Stage is started");
        StartCoroutine(stageStartTimer());
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.B)) addBanana();
    }

    public void updateTime()
    {
        timeTxt.text = timeStr + string.Format("{0:f1}", time);
    }

    public void updateBananaCount()
    {
        bananaTxt.text = bananaStr + bananaCount.ToString();
    }

    private void addBanana()
    {
        bananaCount = (bananaCount + 1) % maxBananaCount;
        updateBananaCount();
    }

    IEnumerator stageStartTimer()
    {
        updateTime();
        Debug.Log("Starting countdown in ...");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("GO !");

        while (time > 0)
        {
            updateTime();
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
        }

        Debug.Log("Time over !");
    }
}
