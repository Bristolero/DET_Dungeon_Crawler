using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private Transform target;
    public float bulletSpeed = 5;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Fly();
	}


    private void Fly()
    {
        Transform tmp = target;
        Vector3 dir = (tmp.position - transform.position).normalized;
        rb.MovePosition(transform.position + dir * bulletSpeed * Time.fixedDeltaTime);
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //überprüfung welche Gameobjekt hat Bullet getrofen
        switch (collision.tag)
        {
            case "Player":

                collision.SendMessage("Damage",30);
                Destroy(gameObject);
                break;

            case "Wall":
                Destroy(gameObject);
                break;
            default:
                break;


        }
    }
}
