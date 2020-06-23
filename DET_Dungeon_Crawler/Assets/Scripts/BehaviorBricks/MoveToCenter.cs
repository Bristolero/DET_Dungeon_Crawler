using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using System.Collections;
using System.Collections.Generic;
 
[Action("MyActions/MoveToCenter")]
[Help("Bewegt den Boss zum Spieler")]

//Boss in Richtung Spieler
public class MoveToCenter : BasePrimitiveAction
{
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;

    [InParam("moveSpeed")]
    [Help("Movement speed of the boss")]
    public float moveSpeed;

    private Transform boss;
    private Vector3 target;
    private Rigidbody2D rb; 
    private float h;
    private float v;
    private Vector3 bossEulerAngles;
    private float distance = 0.1f;

    //Für Animationen
    private Animator m_Animator;
    private bool m_Move;
    

    

    public override void OnStart()
    {
        Debug.Log("MoveToCenter startet");
        boss = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Hard-coded auf Mitte des Raumes
        Vector3 target = new Vector3 (0,0,0);
        m_Animator = gameObject.GetComponent<Animator>();
        m_Move = false;
	}

    private void Move() {	         
        Vector3 direction = target - boss.position;
        Debug.Log(boss.position.x + " " + boss.position.y);
        direction.Normalize();
        m_Move = true;
        if(target.x > boss.position.x)
            {
                h = 1;
		    }
            else if(target.x == boss.position.x)
            {
                h = 0;
		    }
            else
            {
                h = -1;
		    }
            
            rb.velocity = new Vector3(direction.x * moveSpeed, direction.y * moveSpeed);

            if (h <= 0)
            {
                boss.eulerAngles = new Vector3(0, 180, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
            //wenn der Boss sich nach rechts bewegt, sein Kopf bleibt nach rechts 
            if (h > 0)
            {
                boss.eulerAngles = new Vector3(0, 0, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
            Debug.Log(Vector3.Distance(gameObject.transform.position, target));
    }

    private void Stop() {
        rb.velocity = new Vector3(0, 0);
        m_Move = false;       
	}



    public void Update()
    {
        
	}

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {     
        if(gameObject == null)
        {
            return TaskStatus.FAILED;  
		}
        if(Vector3.Distance(gameObject.transform.position, target) > 0.1f) {
            Move();           
            if (m_Move == true) {
                m_Animator.SetTrigger("Move");   
			}
            return TaskStatus.RUNNING;
        }
        else {
            Stop();            
            Debug.Log("MoveToCenter beendet");
            m_Animator.ResetTrigger("Move");
            return TaskStatus.COMPLETED;  
		}
        
    } // OnUpdate
    
}
