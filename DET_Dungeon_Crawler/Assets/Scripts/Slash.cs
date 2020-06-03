using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
         //Slasheffekt verschwindet nach 0,3f sekunde
        Invoke("Slash", 0.3f);
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       switch(other.tag)
        {
            case "Player":
                if (this.name == "MonsterSlash") { other.SendMessage("Damage", 5, SendMessageOptions.DontRequireReceiver); }
                break;
            case "Monster":
                if (this.name == "PlayerSlash") { other.SendMessage("Damage", 5, SendMessageOptions.DontRequireReceiver);  }
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:// slash gegen slash  stoßen
                if (this.name != other.name)
                Destroy(gameObject);
                break;


        }
    } 
}
