using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Takes care of the player
 */
public class Player : MonoBehaviour {

    public float movementSpeed = 2f;
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool facingLeft, facingRight, touchingGround, jumping, falling, canMove;

    // Use this for initialization
    void Awake ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canMove = false;
        facingRight = true;
        facingLeft = false;
        touchingGround = false;
        jumping = false;
        falling = false;
	}
	
    void Update()
    {
        UpdateAnimations();
    }

	// Update is called once per frame
	void FixedUpdate () {

        if(!canMove)
        {
            return;
        }

        var rbVelocity = rigidbody2D.velocity;

        // Takes care of horizontal player movement
        rbVelocity.x = Input.GetAxisRaw("Horizontal") * movementSpeed;

        // Code for jumping
        if (Input.GetAxisRaw("Vertical") != 0 && touchingGround)
        {
            rbVelocity.y = Input.GetAxisRaw("Vertical") * 20F;
            touchingGround = false;
        }

        rigidbody2D.velocity = rbVelocity;

        if (rigidbody2D.velocity.y < 0)
        {
            falling = true;
            jumping = false;
        }
        else if (rigidbody2D.velocity.y > 0)
        {
            jumping = true;
            falling = false;
        }
        else
        {
            jumping = false;
            falling = false;
        }

    }

    void UpdateAnimations()
    {
        if (!canMove)
        {
            return;
        }

        if (touchingGround)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                animator.SetBool("running", false);
            }
            else
            {
                animator.SetBool("running", true);
            }
        }
        else
        {
            if (jumping)
            {
                animator.SetBool("jumping", true);
                animator.SetBool("falling", false);
            }
            if (falling)
            {
                animator.SetBool("falling", true);
                animator.SetBool("jumping", false);
            }
        }

        // Flips character in direction it is moving
        if (Input.GetAxisRaw("Horizontal") == 0)
        {

        }
        else if (Input.GetAxisRaw("Horizontal") == 1) // Character is moving right
        {
            if (!facingRight)
            {
                spriteRenderer.flipX = false;
                facingRight = true;
                facingLeft = false;
            }
        }
        else // Character is moving left
        {
            if (!facingLeft)
            {
                spriteRenderer.flipX = true;
                facingLeft = true;
                facingRight = false;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            touchingGround = true; 
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    // Handles the player death here
    public void Death()
    {
        Destroy(gameObject);
    }
}
