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
    private Vector2 currentposition;
    public float distnation = 10.0f;
    private int face = 1;
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
        currentposition = gameObject.transform.position;
        attackPos = transform.Find("attackPos");
        hp = 20;
    }
    

    private void FixedUpdate()
    {
        if (face == 1)
        {
            gameObject.transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
            
        }
        if (face == 0)
        {
            gameObject.transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);

        }
        if (gameObject.transform.position.x > currentposition.x + distnation)
        {
            face = 0;
            //transform.eulerAngles = new Vector3(gameObject.transform.position.x, 180, 0);
        }
        if (gameObject.transform.position.x < currentposition.x)
        {
            face = 1;
        }
        /* if (h < 0)
         {
             gameObject.transform.rotation.;

         }
         //wenn der Skeleton nach rechts bewegt, sein Kopf bleibt nach rechts 
         else if (h > 0)
         {
             this.transform.eulerAngles = new Vector3(0, 0, 0);
             //skeletonEulerAngles = new Vector3(0, 0, 0);
         }*/


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
                timeValChangeDirection = 2;
                break;
            case "Wall":
                timeValChangeDirection = 2;
                break;
            default:
                break;
        }
     }
    private void Attack()
    {
        GameObject slash = Instantiate(skeletonPrefab, attackPos.position, attackPos.rotation);
        //InvokeRepeating("Attack", 0, 3);
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
