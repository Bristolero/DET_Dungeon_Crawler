using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject bombPrefab;
    public GameObject disappearPrefab;
    public float bombSpeed = 3f;
    public float bulletSpeed = 4f;
    private Transform attackPosition;
    private Transform shootPosition;
    private Transform slime;
    private float timeVal = 0;
    private Vector3 attackEulerAngles;
    private Transform eu;
    private int hp;

    private float h;
    private Transform target;

    private float minRange = 5f;
    private float maxRange = 1f;

     
    private Vector3 slimeEulerAngles;

    private bool didCollide;
    private bool goBack;
    

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        attackPosition = transform.Find("bombPos");
        shootPosition = transform.Find("shootPos");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {   
        if(target!=null) {
            if(Vector3.Distance(target.position,transform.position) < minRange) { 
                if (timeVal > 2f)
                {
                     //Attack();
                     Shoot();
                    
                }
        
                 else
                {
                    timeVal += Time.deltaTime;
                }
            }
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


    private void Shoot()
    {
        timeVal = 0f;
        Transform tmp = target.transform;
        Vector3 dir = (tmp.position - rb.transform.position).normalized;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles)) as GameObject;
        
	}


    private void Stop()
    {
        rb.velocity = new Vector2(0,0);
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
