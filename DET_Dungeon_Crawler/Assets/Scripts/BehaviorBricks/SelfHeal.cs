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


    public override void OnStart()
    {
        m_heal = true;
        hp=gameObject.GetComponent<Boss>().hp;
    }
    private void heal()
    {
        if (timeVal < 6f)
        {
            hp = hp + 100;
            timeVal += Time.deltaTime;
        }
        
        m_heal = false;
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
            heal();
        }
        return TaskStatus.COMPLETED;
    } // OnUpdate
 
    } // class SelfHeal