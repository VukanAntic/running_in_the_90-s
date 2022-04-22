using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;

    // public Animator anim;

    // our player has 2 box colliders, so we need 2 values 
    [SerializeField] private BoxCollider2D regularCollider1;
    [SerializeField] private BoxCollider2D regularCollider2;
    [SerializeField] private BoxCollider2D slideCollider;

    [SerializeField] private float slideSpeed = 5.0F;
    public bool isSliding = false;

    private bool alreadyPressedSlide = false;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMovement.playerStartedMoving)
        {

            animator.SetBool("isSliding", isSliding);
            // izmeni da bude sa "Ctrl"
            if (Input.GetKeyDown(KeyCode.LeftControl) && !alreadyPressedSlide && playerMovement.isGrounded)
            {
                alreadyPressedSlide = true;
                preformSlide();
            }
        
        }
    }

    private void preformSlide() 
    {
        isSliding = true;

        // still dont have animations, once we do, we will uncomment these
        // anim.SetBool("isSliding", true);

        regularCollider1.enabled = false;
        regularCollider2.enabled = false;
        slideCollider.enabled = true;


        rigidBody.AddForce(Vector2.right * slideSpeed);

        // we dont need both, will change this
        /*if (playerMovement.sprite.flipX)
        {
            rigidbody.AddForce(Vector2.left * slideSpeed);
        }
        else 
        {
            
        }*/

        StartCoroutine("stopSlide");
    
    }

    IEnumerator stopSlide() 
    {
        yield return new WaitForSeconds(0.4f);
        // anim.Play("Idle");
        // anim.SetBool("IsSlid", false);
        regularCollider1.enabled = true;
        regularCollider2.enabled = true;
        slideCollider.enabled = false;
        isSliding = false;
        alreadyPressedSlide = false;
    }
}
