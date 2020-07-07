using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class Goblin : MonoBehaviour
{
    public float moveSpeed;
    private float timer = 0.0f;
    private float waitTime = 3.0f;
    public Rigidbody2D rb;
    public GameObject disappearPrefab;
    public GameObject bombPrefab;
    public int hp;
    public int bombSpeed;

    int minRange = 3;
    int maxRange = 8;
    private Transform target;
    private float h;
    private float v;   
    private Transform attackPos;
    private Vector3 goblinEulerAngles;



    private void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();  
        rb = GetComponent<Rigidbody2D>();
        
    }
   
    private void Start()
    {
        rb.freezeRotation = true;
        attackPos = transform.Find("attackPos");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        bombPrefab.SetActive(true);
        hp = 5;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(Vector3.Distance(target.position,transform.position) < minRange && Vector3.Distance(target.position,transform.position) >= minRange - 1)
        {
            if(timer > waitTime) 
            {
                ThrowBomb();
                timer = timer - waitTime;
			}
            
		}
	}

    private void FixedUpdate()
    {
        if(Vector3.Distance(target.position,transform.position) <= maxRange && Vector3.Distance(target.position,transform.position) >= minRange) 
        {
            Chase();
        }
        else if(Vector3.Distance(target.position,transform.position) <= minRange - 1 )
        {
            Runaway();
        }
        else 
        {        
            Stop();
		}
	}


    //Jagt den Spieler falls Distanz klein ist
    //v und h werden je nach Position des Spielers verändert
    private void Chase() {	           
        if(target.position.x + 1 > transform.position.x)
        {
            h = 1;
		}
        else if(target.position.x - 1 < transform.position.x)
        {
            h = -1;
		}
        else
        {
            h = 0;
		}

        if(target.position.y + 1 > transform.position.y)
        {
            v = 1;
		}
        else if(target.position.y - 1 < transform.position.y)
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
        //wenn der Goblin sich nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void Stop()
    {
        rb.velocity = new Vector3(0, 0);
        if (h < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
        }
        //wenn der Goblin sich nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
        }
	}

    private void Runaway() {
        if(target.position.x > transform.position.x)
        {
            h = -1;
		}
        else if(target.position.x < transform.position.x)
        {
            h = 1;
		}
        else
        {
            h = 0;
		}

        if(target.position.y > transform.position.y)
        {
            v = -1;
		}
        else if(target.position.y < transform.position.y)
        {
            v = 1;
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
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            goblinEulerAngles = new Vector3(0, 0, 0);
        }
	}

    private void ThrowBomb()
    {
        GameObject bomb = GameObject.Instantiate(bombPrefab, attackPos.position, Quaternion.Euler(transform.eulerAngles + goblinEulerAngles));
        Vector3 tmp = target.transform.position;
        if(tmp.x > bomb.transform.position.x)
        {
           h = 1;
		}
        else if(tmp.x < bomb.transform.position.x)
        {
           h = -1;
		}
        else
        {
           h = 0;
		}

        if(tmp.y > bomb.transform.position.y)
        {
            v = 1;
		}
        else if(tmp.y < bomb.transform.position.y)
        {
            v = -1;
		}
        else
        {
            v = 0;
		}
        bomb.GetComponent<Rigidbody2D>().velocity = new Vector3(h*bombSpeed, v*bombSpeed);

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

    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}