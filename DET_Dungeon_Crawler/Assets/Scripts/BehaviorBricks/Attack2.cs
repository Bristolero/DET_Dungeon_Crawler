using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
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

        private Transform attack2Point;
        
        private float timer;
        private float wait;

        //Für Animationen
        private Animator m_Animator;
        private bool m_Attack2;
     
        public override void OnStart()
        {             
              Debug.Log("Attack2 startet");
              attack2Point = gameObject.transform.Find("Attack2Pos");
              timer = 0;
              wait = 0.5f;
              m_Animator = gameObject.GetComponent<Animator>();
              m_Attack2 = true;
		}

        public void Attack()
        {
            m_Animator.SetBool("doesAttack2", true);         
            GameObject newBossSlash = GameObject.Instantiate(bossSlash, attack2Point.position, attack2Point.rotation) as GameObject;  
            m_Animator.SetBool("doesAttack2", false);                       
        }
		

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {   
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
                    Debug.Log("Attack2 beendet");   
                    return TaskStatus.COMPLETED;
                }
			}
            return TaskStatus.RUNNING;
        } // OnUpdate
 
    } // class Attack2