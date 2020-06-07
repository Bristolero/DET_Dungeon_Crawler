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
    private Transform player;
    private Transform attackPos;
    public Rigidbody2D rb;
    public GameObject skeletonPrefab;
    public GameObject disappearPrefab;
    private float distance;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
       
    }
   private void Start()
    {
        currentposition = gameObject.transform.position;
        attackPos = transform.Find("attackPos");
        player = GameObject.FindWithTag("Player").transform;
        hp = 20;
    }
    

    private void FixedUpdate()
    {
        if (player == null ) return;
        distance = Vector3.Distance(player.position, transform.position);
        Move();
        if (distance < 1)
        {
            moveSpeed = 0;
            
        }
        else
        {
            moveSpeed = 3;
        }
    }

    //Monster wird 
    private void Move()
    {
        if (face == 1)
        {
            gameObject.transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime, Space.World);

        }
        if (face == 0)
        {
            gameObject.transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime, Space.World);

        }
        if (gameObject.transform.position.x > currentposition.x + distnation)
        {
            face = 0;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (gameObject.transform.position.x < currentposition.x)
        {
            face = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
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
        Invoke("Attack", 1);
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
