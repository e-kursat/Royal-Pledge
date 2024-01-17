using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator enemyAnimator;

    public float speed;
    
    // first point
    public GameObject pointA;
    // second point
    public GameObject pointB;

    private Transform currentPoint;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentPoint = pointB.transform;
        enemyAnimator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        // set direction (like + or -)
        Vector2 point = currentPoint.position - transform.position;
        
        print("point: " + pointB.transform.position);
        print("currentPoint.position: " + currentPoint.position);
        print("transform: " + transform.position);
        
        print("************************************************");
        //print("point: " + point);
        
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        print("distance: " + Vector2.Distance(transform.position, currentPoint.position));
        
        print("************************************************");

        
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            print("A");
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            print("B");
            currentPoint = pointB.transform;
        }
    }
}
