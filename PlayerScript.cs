using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5.3f;
    
    // private float minX = -7.70f;
    // private float maxX = 8.70f;

    private Rigidbody2D rb;
    private Animator playerAnimator;
    
    private bool isGrounded;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void MovePlayer()
    {
        // player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        
        playerAnimator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        // // player jumping
        // if (Input.GetButtonDown("Jump"))
        // {
        //     playerAnimator.SetBool("isJump", true);
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }

        // player face direction
        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }
    }
}