using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/SelfHeal")]
    [Help("Führt die erste Attacke vom Boss aus")]

    //Boss benutzt die erschöpft sein Animation, wartet 3 Sekunden und sich um x hp heilt, falls der Spieler nicht zu nahe kommt
    public class SelfHeal : BasePrimitiveAction
{
   
    [InParam("GameObject")]
    [Help("The gameObject that will be moved, in this case the boss")]
    public GameObject gameObject;

    [Help("hp of the boss")]
    private int hp;
    private bool m_heal;
    private float timeVal = 0;
    private int hpTotal;

    private Animator m_Animator;


    public override void OnStart()
    {
        Debug.Log("heal startet");
        m_heal = true;
        hp = gameObject.GetComponent<Boss>().hp;
        hpTotal = gameObject.GetComponent<Boss>().hpTotal;
        m_Animator = gameObject.GetComponent<Animator>();

    }
    private void heal()
    {
        if(hp < hpTotal && hp > hpTotal - 100)
        {
            if (timeVal < 6f)
            {
                hp = hpTotal ;
                timeVal += Time.deltaTime;
                gameObject.SendMessage("Sethp", hp, SendMessageOptions.DontRequireReceiver);
            }
        }
        else if(hp < hpTotal - 100)
        {
            if (timeVal < 6f)
            {
                hp += 100;
                timeVal += Time.deltaTime;
                gameObject.SendMessage("Sethp", hp, SendMessageOptions.DontRequireReceiver);
            }
        }
        
        
        m_heal = false;
        Debug.Log("heal end");
    }
    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        if (gameObject == null)
        {
            return TaskStatus.FAILED;
        }
        if (m_heal == true)
        {
            m_Animator.SetTrigger("Taunt");
            heal();
        }
        if(m_heal == false)
        {
            m_Animator.ResetTrigger("Taunt");
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.RUNNING;
    } // OnUpdate
 
    } // class SelfHeal