using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform firePos;
    private Vector3 bulletEulerAngles;
    private float timeVal = 4;
    private float timeValChangeDirection = 2;
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
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
    private void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
        bullet.name = "EnemyBullet";
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
}
