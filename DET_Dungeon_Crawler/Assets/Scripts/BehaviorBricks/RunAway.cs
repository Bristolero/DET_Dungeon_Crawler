using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/RunAway")]
    [Help("Führt die erste Attacke vom Boss aus")]

    //Boss benutzt die erschöpft sein Animation, wartet 3 Sekunden und sich um x hp heilt, falls der Spieler nicht zu nahe kommt
    public class RunAway : BasePrimitiveAction
    {

        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {             
            return TaskStatus.COMPLETED; 
        } // OnUpdate
 
    } // class RunAway