using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject disappearPrefab;
    public float bombSpeed = 3f;
    private Transform attackPosition;
    private float timeVal = 0;
    private Vector3 attackEulerAngles;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        attackPosition = transform.Find("bombPos");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeVal > 2f)
        {
            Attack();

        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void Attack()
    {
        timeVal = 0;
        //die Position und Rotation von Attack ist abhängig von dem Slime.
        GameObject go = GameObject.Instantiate(bombPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles)) as GameObject;
        go.GetComponent<Rigidbody2D>().velocity = new Vector3(bombSpeed, 0);
        float h = Input.GetAxis("Horizontal");
        go.name = "MonsterBomb";
        if (h < 0)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector3(-bombSpeed, 0);
        }
    }
    private void MonsterDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
            else
            {
                //damage
            }

        }
    }

    //der Monster tot Methode
    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
