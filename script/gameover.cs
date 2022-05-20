using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource gamesound;
    void Start()
    {
        gamesound = GetComponent<AudioSource>();
    }

    public void sound()
    {
        gamesound.Play();
    }
}
