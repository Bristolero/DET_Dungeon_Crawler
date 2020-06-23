using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 


    //In Behaviorbricks klicke auf den Node (Attack2) und setze die Parameter auf Blackboard
    [Action("MyActions/Attack2")]
    [Help("Führt die erste Attacke vom Boss aus")]
    public class Attack2 : BasePrimitiveAction
    {          
        // Define the input parameter "bossSlash" (the prefab to be cloned).

        [InParam("bossSlash")]
        public GameObject bossSlash;

        [InParam("GameObject")]
        [Help("The gameObject that will be moved, in this case the boss")]
        public GameObject gameObject;

        //Ort an dem der Slash Prefab erstellt wird
        private Transform attack2Point;
        
        //Attacke soll nach x Sekunden ausgeführt werden
        private float timer = 0;
        private float wait = 0.4f;

        //Für Animationen
        private Animator m_Animator;
        private bool m_Attack2;
        

        public override void OnStart()
        {             
              Debug.Log("Attack2 startet");       
              attack2Point = gameObject.transform.Find("Attack2Pos");
              m_Animator = gameObject.GetComponent<Animator>();
              m_Attack2 = true;
              m_Animator.SetTrigger("Attack2");
		}

       
        public void Attack()
        {        
            GameObject newBossSlash = GameObject.Instantiate(bossSlash, attack2Point.position, attack2Point.rotation) as GameObject;                                  
        }
		

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {   
            //Ist der Boss tot/kein GameObject mehr vorhanden dann FAIL
            if(gameObject == null)
            {
                return TaskStatus.FAILED;     
			}                     
            if(m_Attack2 == true)
            {
                if(timer < wait)
                {
                timer = timer + Time.deltaTime;                  
		        }
                else {
                    Attack();        
                    m_Attack2 = false;                  
                    m_Animator.ResetTrigger("Attack2");
                    Debug.Log("Attack2 beendet");   
                    return TaskStatus.COMPLETED;
                }
			}
            return TaskStatus.RUNNING;
        } // OnUpdate
 
    } // class Attack2