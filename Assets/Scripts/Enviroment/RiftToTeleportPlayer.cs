using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftToTeleportPlayer : MonoBehaviour
{

    public GameObject riftDestination;

    public float teleportDelay;

    private bool isInTeleporter = false;
    private bool needsTeleportation;

    private IEnumerator coroutine;

    public AudioSource sfxTeleport;

    void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player") {
            isInTeleporter = true;
            Debug.Log("Teleport Start");
            //Teleport();
            needsTeleportation = true;
            //coroutine = StartTeleportationWait();
            //StartCoroutine(coroutine);
        }
    }

    void FixedUpdate() {
        if (needsTeleportation) {
            Teleport();
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (needsTeleportation) {
                Teleport();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Debug 46");
            isInTeleporter = false;
            needsTeleportation = false;
        }
    }

    /* IEnumerator StartTeleportationWait () {
        yield return new WaitForSeconds(teleportDelay);
        if (isInTeleporter) 
            Teleport();
    } */

    void Teleport() {
        Debug.Log("Teleport " + riftDestination.transform.position);
        Debug.Log("player pos " + ThirdPersonPlayerController.instance.transform.position);
        ThirdPersonPlayerController.instance.gameObject.transform.position = riftDestination.transform.position;
        ThirdPersonPlayerController.instance.verticalVelocity = 0;
        isInTeleporter = false;
        Debug.Log("player pos 2 " + ThirdPersonPlayerController.instance.transform.position);
        sfxTeleport.Play();
    }


}
