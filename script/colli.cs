using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colli : MonoBehaviour
{
    [SerializeField] private AudioSource deadsound;
    public GameObject enemydead;
    public LayerMask ground;
  //  public GameObject laser;
    
    Transform dusts;
    // Start is called before the first frame update
    void Start()
    {
       //dusts=dust.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       
           

  

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            deadsound.Play();
            Instantiate(enemydead, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
       else if (collision.gameObject.tag == "ground")
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "spiderboss")
        {
            collision.gameObject.GetComponent<bosslife>().healthui.fillAmount -= 0.05f;
            Instantiate(enemydead, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
