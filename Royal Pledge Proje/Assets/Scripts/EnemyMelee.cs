using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private float range;
    [SerializeField] private int damage = 20;
    
    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header ("Layers")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    
    private Animator enemyAnimator;
    
    private PlayerManager currentPlayerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (PlayerIsSide())
        {
            // only attack to player when side
            if (coolDownTimer >= attackCooldown)
            {
                // Attack
                coolDownTimer = 0;
                
                enemyAnimator.SetTrigger("Attack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerIsSide();
        }

    }

    private void DamagePlayer()
    {
        // if player still in range
        if (PlayerIsSide())
        {
            // Damage
            currentPlayerHealth.TakeDamage(damage);
        }
    }

    private bool PlayerIsSide()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            currentPlayerHealth = hit.transform.GetComponent<PlayerManager>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance), 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
