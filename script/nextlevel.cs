using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextlevel : MonoBehaviour
{
    [SerializeField] private AudioSource comsound;
    [SerializeField] private GameObject completed;
    public int healthstart=3;
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            comsound.Play();
            completed.SetActive(true);
            //nextscene();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);
            Invoke("nextscene", 5);

        }
    }

    public void nextscene()
    {
        // completed.SetActive(false);
       
        SceneManager.LoadScene(level);

    }

    public void startbutton()
    {
         
        SceneManager.LoadScene(level);
        savedata.instance.health =healthstart;
        powerup.instance.fill.fillAmount = 0;
    }
    public void exit()
    {
        Application.Quit();
    }
}
