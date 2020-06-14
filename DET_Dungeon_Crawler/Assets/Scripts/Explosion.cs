using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //überprüfung welche Gameobjekt hat Bullet getrofen
        switch (other.tag)
        {
            case "Player":
                //überprüfen ob der Bomb von Player
                if (this.name == "MonsterExplosion") { other.SendMessage("Damage", 10, SendMessageOptions.DontRequireReceiver); }
                break;
            case "Monster":
                if (this.name == "PlayerExplosion") { other.SendMessage("MonsterDamage", 10, SendMessageOptions.DontRequireReceiver); }
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:
                break;


        }
    }
}
