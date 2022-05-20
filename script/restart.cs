using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class restart : MonoBehaviour
{
    //ui
    GameObject ui;
    GameObject uipow;
    //int health;
    GameObject player;
     GameObject gameover;
   
    //  static protected  charactercontrol con;
    // Start is called before the first frame update
    //save
    
    void Start()
    {
        //ui
        ui = GameObject.FindGameObjectWithTag("perui1");
        uipow= GameObject.FindGameObjectWithTag("perui2");
        gameover = GameObject.FindGameObjectWithTag("gameover");
        player = GameObject.FindGameObjectWithTag("Player");
        // con.health = player.GetComponent<charactercontrol>().health;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            charactercontrol con =player.GetComponent<charactercontrol>();


            if (savedata.instance.health <= 0)
            {

                //powerup
                powerup.instance.fill.fillAmount =0 ;
                
                gameover.SetActive(true);
                
                Time.timeScale = 1;
                

            }

            else if (savedata.instance.health >0)
            {
                savedata.instance.health -= 1;

                //powerup
                powerup.instance.fill.fillAmount-=0.5f;
                powerup.instance.reducepow = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }



        }
    }
  public  void level()
    {
        Destroy(ui);
        Destroy(uipow);
        SceneManager.LoadScene("front");
        Time.timeScale = 1;

    }

    
  
}
