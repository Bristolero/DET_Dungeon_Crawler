using UnityEngine;
 
    using Pada1.BBCore;           // Code attributes
    using Pada1.BBCore.Tasks;     // TaskStatus
    using Pada1.BBCore.Framework; // BasePrimitiveAction
 
    [Action("MyActions/Attack1")]
    [Help("Führt die erste Attacke vom Boss aus")]
    public class Attack1 : BasePrimitiveAction
    {
        // Define the input parameter "attack1Point".
        [InParam("attack1Point")]
        public Transform attack1Point;
 
        // Define the input parameter "bossSlash" (the prefab to be cloned).
        [InParam("bossSlash")]
        public GameObject bossSlash;
 
        // Main class method, invoked by the execution engine.
        public override TaskStatus OnUpdate()
        {
        // Instantiate the bullet prefab.
        GameObject newbossSlash = GameObject.Instantiate(
                                    bossSlash, attack1Point.position,
                                    attack1Point.rotation * bossSlash .transform.rotation
                                ) as GameObject;
        // Give it a velocity
        if (bossSlash.GetComponent<Rigidbody>() == null)
            // Safeguard test, although the rigid body should be provided by the
            // prefab to set its weight.
            bossSlash.AddComponent<Rigidbody>();
        // The action is completed. We must inform the execution engine.
        return TaskStatus.COMPLETED;

       } // OnUpdate     
    } // class Attack1