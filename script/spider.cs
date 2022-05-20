using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
    private Rigidbody2D rb;
    public float stopleft;
    public float stopright;
    private Transform trans;
    public float speed;
    private bool movestop=false;
    [SerializeField] private LayerMask ground;
    private Collider2D col;
   
    private Animator anim;
    public float timewait;
    /// <summary>
    /// ///////////////////////
    /// follow player
    /// 
    private GameObject playerpre;
    
    /*[SerializeField] */private Transform player;
    [SerializeField] private float stop=2;
    [SerializeField] private float speedmove = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        playerpre = GameObject.FindGameObjectWithTag("Player");
        player = playerpre.GetComponent<Transform>();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(trans.position, player.position) > stop)
        {
            StartCoroutine(move(timewait));
        }
        else
        {
            followplayer();
        }
    }

    private void followplayer()
    {
        if (trans.position.x > player.position.x && Vector2.Distance(trans.position, player.position) < stop)
        {
            if (trans.localScale.x != 1)
            {
                trans.localScale = new Vector2(1, 1);

            }
            else if (Vector2.Distance(trans.position, player.position) < stop)
            {
                Vector2 target = new Vector2(player.position.x, transform.position.y);
                trans.position = Vector2.MoveTowards(trans.position, target, speedmove * Time.deltaTime);
                anim.SetBool("run", true);
            }
            else
                anim.SetBool("run", false);

        }
        else if (trans.position.x < player.position.x && Vector2.Distance(trans.position, player.position) < stop)
        {
            if (trans.localScale.x != -1)
            {
                trans.localScale = new Vector2(-1, 1);

            }
            else if (Vector2.Distance(trans.position, player.position) < stop)
            {
                Vector2 target = new Vector2(player.position.x, transform.position.y);
                trans.position = Vector2.MoveTowards(trans.position, target, speedmove * Time.deltaTime);
                anim.SetBool("run", true);
            }
            else
                anim.SetBool("run", false);

        }
    }

    IEnumerator move(float time)
    {
        if (movestop==false)
        {
            if (trans.position.x >= stopleft)
            {
                if (trans.localScale.x != 1)
                {
                    trans.localScale = new Vector2(1, 1);

                }
                if (col.IsTouchingLayers(ground))
                {
                  
                    rb.velocity = new Vector2(-5f * speed, rb.velocity.y);
                    anim.SetBool("run", true);
                  
                }


            }
            else 
            {
                anim.SetBool("run", false);
                yield return new WaitForSeconds(time);
                movestop = true;




            }
            

        }
        else
        {
            


           if (trans.position.x <= stopright)
            {
                if (trans.localScale.x != -1)
                {
                    trans.localScale = new Vector2(-1, 1);

                }
                if (col.IsTouchingLayers(ground))
                {
                    
                  
                    rb.velocity = new Vector2(5f * speed, rb.velocity.y);
                    anim.SetBool("run", true);
              
                }
            }
            else
            {
                anim.SetBool("run", false);
                yield return new WaitForSeconds(time);
                movestop = false;
            }

           
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.position.x<trans.position.x)
            {

                anim.SetBool("attack", true);
              /*  Vector2 attack = new Vector2(-5, rb.velocity.y);
                rb.AddForce(attack, ForceMode2D.Impulse);*/
                
            }
           
            
            else
            {
                anim.SetBool("attack", true);
               /* Vector2 attack = new Vector2(5, rb.velocity.y);
                rb.AddForce(attack, ForceMode2D.Impulse);*/
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            anim.SetBool("attack", true);
           // Vector2 attack = new Vector2(rb.velocity.x,5);
           // rb.AddForce(attack, ForceMode2D.Impulse);
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
               anim.SetBool("attack", false);
               // rb.velocity = new Vector2(0,0);
      

    }

}
