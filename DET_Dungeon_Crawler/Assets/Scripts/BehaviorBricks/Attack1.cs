using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/Attack1")]
    [Help("Führt die erste Attacke vom Boss aus")]
    public class Attack1 : BasePrimitiveAction
    {
        // Define the input parameter "shootPoint".
        [InParam("attackPoint")]
        public Transform attackPoint;
 
        // Define the input parameter "bullet" (the prefab to be cloned).
        [InParam("bullet")]
        public GameObject bullet;
 
        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {             
            return TaskStatus.COMPLETED; 
        } // OnUpdate
 
    } // class Attack1