using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/Attack1")]
    [Help("Führt die erste Attacke vom Boss aus")]
    public class Attack1 : BasePrimitiveAction
    {
        [InParam("GameObject")]
        [Help("The gameObject that will be moved, in this case the boss")]
        public GameObject gameObject;

        //Ort an dem der Slash Prefab erstellt wird
        private Transform attack1Point;

        //Attacke soll nach 0.5 Sekunden ausgeführt werden
        private float timer = 0;
        private float wait = 0.5f;

        //Für Animationen
        private Animator m_Animator;
        private bool m_Attack1;
        // Define the input parameter "bossSlash" (the prefab to be cloned).
        [InParam("bossSlash1")]
        public GameObject bossSlash1;

       // private AudioManager audioM;

    public override void OnStart()
    {
        Debug.Log("Attack1 startet");
        attack1Point = gameObject.transform.Find("Attack1Pos");
        m_Animator = gameObject.GetComponent<Animator>();
        m_Attack1 = true;
        m_Animator.SetTrigger("Attack1");
       // audioM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();   
       // audioM.Play("BossAttack");
    }


    public void Attack()
    {
        GameObject newBossSlash = GameObject.Instantiate(bossSlash1, attack1Point.position, attack1Point.rotation) as GameObject;
        setLayerToDefault(newBossSlash);
    }

    private void setLayerToDefault(GameObject g)
    {
        SpriteRenderer sprite = g.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = -10;
        sprite.sortingLayerName = "Default";
    }

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        //Ist der Boss tot/kein GameObject mehr vorhanden dann FAIL
        if (gameObject == null)
        {
            return TaskStatus.FAILED;
        }
        if (m_Attack1 == true)
        {
            if (timer < wait)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                Attack();
                m_Attack1 = false;
                Debug.Log("Attack1 beendet");
                m_Animator.ResetTrigger("Attack1");
                return TaskStatus.COMPLETED;
            }
        }
        return TaskStatus.RUNNING;
    } // OnUpdate
} // class Attack1