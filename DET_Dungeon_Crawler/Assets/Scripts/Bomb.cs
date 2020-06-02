﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombExplosionPrefab;
    private float timeVal = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 1f);     
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }
    public void attack()
    {
        timeVal += Time.deltaTime;
        if (timeVal > 0.3f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            timeVal = 0;
        }
    }
    void Explode()
    {
        GameObject ex = GameObject.Instantiate(bombExplosionPrefab, transform.position, transform.rotation) as GameObject; //1
        Destroy(ex, 0.3f); //4
    }

}
