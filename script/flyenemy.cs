using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyenemy : MonoBehaviour
{
    // private Collider2D col;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }*/
    }

}
