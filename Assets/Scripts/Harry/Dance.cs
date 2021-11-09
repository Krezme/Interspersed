using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{

    public Animator knop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        { 

            knop.SetBool("Aiming", true);
        }
        if (Input.GetKey(KeyCode.F))
        {
            knop.SetBool("Aiming", true);

        }

        if(Input.GetKeyUp(KeyCode.F))
        {
            knop.SetBool("Aiming", false);

        }







    }
}
