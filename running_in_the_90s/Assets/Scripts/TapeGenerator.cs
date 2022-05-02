using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerMovement player_movement;
    [SerializeField] private Transform player_transform;
    [SerializeField] private GameObject[] all_tapes;
    private float position_of_tape_y;

    // odlucimo na koliko sek se pusti pesma
    private int lower_bound_time_interval = 10;
    private int upper_bound_time_interval = 20;
    // ova promenljiva uzima random vrednost iz [lb, up], i ako je proslo otp toliko vrmena, onda radi
    private float time_chosen_for_next_tape;
    // nije = 0, da ne bi spawn odmah na pocetku tape
    private float time_passed = 0.0f;

    private GameObject new_tape;

    public float probability_of_secret_tape = 0.005f;
    private bool secret_tape_already_spawned = false;
    private int number_of_tapes_spawned = 0;

    [SerializeField] private Camera main_camera;
    private float camera_width;

    // na kolikoj udaljenosti zelimo sledeci tape


    void Start()
    {
        time_chosen_for_next_tape = Random.Range(lower_bound_time_interval, upper_bound_time_interval);
        // zelimo samo u odnosu na prvu poziciju player-a, tj, da bi bilo uvek u istom y (menjacemo vrv)
        position_of_tape_y = player_transform.position.y;
        main_camera = Camera.main;
        camera_width = 2f * main_camera.orthographicSize * main_camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_movement.playerStartedMoving)
        {
            // verovatno if je kliknuto s, onda radi, iz player movement-a
            time_passed += Time.deltaTime;
            //Debug.Log(time_passed);
            if (player_transform != null && Mathf.Ceil(time_passed) % time_chosen_for_next_tape == 0)
            {

                time_passed = 0f;
                // biramo posle koliko sekundi cemo spawn sledecu tape
                time_chosen_for_next_tape = Random.Range(lower_bound_time_interval, upper_bound_time_interval);
                //Debug.Log("TIME CHOSEN FOR NEXT SPAWN:");
                //Debug.Log(time_chosen_for_next_tape);
                // the last tape!
                if (Random.value < probability_of_secret_tape && !secret_tape_already_spawned
                    && number_of_tapes_spawned > all_tapes.Length)
                {
                    new_tape = Object.Instantiate(all_tapes[all_tapes.Length - 1]);
                    secret_tape_already_spawned = false;
                }
                else
                {
                    // -1, jer zelimo sve, osim poslednje da mogu da se spawn!
                    new_tape = Object.Instantiate(all_tapes[Random.Range(0, all_tapes.Length - 1)]);
                    ++number_of_tapes_spawned;
                }
                // ovaj vektor bi trebao biti range od kamere!
                new_tape.transform.position = new Vector3(camera_width + player_transform.position.x,
                    0 + position_of_tape_y, 0);
            }

        }
        
    }
}
