using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    public Rigidbody rigidbody;
    private CharacterController controller;
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            controller = col.GetComponent<CharacterController>();
        }
    }

    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Player") {
            controller.Move(rigidbody.velocity * Time.deltaTime);
        }
    }

    void OnTriggerExit (Collider col) {
        if (col.gameObject.tag == "Player") {
            controller = new CharacterController();
        }
    }
}
