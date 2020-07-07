using Pada1.BBCore.Framework; // BasePrimitiveAction
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus

using UnityEngine;
 
[Action("MyActions/RunAway2")]
[Help("Lässt den Boss für eine Sekunde weglaufen")]

//Boss in Richtung Spieler
public class RunAway2 : BasePrimitiveAction
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
    private float timer = 0;
    private float finalTimer = 2f;

    private Vector2 movement;

    //Für Animationen
    private Animator m_Animator;
    private bool m_Move;


    public override void OnStart()
    {
        Debug.Log("RunAway2 startet");
        boss = gameObject.transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        rb = gameObject.GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_Move = false;
	}

    private void Leave() {	    
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        boss = gameObject.transform;
        m_Move = true;
        Vector3 direction = - target.position + boss.position;
        direction.Normalize();
 
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

        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

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
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        boss = gameObject.transform;
        //Debug.Log(Vector3.Distance(target.position, boss.position));
        if(gameObject == null)
        {
            return TaskStatus.FAILED;
		}

        if (m_Move == true) {
            m_Animator.SetTrigger("Move");  
			}
        while(timer < finalTimer) {
            Leave();
            timer += Time.deltaTime;
            return TaskStatus.RUNNING;
            }
        Stop();
        m_Animator.ResetTrigger("Move");
        Debug.Log("MoveToPlayer beendet");
        return TaskStatus.COMPLETED;
       
    } // OnUpdate
 
}