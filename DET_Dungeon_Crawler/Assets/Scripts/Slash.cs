using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //Slasheffekt verschwindet nach 0,1f sekunde
        Invoke("Slash", 0.2f);
       Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
