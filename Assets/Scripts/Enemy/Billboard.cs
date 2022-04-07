using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private GameObject cam;

    public bool needsToFlash = false;

    void Start () 
    {
        cam = FindObjectOfType<Camera>().gameObject;
        if (needsToFlash) {
            InvokeRepeating("FlashOff", 2.0f, 2.0f);
            InvokeRepeating("FlashOn", 1.5f, 1.5f);
        }
    }
   
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
    


    void FlashOn()
    {
        this.gameObject.SetActive(false);
    }
    void FlashOff()
    {
        this.gameObject.SetActive(true);
    }

        
}
