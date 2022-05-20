using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerup : MonoBehaviour
{
    // Start is called before the first frame update

    public float coin=25;
    public Image fill;
    public float timewait;
    public bool reducepow = false;
    public static powerup instance;
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        if (!instance)
        {

            instance = this;
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (reducepow == true)
        {
            StartCoroutine(normal(timewait));
        }
        else
        {
            reducepow = false;
        }
    }
    IEnumerator  normal(float time)
    {

      
        
      
       
        yield return new WaitForSeconds(time);


        if (powerup.instance.fill.fillAmount ==0)
        {

            reducepow = false;
        }

        else
        {
            powerup.instance.fill.fillAmount -= 0.0005f;
            reducepow = true;
        }
        
    }
}
