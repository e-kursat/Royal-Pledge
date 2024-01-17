using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimator;

    [Header("Movement")]
    // player run speed
    public float speed = 5f;
    
    [Space(20)]
    
    // player jump force
    public float jumpForce = 12.2f;
    public int jumpLimit = 2;
    private int jumpCounter;
    
    private bool facingRight = true;
    
    // attack variables
    public int attackDamage = 50;
    public Transform attackPoint;
    public float attackRange = 0.9f;
    private float nextAttackTime = 0f;
    // attack per second
    public float attackRate = 2f;
    public LayerMask enemyLayers;
    
    // player is ground check variables
    public Transform groundCheck;
    public float groundCheckRange = 0.2f;
    public LayerMask groundLayer;
    public bool isGround;

    // crouch variables
    public Transform ceilPoint;
    private bool isCeil;
    public bool isCrouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     playerAnimator = GetComponent<Animator>();
    // }

    private void Update()
    {
        // set isGround
        CheckGround();
        
        // MovePlayer();
        
        // player jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // print("Time: " + Time.time);
        if (Time.time >= nextAttackTime)
        {
            // player do attack
            if (Input.GetKeyDown(KeyCode.F) && !isCrouching)
            {
                //print("-----------------Time: " + Time.time);
                Attack();
                
                nextAttackTime = Time.time + 1f / attackRate;
                //print("Next Attack Time: " + nextAttackTime);
            }
        }

        playerAnimator.SetBool("isGround", isGround);
    }

    private void FixedUpdate()
    {
        MovePlayer();

        // set isCeil
        CheckCeil();
        
        // hold up down arrow and crouch
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Crouch();
        }
        
        // if hold up down arrow
        else
        {
            // if have a ceil
            if (isCeil)
            {
                Crouch();
            }
            
            // if not have a ceil
            else
            {
                isCrouching = false;
            }
        }
        
        playerAnimator.SetBool("isCrouching", isCrouching);
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

    private void Crouch()
    {
        if (isGround)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    private void Attack()
    {
        // play attack animation
        playerAnimator.SetTrigger("Attack");

        // // detect enemies
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //
        // // Damage enemies
        // foreach (Collider2D Enemy in hitEnemies)
        // {
        //     // print("Hit " + Enemy.name);
        //    Enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        // }
    }

    private void GiveDamage()
    {
       // detect enemies
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

       // Damage enemies
       foreach (Collider2D Enemy in hitEnemies)
       { 
           print("Hit " + Enemy.name);
           Enemy.GetComponent<EnemyManager>().TakeDamage(attackDamage);
       }  
    }

    private void CheckGround()
    {
        // detect grounds
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
    }
    
    private void CheckCeil()
    {
        // detect ceil
        isCeil = Physics2D.OverlapCircle(ceilPoint.position, groundCheckRange, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // draw points
        
        if (attackPoint == null || groundCheck == null || ceilPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRange);
        
        Gizmos.DrawWireSphere(ceilPoint.position, groundCheckRange);
    }
}