﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int hp;
    //public Slider hpSlider;

    public bool isLow;  
    public int hpTotal;

    private float timer = 0f;
    private float waitTimer = 0.2f;

    private Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        hpTotal = hp;
        m_Animator = GetComponent<Animator>();
        //hpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetTrigger("Attacked1");
        isLow = (hp < 1500);       

        Debug.Log("Is Boss low: " + isLow);
        while(timer < waitTimer)
        {
            timer = timer + Time.deltaTime;  
		}
        m_Animator.ResetTrigger("Attacked1");
        //hpSlider.value = (float)hp / hpTotal;


    }
    void TakeDamage(int damage)
    {
        if (hp <= 0) return;       
        hp -= damage;
        //hpSlider.value = (float)hp / hpTotal;
        if (hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }      
    }


    public void Sethp(int selfhp)
    {
        hp = selfhp;
    }

}
