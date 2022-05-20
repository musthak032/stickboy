using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderweb : MonoBehaviour
{
    public float destime;
    float distance;
    public float webbulspeed;
    public float stop;
    /// for web bullet time

    public float spawntime;
    public float min;
    public float decrease;
    float timespawn;

    /// <summary>
    /// ///game object
    /// 
    /// </summary>
    /// 
    Animator anim;
    [SerializeField] private GameObject web;
    GameObject player;
    GameObject webbul;
    [SerializeField] private GameObject webstart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D webrb = web.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 x = new Vector2(player.transform.position.x, 0);
        Vector2 x1 = new Vector2(transform.position.x, 0);
         distance = Vector2.Distance(x,x1);
        //
        if (timespawn <= 0)
        {
            if (distance < stop)
            {
                if (player.transform.position.x >= transform.position.x)
                {

                    transform.localScale = new Vector2(-1, 1);

                    /// web bullet
                    web.transform.localScale = new Vector2(1, 1);
                    webbul = Instantiate(web, webstart.transform.position, Quaternion.identity);
                    Vector2 force = new Vector2(webbulspeed, 0);
                    anim.SetBool("webattack", true);
                    webbul.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                    Destroy(webbul, destime);

                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                    ///    web bullet
                    web.transform.localScale = new Vector2(-1, 1);
                    webbul = Instantiate(web, webstart.transform.position, Quaternion.identity);
                    Vector2 force = new Vector2(webbulspeed, 0);
                    anim.SetBool("webattack", true);
                    webbul.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
                    Destroy(webbul, destime);
                }
                //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime*5);

            }
            timespawn = spawntime;
            if (timespawn < min)
            {
                timespawn -= decrease;
            }
        }///
        else
        {
            timespawn -= Time.deltaTime;
            anim.SetBool("webattack", false);
        }
    }
}
