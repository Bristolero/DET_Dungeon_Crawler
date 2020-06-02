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
        //GameObject.Instantiate(bombExplosionPrefab, transform.position, transform.rotation);
        //GameObject.Destroy(this.gameObject);
        //Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        Instantiate(bombExplosionPrefab, transform.position, Quaternion.identity); //1
        GetComponent<MeshRenderer>().enabled = false; //2
        transform.Find("Collider").gameObject.SetActive(false); //3
        Destroy(gameObject, 0.3f); //4
    }

}
