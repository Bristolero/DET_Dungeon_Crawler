 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class Priest : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject disappearPrefab;
    public GameObject slashPrefab;
    public GameObject skullPrefab;
    public GameObject torchPrefab;
     
    public Rigidbody2D rb;
    public int hp;
    public float minRange = 5f;
    
    private int dir = 1;
    private float timer = 0.0f;
    private float waitTime = 5.0f;
    private Transform target;
    private Transform slashPos;
    private float timeLeft;
    private Transform spawnPos;
    private Vector2 movement;
    private float v;
    private float h;

    private float dirTotal = 5f;
    private float dirTimer = 0f;

    private Vector3 priestEulerAngles;

    



    private void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();  
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.Find("SpawnPos");
        slashPos = transform.Find("SlashPos");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hp = 50;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            if(Vector3.Distance(target.position,transform.position) < minRange) 
            {
                Transform tmp = spawnPos;
                GameObject torch = Instantiate(torchPrefab, tmp.position, tmp.rotation);
                Stop();
                Invoke("SummonSkull", 2);
                Destroy (torch, 2f);                         
			}
            timer = timer - waitTime;          
		}
        if(Vector3.Distance(target.position,transform.position) > minRange) 
        {
            directionChanger(); 
		}
    }

    void FixedUpdate()
    {
       RandomMovement();
	}

    private void directionChanger()
    {
        dirTimer += Time.deltaTime;
        if(dirTimer >= dirTotal)
        {
              dir = Random.Range(1,4);
              dirTimer = dirTimer - dirTotal;
		}
	}

    private void RandomMovement()
    {
        switch(dir)
        {
        case 1 :
            h = 1;
            v = 0;    
        break;
        case 2 :
            h = -1;
            v = 0;
        break;
        case 3 :
            h = 0;
            v = -1;
        break;
        case 4 :
            h = 0;
            v = 1;
        break;
        }
        rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
        if (h <= 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            priestEulerAngles = new Vector3(0, 0, 0);
        }
        //wenn der Goblin sich nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            priestEulerAngles = new Vector3(0, 0, 0);
        }
	}

    private void Stop()
    {
        rb.velocity = new Vector3(0,0,0);
	}

    private void SummonSkull()
    {
        GameObject skull = Instantiate(skullPrefab, spawnPos.position, spawnPos.rotation);
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":

                Attack();
                break;

            case "Monster":
                dir = Random.Range(1,4);
                break;
            //case "Wall":
              //  dir = Random.Range(1,4);
                //break;
            default:
                break;  
		}
	}

    private void Attack()
    {
        Invoke("Attack", 1);
        GameObject slash = Instantiate(slashPrefab, slashPos.position, slashPos.rotation);
        
        slash.name = "MonsterSlash";
        slash.AddComponent<Slash>();        
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
