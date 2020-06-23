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

    [InParam("hp")]
    [Help("hp of the boss")]
    public int hp;
    private bool m_heal;


        public override void OnStart()
    {
        m_heal = true;
    }
    private void heal()
    {
        hp = hp + 10;
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
        return TaskStatus.RUNNING;
    } // OnUpdate
 
    } // class SelfHeal