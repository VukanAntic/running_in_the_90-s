using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private float score;
    public GameObject startMenu;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null && !startMenu.activeSelf)
        {
            score += 1 * Time.deltaTime;
            scoreText.text = "Distance passed: " + ((int)score).ToString();
        }    
    }
}
