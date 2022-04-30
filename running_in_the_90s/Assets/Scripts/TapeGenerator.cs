using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeGenerator : MonoBehaviour
{
    [SerializeField] private Transform player_transform;
    [SerializeField] private GameObject[] all_tapes;
    private float position_of_last_tape = 0f;
    private float position_of_tape_y;

    // na kolikoj udaljenosti zelimo sledeci tape
    public int tape_distance;
    // Start is called before the first frame update
    void Start()
    {
        // zelimo samo u odnosu na prvu poziciju player-a, tj, da bi bilo uvek u istom y (menjacemo vrv)
        position_of_tape_y = player_transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_transform!= null && Mathf.Ceil(player_transform.position.x) % tape_distance == 0 
            && position_of_last_tape != Mathf.Ceil(player_transform.position.x)) 
        {
            // da ne bismo spanovali milion tapeova od [10, 11]
            position_of_last_tape = Mathf.Ceil(player_transform.position.x);
            // weighted je izbor, izmeni
            GameObject new_tape = Object.Instantiate(all_tapes[Random.Range(0, all_tapes.Length)]);
            // ovaj vektor bi trebao biti range od kamere
            new_tape.transform.position = new Vector3(Random.Range(10, 20) + player_transform.position.x,
                0 + position_of_tape_y, 0);
        }
    }
}
