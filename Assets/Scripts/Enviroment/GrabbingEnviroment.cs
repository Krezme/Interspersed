using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingEnviroment : MonoBehaviour
{
    public bool isAlredySpawned;
    public GameObject[] unparentObjects;
    public GameObject objectToGrab;

    public Rigidbody GrabObject() {
        for (int i = 0; i < unparentObjects.Length; i++) {
            unparentObjects[i].transform.parent = null;
        }
        if (isAlredySpawned) {
            Destroy(transform.GetComponent<Collider>());
            objectToGrab.AddComponent<Rigidbody>();
            objectToGrab.AddComponent<BoxCollider>();
        }
        return objectToGrab.GetComponent<Rigidbody>();
    }
}
