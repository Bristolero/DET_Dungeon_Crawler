using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]


public class Skeleton : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 skeletonEulerAngles;
    public int hp;
    private float h;
    private float v;
    
    private float timeValChangeDirection = 2;

    private Transform attackPos;
    public Rigidbody2D rb;
    public GameObject skeletonPrefab;
    public GameObject disappearPrefab;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
       
    }
   private void Start()
    {
        
        attackPos = transform.Find("attackPos");
        hp = 20;
    }
    

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (timeValChangeDirection >= 1f)
        {
            int num = Random.Range(0, 3);
            {
                if (num <= 1)
                {
                    h = -1;
                    v = 0;
                }
                else
                {
                    h = 1;
                    v = 0;
                 }
               
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
        if (h < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            skeletonEulerAngles = new Vector3(0, 0, 0);

        }
        //wenn der Skeleton nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            skeletonEulerAngles = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //
        switch (other.gameObject.tag)
        {
            case "Player":
                Attack();
                break;
            case "Monster":
                timeValChangeDirection = 1;
                break;
            case "Wall":
                timeValChangeDirection = 1;
                break;
            default:
                break;
        }
     }
    private void Attack()
    {
        GameObject slash = Instantiate(skeletonPrefab, attackPos.position, attackPos.rotation);
        InvokeRepeating("Slash", 0, 3);
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
