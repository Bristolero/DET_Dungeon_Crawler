using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public GameObject boss;
    private int hp;
    private int hpTotal;
    public Slider hpSlider;
    public Transform hpPosition;
    // Start is called before the first frame update
    void Start()
    {
        hp = boss.GetComponent<Boss>().hp;
        hpTotal = boss.GetComponent<Boss>().hpTotal;
        transform.Find("Attack_pos");
    }

    // Update is called once per frame
    void Update()
    {
        
        hpPosition= boss.transform.Find("hpPos");
        hpSlider.transform.position = hpPosition.position;
        hpTotal = boss.GetComponent<Boss>().hpTotal;
        hp = boss.GetComponent<Boss>().hp;
        print(hp);
        print("total"+hpTotal);
        hpSlider.value = (float)hp / hpTotal;

    }
}
