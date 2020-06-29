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
        
        private float timer = 0;
        private float wait = 0.2f;

        private float testtimer;

        //Für Animationen
        private Animator m_Animator;
        private bool m_Attack3;
     
        public override void OnStart()
        {             
              Debug.Log("Attack3 startet"); 
              attack3Point1 = gameObject.transform.Find("Attack3Pos1");
              attack3Point2 = gameObject.transform.Find("Attack3Pos2");
              attack3Point3 = gameObject.transform.Find("Attack3Pos3");
              m_Animator = gameObject.GetComponent<Animator>();
              m_Attack3 = true;
              m_Animator.SetTrigger("Attack3");
		}

        private void setLayerToDefault(GameObject g)
        {
            SpriteRenderer sprite = g.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = -10;
            sprite.sortingLayerName = "Default";
		}

        public void Attack()
        {           
            GameObject newBossSlash1 = GameObject.Instantiate(bossSlash, attack3Point1.position, attack3Point1.rotation) as GameObject; 
            setLayerToDefault(newBossSlash1);
            GameObject newBossSlash2 = GameObject.Instantiate(bossSlash, attack3Point2.position, attack3Point2.rotation) as GameObject;
            setLayerToDefault(newBossSlash2);
            GameObject newBossSlash3 = GameObject.Instantiate(bossSlash, attack3Point3.position, attack3Point3.rotation) as GameObject;    
            setLayerToDefault(newBossSlash3);
        }
		

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {   
            //Ist der Boss tot/kein GameObject mehr vorhanden dann FAIL
            if(gameObject == null)
            {
                return TaskStatus.FAILED;     
			}
            if(m_Attack3 == true)
            {
                if(timer < wait)
                {
                timer = timer + Time.deltaTime;                 
		        }
                else {
                    Attack();        
                    m_Attack3 = false;                   
                    Debug.Log("Attack3 beendet");  
                    m_Animator.ResetTrigger("Attack3");
                    return TaskStatus.COMPLETED;
                }
			}
            return TaskStatus.RUNNING;
        } // OnUpdate
 
    } // class Attack2