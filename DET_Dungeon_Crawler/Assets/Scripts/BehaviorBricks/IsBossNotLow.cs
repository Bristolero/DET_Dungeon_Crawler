using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Framework; // ConditionBase


[Condition("MyConditions/IsBossNotLow")]
[Help("Returns true, falls der Boss mehr als 1000 Lebenspunkte hat")]
public class IsBossNotLow : ConditionBase
{
    public override bool Check()
    {
        Debug.Log("Ist der Boss low HP?");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            Boss bossScript = boss.GetComponent<Boss>();
            if (bossScript!=null) {               
                Debug.Log("Ja");
                Debug.Log("Ist der Boss low?" + !bossScript.isLow);
                return !bossScript.isLow;        				
			}
		}
        Debug.Log("Nein");
        return true;
	}
} //class IsBossNotLow
