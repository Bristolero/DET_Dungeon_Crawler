using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{  // Der Spieler bewegt sich mit einer Geschwindigkeit 3f  
    public float moveSpeed = 3f;
    private Vector3 attackEulerAngles;
    private float timeVal= 0;

    //ein Rigidbody 2D component unter der Kontrolle vom physics engine
    public Rigidbody2D rb;
    public GameObject swordPrefab;
    public GameObject bombPrefab;
    public GameObject disappearPrefab;
    public float bombSpeed = 3f;
    public int hp;
    public int hptotal;
    //Die Position von attack ist vorne von dem player
    private Transform attackPosition;

    //Falls der Player den Key hat dann true
    public bool hasKey = false;
   
    //Momentaner Index für die Anzahl an Scenen 
    private int nextSceneToLoad;

    //Für Kamera
    //Für Camera
    private Transform playerTransform;
    public float offset;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        attackPosition = transform.Find("Attack_pos");
        rb = GetComponent<Rigidbody2D>();
        swordPrefab.SetActive(true);
        bombPrefab.SetActive(true);
        hp = 100;
        hptotal = hp;
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            cam = GameObject.Find("Camera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //die Methode swordAttack wird aufgerufen 

                swordAttack();
            
                bombAttack();
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            CameraFollow();
        }
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
            GameObject slash = Instantiate(swordPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles));
            slash.name = "PlayerSlash";
            slash.AddComponent<Slash>();
            FindObjectOfType<AudioManager>().Play("PlayerAttack");
          }
    }

    private void bombAttack()
    {
        timeVal += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F))
        {
           
            //die Position und Rotation von Attack ist abhängig von dem player.
            GameObject go = GameObject.Instantiate(bombPrefab, attackPosition.position, Quaternion.Euler(transform.eulerAngles + attackEulerAngles)) as GameObject;
            go.GetComponent<Rigidbody2D>().velocity = new Vector3( bombSpeed, 0);
            go.name = "PlayerBomb";
            float h = Input.GetAxis("Horizontal");
            if (h < 0)
            {
                go.GetComponent<Rigidbody2D>().velocity = new Vector3(-bombSpeed, 0);
            }
            if (h == 0)
            {
                go.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
            }
        }
    }
    private void Move()
    {
        Vector3 vel = rb.velocity;
        float h = Input.GetAxis("Horizontal");

        //v ist die Bewegung der Spieler in vertikaler Richtung,-1 nach unten, 1 nach oben.
        float v = Input.GetAxis("Vertical");
        //h ist die Bewegung der Spieler in horizontaler Richtung,-1 nach links,1 nach rechts.

        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            //rigidbody bewegt sich mit moveSpeed im jeweiligen Richtung
            rb.velocity = new Vector3(h * moveSpeed, v *moveSpeed);
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

    private void OnTriggerEnter2D (Collider2D other)
    {
        //Guckt ob der Spieler mit dem Key collided
        if(other.tag == "Key")
        {
           hasKey = true;
           other.gameObject.SetActive (false);
           Destroy(other);
        }
        else if(other.tag == "Exit")
        {
            if(hasKey == true) SceneManager.LoadScene(nextSceneToLoad);  
		}
    }

    private void Damage(int damage)
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

    //der Player tot Methode
    private void Die()
    {
        //disappeareffect
        Instantiate(disappearPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void CameraFollow()
    {
        playerTransform = transform;
        Vector3 temp = cam.transform.position;
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;

        temp.y += offset;

        temp.x += offset;

        cam.transform.position = temp;
    
    }


}
