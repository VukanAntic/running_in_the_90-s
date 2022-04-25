using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement player;
    public float cameraSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(transform.position.x, player.transform.position.y, MainCam.transform.position.z);
    }
}
