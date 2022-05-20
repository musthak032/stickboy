using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public  class charactercontrol : MonoBehaviour
{
    //ui

    //boss1
    //layemask
    [SerializeField] private LayerMask liver;
  
    //powerup
    [SerializeField] private GameObject poweruppartical;
    /// <summary>
    /// health progress
    /// </summary>
    /// 
         public static int  healthstart=3;
    //sound
    [SerializeField] private AudioSource jumpsound;
    [SerializeField] private  AudioSource hit;
    [SerializeField] private AudioSource deadsound;
    [SerializeField] private AudioSource coinsound;
    ///////////////now button
    /// </summary>
    [SerializeField] private GameObject fire;
    
    ////////////////////////
  [SerializeField] private float hurtforce;

    [SerializeField] private GameObject enemydead;
    [SerializeField] private GameObject jumpcollider;

   [SerializeField] private GameObject gun;
    [SerializeField] private float jumpforce;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private LayerMask ground;
     
    private Collider2D coll;
    private enum State { playeridle,run,jump,fall,hurt,weaponrun}
    State state = State.playeridle;
    /// <summary>
    /// mobile move
   public  GameObject gameover;
    /// </summary>
    /// 
     bool dontmove=true;
     bool moveleft;
    /// <summary>
    /// save files
    /// </summary>
    /// 
    CapsuleCollider2D colc;
    Animator animover;
   
    // Start is called before the first frame update
    void Start()
    {


        colc = GetComponent<CapsuleCollider2D>();
       // col = jumpcollider.GetComponent<Collider2D>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameover = GameObject.FindGameObjectWithTag("gameover");
        animover = gameover.GetComponent<Animator>();
         gameover.SetActive(false);
        Time.timeScale = 1;
        ////////////////////////
        
        powerup.instance.reducepow = false;

        /// save
        /// 
       

    }
    
    // Update is called once per frame
    void Update()
    {

        
        //
        if (powerup.instance.fill.fillAmount == 1)
        {
            poweruppartical.SetActive(true);
            powerup.instance.reducepow = true;
        }
        else if (powerup.instance.fill.fillAmount == 0)
        {
            poweruppartical.SetActive(false);
            powerup.instance.reducepow = false;
        }
        //

        if (powerup.instance.reducepow == true)
        {
            poweruppartical.SetActive(true);
        }
        else
        {
            poweruppartical.SetActive(false);
        }


        life();
        
        handler();
        pcmove();
        animationmove();
        anim.SetInteger("state", (int)state);
    }
    void life()
    {
        savedata.instance.healt.text = savedata.instance.health.ToString();
        if (savedata.instance.health <= 0)
        {
            gameover.SetActive(true);
           
            
            ///powerup
            powerup.instance.fill.fillAmount = 0;
            Time.timeScale = 1;
            
        }
    }

    private void pcmove()
    {
        if (state != State.hurt)
        {
            move();
        }
        if (Input.GetKey(KeyCode.Space) && colc.IsTouchingLayers(ground))
        {
            jump();
        }
    }

  

    public void jump()
    {
        //powerup
        if (powerup.instance.reducepow==true)
        {


            state = State.jump;

            rb.velocity = new Vector2(rb.velocity.x, 6 * jumpforce);
        }

        else
        {
            powerup.instance.reducepow = false;
            state = State.jump;

            rb.velocity = new Vector2(rb.velocity.x, 5 * jumpforce);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (poweruppartical.activeSelf==true) 
        {
            if ( collision.gameObject.tag == "enemy") 
            {
                jump();
                Instantiate(enemydead, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                deadsound.Play();
            }

        }
        else
        {


            if (collision.gameObject.tag == "enemy")
            {

                if (state == State.fall)
                {
                    jumpsound.Play();
                    jump();
                    Instantiate(enemydead, collision.gameObject.transform.position, Quaternion.identity);
                    deadsound.Play();
                    Destroy(collision.gameObject);
                }


                else
                {
                    hit.Play();
                    state = State.hurt;
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        rb.velocity = new Vector2(-hurtforce, rb.velocity.y);

                    }
                    else
                    {
                        rb.velocity = new Vector2(hurtforce, rb.velocity.y);

                    }
                    savedata.instance.health -= 1;
                    //powerup
                    powerup.instance.fill.fillAmount -= 0.1f;

                }
            }
            else if (collision.gameObject.tag == "web")
            {
                hit.Play();
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {


                    Destroy(collision.gameObject);
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);

                }
                else
                {
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                    Destroy(collision.gameObject);
                }
                savedata.instance.health -= 1;
                //powerup
                powerup.instance.fill.fillAmount -= 0.1f;
            }
            else if (collision.gameObject.tag == "spiderboss")
            {
                hit.Play();
                if (state == State.fall)
                {
                    Instantiate(enemydead, collision.gameObject.transform.position, Quaternion.identity);
                    collision.gameObject.GetComponent<bosslife>().takedamage();
                    if (transform.position.x < collision.gameObject.transform.position.x)
                    {

                       Rigidbody2D bossrb =collision.gameObject.GetComponent<Rigidbody2D>();
                        bossrb.velocity = new Vector2(10, 5);
                    }
                    else
                    {
                        Rigidbody2D bossrb = collision.gameObject.GetComponent<Rigidbody2D>();
                        bossrb.velocity = new Vector2(-10, 5);

                    }

                    
                }
                else
                {

                    state = State.hurt;
                   // Vector2 up = new Vector2(2*5,rb.velocity.y);
                   // rb.velocity = up;
                    savedata.instance.health -= 1;
                }
            }
            else if (collision.gameObject.tag=="boss1")
            {
                if (state == State.fall)
                {
                    jump();
                    Instantiate(enemydead, collision.gameObject.transform.position, Quaternion.identity);
                    collision.gameObject.GetComponent<boss1health>().takedamage();
                   
                  Rigidbody2D boss1rb =collision.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 boss1jump = new Vector2(boss1rb.velocity.x, 5);
                    boss1rb.velocity = boss1jump;
                }
                else
                {

                    state = State.hurt;
                    savedata.instance.health -= 1;
                }

            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spiderboss")
        {
            if (state == State.fall)
            {
                //collision.gameObject.GetComponent<bosslife>().takedamage();
                state = State.jump;
                
                Vector2 up = new Vector2(rb.velocity.x, 2 * 10 );
                rb.velocity = up;
            }
            else
            {
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    Vector2 up = new Vector2(-2 * 5, rb.velocity.y);
                    rb.velocity = up;
                }
                else
                {
                    Vector2 up = new Vector2(2 * 5, rb.velocity.y);
                    rb.velocity = up;
                }
            }

        }
      
        else if (collision.gameObject.tag == "liver")
        {
            
            
                state = State.fall;

                mobilejump();
            
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "liver")
        {


            state = State.fall;
            mobilejump();


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            gun.SetActive(true);
            fire.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "healthbox")
        {
            savedata.instance.health += 2;
            Destroy(collision.gameObject);
            coinsound.Play();


        }
        if (poweruppartical.activeSelf == true)
        {
            if (collision.gameObject.tag == "web")
            {
                jump();
                Instantiate(enemydead, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                deadsound.Play();
            }

        }
        else if (collision.gameObject.tag == "web")
        {
            hit.Play();
            state = State.hurt;
            if (collision.gameObject.transform.position.x > transform.position.x)
            {


                Destroy(collision.gameObject);
                rb.velocity = new Vector2(-hurtforce, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                Destroy(collision.gameObject);
            }
            savedata.instance.health -= 1;
            //powerup
            powerup.instance.fill.fillAmount -= 0.1f;
        }

        if (collision.gameObject.tag == "coin")
        {


            coinsound.Play();
            powerup.instance.fill.fillAmount += powerup.instance.coin / 100;
            Destroy(collision.gameObject);
        }

    }
    
  /// <summary>
  /// laptop
  /// </summary>
    private void move()
    {
        if (Input.GetKey("a"))
        {
            if (!coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(-5 * speed, rb.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y);
            }
            
        }
        else if (Input.GetKey("d") )
        {
            if (!coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(5 * speed, rb.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
           
        }
      
    }
    
    private void animationmove()
    {
        
        if (state == State.jump)
        {
            if (rb.velocity.y < .1)
            {
                state = State.fall;
            }
        }
        else if (state==State.fall)
        {
            if (state == State.fall && colc.IsTouchingLayers(ground))
            {
                state = State.playeridle;
            } 
        }
        else if (state == State.hurt)
        {
           
        
       if ( Mathf.Abs(rb.velocity.x)<.1)
            {
                state = State.playeridle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > .1f)
        {
            
            if (gun.activeSelf==false)
            {

                state = State.run;
            }
            else if (gun.activeSelf == true)
            {
                state = State.weaponrun;
            }
        }
        else
            state = State.playeridle;
    }
    /// mobile
    /// 



    void handler()
    {
        if (dontmove==true)
        {
            if (state != State.hurt)
            {
                stop();
            }

        }



        else
        {
            if (moveleft==true)
            {
                if (state != State.hurt)
                {
                    mobileinputa();
                }

            }
            else if (!moveleft==true)
            {
                if (state != State.hurt)
                {
                    mobileinputd();
                }
            }
        }

       

    }
    void mobileinputa()
    {
        
        {///powerup
            if (powerup.instance.reducepow==true)
            {

                if (!coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector2(-1, 1);
                    rb.velocity = new Vector2(-7 * speed, rb.velocity.y);
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }

            }
            ////powerdown
           else 
            {
                ///
                if (!coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector2(-1, 1);
                    rb.velocity = new Vector2(-5 * speed, rb.velocity.y);
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
        }
    }
   void mobileinputd()
    {
        //powerup
        if (powerup.instance.reducepow == true)
        {
            if (!coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(7 * speed, rb.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }

        }
        ///powerdown
        else
        {
            if (!coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(5 * speed, rb.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }

        }
    }

  public  void allowmove(bool move)
    {



        
            dontmove = false;
            moveleft = move;

            
            
        
        
    }


 void stop()
    {
        
        rb.velocity = new Vector2(0,rb.velocity.y);
    }  
  public  void donotmove()
    {
        
        {
            dontmove = true;
           
        }

    }
  public   void mobilejump()
    {
      
       
         if (colc.IsTouchingLayers(ground)||coll.IsTouchingLayers(liver))
        {
            jumpsound.Play();
            jump();
        }
    }

   
    /// save
    /// 

  

}
