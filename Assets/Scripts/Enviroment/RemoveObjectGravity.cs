using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectGravity : MonoBehaviour
{
   

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Throwable")
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("Object Gravity Removed");
        }
    }
}
