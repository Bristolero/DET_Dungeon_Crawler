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
    
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;

    public override void OnStart()
    {
          Debug.Log("Taunt startet");
          m_Animator = gameObject.GetComponent<Animator>();
          m_Taunt = false;
	}

    private void Taunting()
    {
        m_Taunt = true;
        if(m_Taunt == true)
        {
            m_Animator.SetTrigger("Taunt");
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