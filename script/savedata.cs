
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class savedata : MonoBehaviour
{
    //inspector
   public int health=3;
   public Text healt;
   public static savedata instance;

  


    void Start()
    {
        
        
        DontDestroyOnLoad(gameObject);
        
        if (!instance)
        {

            instance = this;

        }
        else 
        {
            Destroy(gameObject);
        }
        

    }
   


}
