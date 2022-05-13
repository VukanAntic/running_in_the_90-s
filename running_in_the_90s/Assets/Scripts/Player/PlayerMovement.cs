using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    private Vector3 playerCameraDistance;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    // private float moveInput;

    public bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce = 7.0F;

    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping = false;

    public PlayerSlide playerSlide;
    public bool playerStartedMoving = false;

    public float speed = 7.0f;

    [SerializeField] private Text Score;
    private float Boost;
    private int BoostDistance;
    private bool ShouldBoost;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerSlide = GetComponent<PlayerSlide>();
        animator = GetComponent<Animator>();

        playerCameraDistance = new Vector3(transform.position.x - mainCamera.transform.position.x, 0, 0);

        Boost = 0.5f;
        BoostDistance = 100;
        ShouldBoost = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerStartedMoving = true;
        }

        if (playerStartedMoving)
        {
            // might need to change the checkRadius value, a little bit greater, this works for now
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            animator.SetBool("isJumping", !isGrounded);

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && !playerSlide.isSliding)
            {
                rigidBody.velocity = Vector2.up * jumpForce;
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }

            if (Input.GetKey(KeyCode.Space) && isJumping && !playerSlide.isSliding)
            {
                if (jumpTimeCounter > 0)
                {
                    rigidBody.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }

            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }

        }            
    }

    private void FixedUpdate()
	{
        // treba probati GetAxisRaw, mozda je bolje tako
        // moveInput = Input.GetAxis("Horizontal");
        // FEATURE:
        // once our player presses start, only then will he start moving!
        if (playerStartedMoving)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            animator.SetFloat("Speed", speed);
            mainCamera.transform.position = 
                new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z) - playerCameraDistance;
        }


        int scr = Int32.Parse(Score.text);
        if (ShouldBoost && scr != 0 && scr % BoostDistance == 0)
        {
            speed += Boost;
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            animator.SetFloat("Speed", speed);
            ShouldBoost = false;
        } else if(scr % BoostDistance == 1)
        {
            ShouldBoost = true;
        }
	}

}
