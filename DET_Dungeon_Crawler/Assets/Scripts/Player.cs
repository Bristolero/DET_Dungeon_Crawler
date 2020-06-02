using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  // Der Spieler bewegt sich mit einer Geschwindigkeit 3f  
    public float moveSpeed = 3f;
    private Vector3 attackEulerAngles;
    private float timeVal;

    //ein Rigidbody 2D component unter der Kontrolle vom physics engine
    public Rigidbody2D rb;
    public GameObject swordPrefab;
    public GameObject bombPrefab;
    public float bombSpeed = 10f;

    //Die Position von attack ist vorne von dem player
    private Transform attackPosition;


    // Start is called before the first frame update
    void Start()
    {
        attackPosition = transform.Find("Attack_pos");
        rb = GetComponent<Rigidbody2D>();
        swordPrefab.SetActive(true);
        bombPrefab.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //die Methode swordAttack wird aufgerufen 
                swordAttack();
            
            bombAttack();
            
    }
    void FixedUpdate()
    {
        Move();
        //die Methode swordAttack wird aufgerufen 

    }
    // Attack mit dem sword
    private void swordAttack()
    {  
        //die methode swordAttack wird mit dem Key"Space" kontrolliert.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //die Position und Rotation von Attack ist abhängig von dem player.
            Instantiate(swordPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles));
        }
    }

    private void bombAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //die Position und Rotation von Attack ist abhängig von dem player.
            GameObject go = GameObject.Instantiate(bombPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles)) as GameObject;
            go.GetComponent<Rigidbody2D>().velocity = new Vector3( bombSpeed, 0);
        }
    }
    private void Move()
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
            attackEulerAngles = new Vector3(0, 0, 0);
        }
        //wenn der Spieler nach rechts bewegt, sein Kopf bleibt nach rechts 
        if (h > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            attackEulerAngles = new Vector3(0, 0, 0);
        }
        if (h == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (v == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
