using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    // private float moveInput;
    [SerializeField] private float speed = 7.0f;

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

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerSlide = GetComponent<PlayerSlide>();
        animator = GetComponent<Animator>();
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
        }
	}

}
