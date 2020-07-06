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
        //transform.Find("Attack_pos");
    }

    // Update is called once per frame
    void Update()
    {
        //hpSlider.direction = Slider.Direction.RightToLeft;
        hpPosition.position = new Vector3(boss.transform.position.x-1.25f, boss.transform.position.y+1.5f, boss.transform.position.z);
        hpSlider.transform.position = hpPosition.position;
        hpTotal = boss.GetComponent<Boss>().hpTotal;
        hp = boss.GetComponent<Boss>().hp;
        hpSlider.value = (float)hp / hpTotal;
    }
}
