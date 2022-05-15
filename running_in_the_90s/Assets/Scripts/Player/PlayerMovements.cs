using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector3 playerCameraDistance;

    [SerializeField] GameObject deathScreen;

    private Animator animator;
    private Rigidbody2D rigidBody;

    private bool canJump;
    private bool isJumping;
    [SerializeField] private float jumpSpeed;

    private bool isSliding;
    [SerializeField] private float maxSlideDuration;
    private float currentSlideDuration;
    [SerializeField] private BoxCollider2D regularCollider;
    [SerializeField] private BoxCollider2D slideCollider;


    [SerializeField] private float runSpeed;

    [SerializeField] private Text Score;
    [SerializeField] private float Boost;
    [SerializeField] private int BoostDistance;
    private bool ShouldBoost;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerCameraDistance = new Vector3(transform.position.x - mainCamera.transform.position.x, 0, 0);

        canJump = false;
        isJumping = false;

        isSliding = false;

        ShouldBoost = true;

        Time.timeScale = 0f;
        StartCoroutine(waitForStart());
    }

    // Update is called once per frame
    void Update()
    {

        if (canJump && !isSliding && Input.GetKeyDown(KeyCode.Space))
        {
            StartJump();
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartSlide();
        } else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopSlide();
        }

        DoSlideFrame();

        CheckForBoost();
        MoveCamera();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(runSpeed,rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.name == "Death")
        {
            deathScreen.SetActive(!deathScreen.activeSelf);
            Time.timeScale = 0f;
        }
        canJump = true;
        isJumping = false;
        GetComponent<Animator>().SetBool("isJumping", false);
    }

    private void StartJump()
    {
        canJump = false;
        isJumping = true;
        animator.SetBool("isJumping", true);
        rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
    private void StopJump()
    {
        if (rigidBody.velocity.y > 0)
        {
            rigidBody.AddForce(Vector2.up * (-rigidBody.velocity.y), ForceMode2D.Impulse);
        }
    }

    private void StartSlide()
    {
        if (isJumping)
        {
            rigidBody.AddForce(Vector2.up * (-jumpSpeed), ForceMode2D.Impulse);
        }
        isSliding = true;
        currentSlideDuration = 0;
    }
    private void DoSlideFrame()
    {
        if(!isJumping && isSliding) 
        {
            regularCollider.enabled = false;
            slideCollider.enabled = true;

            animator.SetBool("isSliding", true);
            currentSlideDuration += Time.deltaTime;
            if(currentSlideDuration >= maxSlideDuration)
            {
                StopSlide();
            }
        }
    }
    private void StopSlide()
    {
        isSliding = false;
        animator.SetBool("isSliding", false);
        regularCollider.enabled = true;
        slideCollider.enabled = false;
    }

    private void CheckForBoost()
    {
        int scr = Int32.Parse(Score.text);
        if (ShouldBoost && scr != 0 && scr % BoostDistance == 0)
        {
            runSpeed += Boost;
            rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);
            animator.SetFloat("Speed", runSpeed);
            ShouldBoost = false;
        }
        else if (scr % BoostDistance == 1)
        {
            ShouldBoost = true;
        }
    }

    private void MoveCamera()
    {
        mainCamera.transform.position =
                new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z) - playerCameraDistance;
    }


    private IEnumerator waitForStart()
    {
        float tmp = runSpeed;
        runSpeed = 0;
        while (!Input.GetKeyDown(KeyCode.S))
        {
            yield return null;
        }

        runSpeed = tmp;
        canJump = true;
        rigidBody.velocity = new Vector2(runSpeed, rigidBody.velocity.y);
        animator.SetFloat("Speed", runSpeed);
        Time.timeScale = 1f;
    }
}
