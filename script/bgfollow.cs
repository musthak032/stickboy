using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgfollow : MonoBehaviour
{
    public float effect;
    public float move;
     Transform cam;
    Vector3 lastcampos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        lastcampos = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltamove = cam.position - lastcampos;
        transform.position += deltamove*move;
        lastcampos = cam.position;
    }
}
