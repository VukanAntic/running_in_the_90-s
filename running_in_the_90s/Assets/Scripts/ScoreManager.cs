using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Transform Player;
    private float PlayerStartX;

    public Text scoreText;
    private float score;
    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerStartX = Player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null && !startMenu.activeSelf)
        {
            score = Player.position.x - PlayerStartX;
            scoreText.text = ((int)score).ToString();
        }
    }
}
