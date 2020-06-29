using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
using Pada1.BBCore.Tasks;  


[Condition("MyConditions/IsBossLow")]
[Help("Returns true, falls der Boss weniger als 1000 Lebenspunkte hat")]
public class IsBossLow : ConditionBase
{
    
    private Boss boss;
   
    public override bool Check()
    {
        GameObject BossGO = GameObject.FindGameObjectWithTag("Boss");
        if (BossGO != null)
        {
            boss = BossGO.GetComponent<Boss>();
            if (boss != null)
                Debug.Log("Ist der Boss low HP? " + boss.isLow );
                Debug.Log("Boss HP: " + boss.hp);
                return boss.isLow;
        }
        return false;
    }
   
} //class IsBossLow
