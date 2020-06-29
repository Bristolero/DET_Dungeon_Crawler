using UnityEngine;
using System;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
 
[Action("MyActions/Taunt")]
[Help("Der Boss Taunted bevor er einen starken Angriff macht")]

//Boss benutzt die erschöpft sein Animation, wartet 3 Sekunden und sich um x hp heilt, falls der Spieler nicht zu nahe kommt
public class Taunt : BasePrimitiveAction
{
    private float timer = 0;
    private float maxTime = 3;
    private Animator m_Animator;
    private bool m_Taunt;
    private Transform target;
    private Vector3 bossEulerAngles;
    private Transform boss;

    private float h;
    
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;
   
    public override void OnStart()
    {
          Debug.Log("Taunt startet");
          boss = gameObject.transform;
          m_Animator = gameObject.GetComponent<Animator>();
          m_Taunt = false;
          target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
	}

    private void Taunting()
    {
        m_Taunt = true;
        if(m_Taunt == true)
        {
            m_Animator.SetTrigger("Taunt");
	    }
        if(target.position.x > boss.position.x)
        {
            h = 1;
		}
        else if(target.position.x < boss.position.x)
        {
            h = -1;
		}
        else
        {
                h = 0;
	    }        
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
	}

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {             
        if(gameObject == null)
        {
            return TaskStatus.FAILED;     
	    }
        if(timer < maxTime) 
        {
            if(m_Taunt == false) {
                Taunting();
            }
            timer = timer + Time.deltaTime;
            return TaskStatus.RUNNING;
		}
        else { 
            m_Animator.ResetTrigger("Taunt");
            Debug.Log("Taunt beendet");
            return TaskStatus.COMPLETED; 
        }
    } // OnUpdate
 
} // class Taunt