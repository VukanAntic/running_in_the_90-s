using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidBody;
    // private float moveInput;
    [SerializeField] private float speed = 7.0f;

    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce = 7.0F;

    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    public bool isJumping = false;

    public PlayerSlide playerSlide;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerSlide = GetComponent<PlayerSlide>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
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
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
    }

    private void FixedUpdate()
	{
        // treba probati GetAxisRaw, mozda je bolje tako
        // moveInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);

	}

}
