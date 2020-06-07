using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class Goblin : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public GameObject slashPrefab;
    public GameObject disappearPrefab;
    public int hp;

    int minRange = 3;
    int maxRange = 8;
    private Transform target;
    private float h;
    private float v;   
    private float timeValChangeDirection = 1;
    private Transform attackPos;
    private Vector3 goblinEulerAngles;


    private void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();  
        rb = GetComponent<Rigidbody2D>();
    }
   
    private void Start()
    {
        attackPos = transform.Find("attackPos");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        hp = 10;
    }

    void Update()
    {
     
 
	}

    private void FixedUpdate()
    {
        if(Vector3.Distance(target.position,transform.position) <= maxRange && Vector3.Distance(target.position,transform.position) >= minRange) 
        {
            Chase();
        }
	}


    //Jagt den Spieler falls Distanz klein ist
    //v und h werden je nach Position des Spielers verändert
    private void Chase() {	           
        if(target.position.x > transform.position.x)
        {
            h = 1;
		}
        else if(target.position.x < transform.position.x)
        {
            h = -1;
		}
        else
        {
            h = 0;
		}

        if(target.position.y > transform.position.y)
        {
            v = 1;
		}
        else if(target.position.y < transform.position.y)
        {
            v = -1;
		}
        else
        {
            v = 0;
		}
        
        rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
        if (h < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
        }
        //wenn der Skeleton nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
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

    private void Move()
    {
        
    }

    private void Attack()
    {
        GameObject slash = Instantiate(slashPrefab, attackPos.position, attackPos.rotation);
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

    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}