using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
<<<<<<< HEAD
=======

    
>>>>>>> My-tree
    public int hp;
    public Slider hpSlider;

    public bool isLow { get; private set; }
<<<<<<< HEAD

    public int hpTotal;
=======
    
    private int hpTotal;
>>>>>>> My-tree
    private float timer = 0f;
    private float waitTimer = 0.2f;

    private Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        hpTotal = hp;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetTrigger("Attacked1");
<<<<<<< HEAD
        isLow = (hp < 1000);
=======
        isLow = (hp < 1500);       
>>>>>>> My-tree
        Debug.Log("Is Boss low: " + isLow);
        while(timer < waitTimer)
        {
            timer = timer + Time.deltaTime;  
		}
        m_Animator.ResetTrigger("Attacked1");
<<<<<<< HEAD
        hpSlider.value = (float)hp / hpTotal;
=======

>>>>>>> My-tree
    }
    void TakeDamage(int damage)
    {
        if (hp <= 0) return;       
        hp -= damage;
<<<<<<< HEAD
        
=======
        hpSlider.value = (float)hp / hpTotal;
>>>>>>> My-tree
        if (hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }      
    }
<<<<<<< HEAD

    public void Sethp(int selfhp)
    {
        hp = selfhp;
    }
=======
>>>>>>> My-tree
}
