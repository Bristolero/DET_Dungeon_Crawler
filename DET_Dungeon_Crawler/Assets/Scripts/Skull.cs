using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public float speed = 5f;
    public int hp;
    public Rigidbody2D rb;
    public GameObject disappearPrefab;
    public float chargeDistance;
    private Transform target;
    private Vector3 skullEulerAngles;
    private float h;
    private float v;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        gameObject.AddComponent<BoxCollider2D>();
	}
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (Vector3.Distance(target.position,transform.position) < chargeDistance )
        ChargeAtPlayer();
        else {
            Stop();  
		}
	}

    private void Stop()
    {
        rb.velocity = new Vector3(0, 0);
        if (h < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            skullEulerAngles = new Vector3(0, 0, 0);
        }
        //wenn der Skull sich nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            skullEulerAngles = new Vector3(0, 0, 0);
        }
	}

    private void ChargeAtPlayer()
    {
        Vector3 dir = (target.transform.position - rb.transform.position).normalized;
        rb.MovePosition(rb.transform.position + dir * speed * Time.fixedDeltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                
                collision.SendMessage("Damage",30);
                Die();
                break;
            default:
                break;


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

    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
