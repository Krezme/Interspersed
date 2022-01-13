using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    // This rigidbody
    public Rigidbody rigidbody;

    // The character controller that needs to be parented
    private CharacterController controller;

    /// <summary>
    /// Getting the controller when the player enters
    /// </summary>
    /// <param name="col">The other collider</param>
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            controller = col.GetComponent<CharacterController>();
        }
    }

    /// <summary>
    /// When the player is staying they move with the platform
    /// </summary>
    /// <param name="col">The other collider</param>
    void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Player") {
            controller.Move(rigidbody.velocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Restarting the controller
    /// </summary>
    /// <param name="col">The other collider</param>
    void OnTriggerExit (Collider col) {
        if (col.gameObject.tag == "Player") {
            controller = new CharacterController();
        }
    }
}
