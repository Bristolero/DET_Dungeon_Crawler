using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject disappearPrefab;
    private Transform firePos;
    private Vector3 bulletEulerAngles;
    private float timeVal = 4;
    private float timeValChangeDirection = 2;
    private float h;
    private float v;
    public float moveSpeed = 2;
    public Rigidbody2D rb;
    private int hp;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();

    }
    void Start()
    {
        hp= 20;
        firePos = transform.Find("flyAttackPos");
    }
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
    private void FixedUpdate()
    {

        Move();


    }
    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
        bullet.name = "EnemyBullet";
    }

    private void Move()
    {
        if (timeValChangeDirection >= 0.5f)
        {
            int num = Random.Range(0, 4);
            if (num == 3)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num == 1)
            {
                v = 0;
                h = -1;
            }
            else
            {
                v = 0;
                h = 1;
            }
            timeValChangeDirection = 0;


        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }


        rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
        if (h<0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if(h>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
            
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //
        switch (other.gameObject.tag)
        {
            case "Player":
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
