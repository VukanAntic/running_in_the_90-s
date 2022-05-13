using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private List<Sprite> DroneSprites;

    // Start is called before the first frame update
    void Start()
    {
        int droneNumber = Random.Range(0, DroneSprites.Count);
        this.GetComponent<SpriteRenderer>().sprite = DroneSprites[droneNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
