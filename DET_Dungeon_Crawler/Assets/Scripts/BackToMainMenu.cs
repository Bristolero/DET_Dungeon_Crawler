using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    
    private float time = 3f;
    private float waitTimer = 0f;

    public void Start()
    {
    
	}

    public void Update()
    {
        if(waitTimer < time)
        {
            waitTimer += Time.deltaTime;  
		}
        else 
        {
            SceneManager.LoadScene(0);  
		}
	}
}
