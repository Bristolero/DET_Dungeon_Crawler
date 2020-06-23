    using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/RunAway")]
    [Help("Führt die erste Attacke vom Boss aus")]
//Boss in Richtung Spieler
public class RunAway : BasePrimitiveAction
{
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;

    [InParam("moveSpeed")]
    [Help("Movement speed of the boss")]
    public float moveSpeed;

    private Transform target;
    private Transform boss;
    private Rigidbody2D rb;
    private float h;
    private float v;
    private Vector3 bossEulerAngles;
    private float distance = 2f;

    private Vector2 movement;

    //Für Animationen
    private Animator m_Animator;
    private bool m_Run;


    public override void OnStart()
    {
        Debug.Log("RunAway startet");
        boss = gameObject.transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_Run = false;
    }

    private void Chase()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = gameObject.transform;
        m_Run = true;
        Vector3 direction = target.position - boss.position;
        direction.Normalize();
        if (Vector3.Distance(target.position, boss.position) < distance)
        {
            if (target.position.x > boss.position.x)
            {
                h = -1;
            }
            else if (target.position.x < boss.position.x)
            {
                h = 1;
            }
            else
            {
                h = 0;
            }
            if (target.position.y > boss.position.y)
            {
                v = -1;
            }
            else if (target.position.y < boss.position.y)
            {
                v = 1;
            }
            else
            {
                v = 0;
            }
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

            if (h<= 0)
            {
                boss.eulerAngles = new Vector3(0, 0, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
            //wenn der Boss sich nach rechts bewegt, sein Kopf bleibt nach rechts 
            if (h > 0)
            {
                boss.eulerAngles = new Vector3(0, 180, 0);
                bossEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else Stop();
    }

    private void Stop()
    {
        rb.velocity = new Vector3(0, 0);
        m_Run = false;
    }

    public void Update()
    {

    }

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = gameObject.transform;
        Debug.Log(Vector3.Distance(target.position, boss.position));
        if (gameObject == null)
        {
            return TaskStatus.FAILED;
        }
        if (Vector3.Distance(target.position, boss.position) < distance)
        {
            if (m_Run== true)
            {
                m_Animator.SetBool("isMoving", true);
            }
            Chase();
        }
        else
        {
            Stop();
            m_Animator.SetBool("isMoving", false);
            Debug.Log("RunAway beendet");
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.RUNNING;

    } // OnUpdate
      //Boss benutzt die erschöpft sein Animation, wartet 3 Sekunden und sich um x hp heilt, falls der Spieler nicht zu nahe kom
  } // class RunAway