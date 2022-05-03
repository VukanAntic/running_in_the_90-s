using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{

    public TapeGenerator tapeGenerator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collide!");
        AudioManager manager = FindObjectOfType<AudioManager>();
        FindObjectOfType<AudioManager>().Play(tapeGenerator.current_tape);
        Destroy(gameObject);
    }
}
