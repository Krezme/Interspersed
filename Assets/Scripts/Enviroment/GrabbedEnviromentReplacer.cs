using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedEnviromentReplacer : MonoBehaviour
{
    public GameObject newReplacingGameObject;
    public Rigidbody Replace() {
        
        GameObject newGameObject = Instantiate(newReplacingGameObject, transform.position, transform.rotation);
        Rigidbody newGameObjectRB = newGameObject.GetComponent<GrabbingEnviroment>().GrabObject();
        Destroy(newGameObject.GetComponent<GrabbingEnviroment>());
        return newGameObjectRB;
    }
}
