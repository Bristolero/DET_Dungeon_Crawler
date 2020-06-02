using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 0.3f);     
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        GameObject ex = GameObject.Instantiate(bombExplosionPrefab, transform.position, transform.rotation) as GameObject; //1
        Destroy(ex, 0.3f); //4
    }

}
