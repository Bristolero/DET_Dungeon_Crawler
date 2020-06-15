using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int lifeValue;
    public int playerKey = 0;
    


    private static PlayerManager instance;
    public bool isDead;
    public GameObject Player;
    public bool isDefeat;
    public Text playerLifeValueText;
    public Text keyText;


    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        lifeValue = Player.GetComponent<Player>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Player>().hasKey)
        {
            playerKey = 1;
        }
        
        lifeValue = Player.GetComponent<Player>().hp;
        playerLifeValueText.text = lifeValue.ToString();
        keyText.text = playerKey.ToString();

    }

    /* private void recover()
     {
         if (lifeValue <= 0)
         {
             isDefeat = true;

         }
         else
         {
             lifeValue--;
             GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
             go.GetComponent<Born>().createPlayer = true;
             isDead = false;
         }
     }*/

    private void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
