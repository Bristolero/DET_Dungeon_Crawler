using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase
<<<<<<< HEAD
=======
using Pada1.BBCore.Tasks;  
>>>>>>> My-tree


[Condition("MyConditions/IsBossLow")]
[Help("Returns true, falls der Boss weniger als 1000 Lebenspunkte hat")]
public class IsBossLow : ConditionBase
{
<<<<<<< HEAD
    public override bool Check()
    {
        Debug.Log("Ist der Boss low HP?");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            Boss bossScript = boss.GetComponent<Boss>();
            if (bossScript!=null) {               
                Debug.Log("Ja");
                return bossScript.isLow;        				
			}
		}
        Debug.Log("Nein");
        return false;
	}
=======
    
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
   
>>>>>>> My-tree
} //class IsBossLow
