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
        
    }

    // Update is called once per frame
   
    void FixedUpdate()
    { //x ist die Bewegung der Spieler in horizontaler Richtung
        float x = Input.GetAxisRaw("Horizontal");

      //y ist die Bewegung der Spieler in vertikaler Richtung
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right *x * moveSpeed * Time.fixedDeltaTime, Space.World);

        //wenn der Spieler nach links bewegt, sein Kopf dreht nach links
        if (x < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        //wenn der Spieler nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (x > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.Translate(Vector3.up * y * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}
