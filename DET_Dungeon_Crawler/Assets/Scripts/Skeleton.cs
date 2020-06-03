using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]


public class Skeleton : MonoBehaviour
{
    public int hp;
    public GameObject skeletonPrefab;
    public GameObject disappearPrefab;
    private Transform attackPos;

    
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        attackPos = transform.Find("attackPos");
        InvokeRepeating("Attack", 0, 3);
        hp = 20;
    }
    void Update()
    {
        //Attack();
        //transform.Translate(Vector3.down * Time.deltaTime * 5);
    }
    private void Attack()
    {
        GameObject slash = Instantiate(skeletonPrefab, attackPos.position, attackPos.rotation);
        slash.name = "MonsterSlash";
        slash.AddComponent<Slash>();
    }

    private void Damage(int damage)
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

    //der skeleton tot Methode
    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
