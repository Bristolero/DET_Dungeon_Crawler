using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float bulletSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime, Space.World);
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
