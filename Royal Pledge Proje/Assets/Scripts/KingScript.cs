using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimator;
    
    public float speed = 5f;
    
    // player jump force
    public float jumpForce = 12.2f;
    public int jumpLimit = 2;
    private int jumpCounter;
    
    private bool facingRight = true;
    
    // player is ground check variables
    public Transform groundCheck;
    public float groundCheckRange = 0.2f;
    public LayerMask groundLayer;
    public bool isGround;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // set isGround
        CheckGround();
        
        // player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        playerAnimator.SetBool("isGround", isGround);
    }
    
    private void FixedUpdate()
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
        
        // play run animation
        playerAnimator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        
        // player face direction
        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }
    }
    
    private void Jump()
    {
        if (isGround)
        {
            jumpCounter = 0;
            
            // player is jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpCounter += 1;
        }
        else
        {
            if (jumpCounter < jumpLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCounter += 1;
            }
        }
    }
    
    private void CheckGround()
    {
        // detect grounds
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
    }
    
    private void OnDrawGizmosSelected()
    {
        // draw points
        
        if (groundCheck == null)
            return;
        
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRange);
    }
}
