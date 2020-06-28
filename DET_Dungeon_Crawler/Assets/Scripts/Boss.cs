using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int hp = 500;
    public Slider hpSlider;

    private int hpTotal;

    // Use this for initialization
    void Start()
    {
        hpTotal = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / hpTotal;
        if (hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
