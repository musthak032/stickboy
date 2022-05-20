using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weapon : MonoBehaviour
{
    
    public GameObject dust;
    public float decrease;
    public float spawnbtw;
    public float min = 0.5f;
     float timespawn;
    [SerializeField] private Transform scale;
   private GameObject bullets;
    public float time;
    public float forcespeed;
    public GameObject bullet;
    public Rigidbody2D player;
    private Collider2D laser;
    // private Transform weaponrotate;
    //sound
    AudioSource bom;
    [SerializeField] private Transform shotpoint;
    // Start is called before the first frame update
    /// <summary>
    /// ///////////////////
    /// </summary>mobile
    /// 
   
    void Start()
    {
        // weaponrotate = GetComponent<Transform>();
        // laser = bullets.GetComponent<Collider2D>();
         bom = GetComponent<AudioSource>();

    }

    private void Update()
    {
        /* Vector2 gunposition = transform.position;
         Vector2 mouseposition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
         Vector2 direction = mouseposition -gunposition;
         transform.right = direction;*/
        // transform.right = new Vector2(0, 0);
       // pcinput();
        bdestroy();

    }

    private void pcinput()
    {
        if (timespawn <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (scale.localScale.x == 1)
                {

                    bullet.transform.localScale = new Vector2(1, 1);
                    bullets = Instantiate(bullet, shotpoint.position, Quaternion.identity);
                    Instantiate(dust, shotpoint.position, Quaternion.identity);
                    //  bullets.GetComponent<Rigidbody2D>().velocity =transform.right * forcespeed;
                    transform.right = new Vector2(0.7f, 0.7f);
                    bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f, 0) * forcespeed, ForceMode2D.Impulse);
                    Vector2 force = new Vector2(-10, player.velocity.y);
                    player.AddForce(force, ForceMode2D.Impulse);

                    Invoke("normalps", 0.5f);

                }
                else if (scale.localScale.x == -1)
                {
                    bullet.transform.localScale = new Vector2(-1, 1);
                    bullets = Instantiate(bullet, shotpoint.position, Quaternion.identity);
                    Instantiate(dust, shotpoint.position, Quaternion.identity);
                    // bullets.GetComponent<Rigidbody2D>().velocity = -transform.right  * forcespeed;
                    transform.right = -new Vector2(-0.7f, 0.9f);


                    bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, 0) * forcespeed, ForceMode2D.Impulse);
                    Vector2 force = new Vector2(10, player.velocity.y);
                    player.AddForce(force, ForceMode2D.Impulse);


                    Invoke("normalps", 0.5f);

                }






            }

            timespawn = spawnbtw;
            if (spawnbtw < min)
            {
                spawnbtw -= decrease;
            }
        }
        else
        {
            timespawn = -Time.deltaTime;
        }
    }

    void bdestroy()
    {
        Destroy(bullets, time);
    }
    void normalps()
    {
        transform.right = new Vector2(0, 0);
        
    }
    /// <summary>
    /// //mobele input
    /// </summary>
    public void mobileinputmouse()
    {


          
            if (gameObject.activeInHierarchy == true)
            {
                if (timespawn <= 0)
                {
                       bom.Play();
                    // if (Input.GetMouseButtonDown(0))
                    {

                        if (scale.localScale.x == 1)
                        {

                            bullet.transform.localScale = new Vector2(1, 1);
                            bullets = Instantiate(bullet, shotpoint.position, Quaternion.identity);
                           
                            //  bullets.GetComponent<Rigidbody2D>().velocity =transform.right * forcespeed;
                            transform.right = new Vector2(0.7f, 0.7f);
                            bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f, 0) * forcespeed, ForceMode2D.Impulse);
                          player.velocity = new Vector2(-10, player.velocity.y);

                        Invoke("normalps", 0.5f);

                        }
                        else if (scale.localScale.x == -1)
                        {
                            bullet.transform.localScale = new Vector2(-1, 1);
                            bullets = Instantiate(bullet, shotpoint.position, Quaternion.identity);
                           
                            // bullets.GetComponent<Rigidbody2D>().velocity = -transform.right  * forcespeed;
                            transform.right = -new Vector2(-0.7f, 0.9f);


                            bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, 0) * forcespeed, ForceMode2D.Impulse);

                        player.velocity = new Vector2(10, player.velocity.y);

                            Invoke("normalps", 0.5f);

                        }






                    }

                    timespawn = spawnbtw;
                    if (spawnbtw < min)
                    {
                        spawnbtw -= decrease;
                    }
                }
                else
                {
                    timespawn = -Time.deltaTime;
                }
            }

        
    }
  
}
