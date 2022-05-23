using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerMovements player_movement;
    [SerializeField] private Transform player_transform;
    [SerializeField] private GameObject[] all_tapes;
    private float position_of_tape_y;
    public string current_tape;

    private int lower_bound_time_interval = 25;
    private int upper_bound_time_interval = 45;
    private float time_chosen_for_next_tape;
    private float time_passed = 0.0f;

    private GameObject new_tape;

    public float probability_of_secret_tape = 0.005f;
    private bool secret_tape_already_spawned = false;
    private int number_of_tapes_spawned = 0;

    [SerializeField] private Camera main_camera;
    private float camera_width;


    void Start()
    {
        current_tape = "Tape-1";
        time_chosen_for_next_tape = Random.Range(lower_bound_time_interval, upper_bound_time_interval);
        position_of_tape_y = player_transform.position.y;
        main_camera = Camera.main;
        camera_width = 2f * main_camera.orthographicSize * main_camera.aspect;
        time_passed = 0.0001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_movement.playerStartedMoving)
        {
            time_passed += Time.deltaTime;
            if (player_transform != null && Mathf.Ceil(time_passed) % time_chosen_for_next_tape == 0)
            {

                time_passed = 0f;
                time_chosen_for_next_tape = Random.Range(lower_bound_time_interval, upper_bound_time_interval);
                if (Random.value < probability_of_secret_tape && !secret_tape_already_spawned
                    && number_of_tapes_spawned > all_tapes.Length)
                {
                    current_tape = "Tape-secret";
                    new_tape = Object.Instantiate(all_tapes[all_tapes.Length - 1]);
                    Tape tape_info = new_tape.AddComponent<Tape>();
                    tape_info.tapeGenerator = this;
                    secret_tape_already_spawned = false;
                }
                else
                {
                    int tape_chosen = Random.Range(0, all_tapes.Length - 1);
                    current_tape = "Tape-" + (tape_chosen + 1);
                    new_tape = Object.Instantiate(all_tapes[tape_chosen]);
                    Tape tape_info = new_tape.AddComponent<Tape>();
                    tape_info.tapeGenerator = this;
                    ++number_of_tapes_spawned;
                }
                new_tape.transform.position = new Vector3(camera_width + player_transform.position.x,
                    0 + position_of_tape_y, 0);
            }

        }
        
    }
}
