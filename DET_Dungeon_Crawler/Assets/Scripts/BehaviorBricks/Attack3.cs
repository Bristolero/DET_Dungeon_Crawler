using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
 
[Action("MyActions/Attack3")]
[Help("Führt die dritte Attacke vom Boss aus")]
    public class Attack3 : BasePrimitiveAction
    {          
        // Define the input parameter "bossSlash" (the prefab to be cloned).
        [InParam("bossSlash")]
        [Help("Slash Prefab welches instantiiert werden soll")]
        public GameObject bossSlash;

        [InParam("GameObject")]
        [Help("The gameObject that will be moved, in this case the boss")]
        public GameObject gameObject;

        private Transform attack3Point1;
        private Transform attack3Point2;
        private Transform attack3Point3;
        private Transform attack3Point4;
        private Transform attack3Point5;
        private Transform attack3Point6;
        private Transform attack3Point7;
        private Transform attack3Point8;
        
        private float timer;
        private float wait;

        //Für Animationen
        private Animator m_Animator;
        private bool m_Attack3;
     
        public override void OnStart()
        {             
              attack3Point1 = gameObject.transform.Find("Attack3Pos1");
              attack3Point2 = gameObject.transform.Find("Attack3Pos2");
              attack3Point3 = gameObject.transform.Find("Attack3Pos3");
              attack3Point4 = gameObject.transform.Find("Attack3Pos4");
              attack3Point5 = gameObject.transform.Find("Attack3Pos5");
              attack3Point6 = gameObject.transform.Find("Attack3Pos6");
              attack3Point7 = gameObject.transform.Find("Attack3Pos7");
              attack3Point8 = gameObject.transform.Find("Attack3Pos8");
              timer = 0;
              wait = 0.5f;
              m_Animator = gameObject.GetComponent<Animator>();
              m_Attack3 = true;
		}

        public TaskStatus Attack()
        {
            m_Animator.SetBool("doesAttack3", true);
            if(timer < wait)
            {
                timer = timer + Time.deltaTime;    
                return TaskStatus.RUNNING;
		    }
            else {
                GameObject newBossSlash = GameObject.Instantiate(bossSlash, attack3Point1.position, attack3Point1.rotation) as GameObject;  
                m_Animator.SetBool("doesAttack3", false);
                m_Attack3 = false;
                return TaskStatus.COMPLETED;
            }
        }
		

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {   
            if(gameObject == null)
            {
                return TaskStatus.FAILED;     
			}
            if(m_Attack3 == true)
            {
                Attack();              
			}
            return TaskStatus.RUNNING;
        } // OnUpdate
 
    } // class Attack2