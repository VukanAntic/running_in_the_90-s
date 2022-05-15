using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "Death")
        {
            deathScreen.SetActive(!deathScreen.activeSelf);
            Time.timeScale = 0f;
        }
    }
    */
}
