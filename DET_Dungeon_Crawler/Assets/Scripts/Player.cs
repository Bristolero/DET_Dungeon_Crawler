using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  // Der Spieler bewegt sich mit einer Geschwindigkeit 3f  
    public float moveSpeed = 3f;
    
   //ein Rigidbody 2D component unter der Kontrolle vom physics engine
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        //h ist die Bewegung der Spieler in horizontaler Richtung,-1 nach links,1 nach rechts.
        float h = Input.GetAxis("Horizontal");

        //v ist die Bewegung der Spieler in vertikaler Richtung,-1 nach unten, 1 nach oben.
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            //rigidbody bewegt sich mit moveSpeed im jeweiligen Richtung
            rb.velocity = new Vector3(h * moveSpeed, v * moveSpeed);
        }
        //wenn der Spieler nach links bewegt, sein Kopf dreht nach links
        if (h < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        //wenn der Spieler nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
     }
}
