using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public GameObject player;
    private int hp;
    private int hpTotal;
    public Slider hpSlider;
    public Transform hpPosition;
    // Start is called before the first frame update
    void Start()
    {
        hp = player.GetComponent<Player>().hp;
        hpTotal = player.GetComponent<Player>().hptotal;
        //transform.Find("Attack_pos");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Destroy(hpSlider);
            return;
        }
        //hpSlider.direction = Slider.Direction.RightToLeft;
        hpPosition.position = new Vector3(player.transform.position.x - 0.9f, player.transform.position.y + 0.7f, player.transform.position.z);
        hpSlider.transform.position = hpPosition.position;
        hpTotal = player.GetComponent<Player>().hptotal;
        hp = player.GetComponent<Player>().hp;
        hpSlider.value = (float)hp / hpTotal;
    }
}
