﻿using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using System.Collections;
using System.Collections.Generic;
 
[Action("MyActions/MoveToPlayer")]
[Help("Bewegt den Boss zum Spieler")]

//Boss in Richtung Spieler
public class MoveToPlayer : BasePrimitiveAction
{
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;

    [InParam("moveSpeed")]
    [Help("Movement speed of the boss")]
    public float moveSpeed;

    private Transform targetTransform;
    private Transform bossTransform;
    private Rigidbody2D rb; 
    private float h;
    private float v;
    private Vector3 bossEulerAngles;
    private float distance = 6.5f;

    //Für Animationen
    private Animator m_Animator;
    private bool m_Move;


    public override void OnStart()
    {
        Debug.Log("MoveToPlayer startet");
        bossTransform = gameObject.transform;
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        rb = gameObject.GetComponent<Rigidbody2D>();

        m_Animator = gameObject.GetComponent<Animator>();
        m_Move = false;
	}

    private void Chase() {	    
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        bossTransform = gameObject.transform;
        m_Move = true;
        if(calculateDistance(targetTransform, bossTransform)) {
            if(targetTransform.position.x > bossTransform.position.x)
            {
                h = 1;
		    }
            else if(targetTransform.position.x < bossTransform.position.x)
            {
                h = -1;
		    }
            else
            {
                h = 0;
		    }

            if(targetTransform.position.y > bossTransform.position.y)
            {
                v = 1;
		    }
            else if(targetTransform.position.y < bossTransform.position.y)
            {
                v = -1;
		    }
            else
            {
                v = 0;
		    }
        
            rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
            if (h <= 0)
            {
                bossTransform.eulerAngles = new Vector3(0, 180, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
            //wenn der Boss sich nach rechts bewegt, sein Kopf bleibt nach rechts 
            if (h > 0)
            {
                bossTransform.eulerAngles = new Vector3(0, 0, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else Stop();
    }

    private void Stop() {
        rb.velocity = new Vector3(0, 0);
        m_Move = false;
	}

    private bool calculateDistance(Transform t1, Transform t2)
    {
        return ((t1.position.x-t2.position.x)*(t1.position.x-t2.position.x)+(t1.position.y-t2.position.y)*(t1.position.y-t2.position.y)) < distance*distance;
	}


    public void Update()
    {
        
	}

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {     
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        bossTransform = gameObject.transform;
        if(gameObject == null)
        {
            return TaskStatus.FAILED;
		}
        if (Vector3.Distance(targetTransform.position, bossTransform.position) > distance){
            if (m_Move == true) {
                m_Animator.SetBool("isMoving", true);   
			}
            Chase();                   
        }        
        else {
            Stop();
            m_Animator.SetBool("isMoving", false);
            Debug.Log("MoveToPlayer beendet");
            return TaskStatus.COMPLETED;
         }
        return TaskStatus.RUNNING;
        
    } // OnUpdate
 
} // class MoveToPlayer
//if (Vector3.Distance(targetTransform.position, bossTransform.position) > distance)