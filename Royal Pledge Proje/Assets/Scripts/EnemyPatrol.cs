using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    
    [Header ("Movement")]
    [SerializeField] private float speed;
    
    [Header ("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private Vector3 initScale;
    private bool movingLeft;

    private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        
        // get enemy scale in start
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        enemyAnimator.SetBool("isWalking", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // is move left
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        
        // is move right
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        enemyAnimator.SetBool("isWalking", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveDirection(int direction)
    {
        idleTimer = 0;
        
        enemyAnimator.SetBool("isWalking", true);
        
        // flip face
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        
        // move enemy
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
