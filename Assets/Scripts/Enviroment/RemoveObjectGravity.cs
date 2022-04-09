using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectGravity : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged")
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().drag = 0;

            //Debug.Log("Object Gravity Removed");
        }
    }
}
