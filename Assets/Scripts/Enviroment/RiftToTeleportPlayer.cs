using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftToTeleportPlayer : MonoBehaviour
{

    public GameObject riftDestination;

    public float teleportDelay;

    public bool enableRift;

    public bool teleportingToAnEvent;

    private bool isInTeleporter = false;

    private bool needsTeleportation;

    private float teleportTimePassed;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            isInTeleporter = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            isInTeleporter = false;
            needsTeleportation = false;
        }
    }

    void Update () {
        if (isInTeleporter) {
            teleportTimePassed += Time.deltaTime;
            if (teleportTimePassed >= teleportDelay) {
                needsTeleportation = true;
            }
        }else {
            teleportTimePassed = 0;
        }
    }

    void FixedUpdate() {
        if (needsTeleportation) {
            Teleport();
        }
    }

    void Teleport() {
        if (enableRift) {
            riftDestination.SetActive(true);
        }
        SaveData.instance.ToggleInMiddleOfAnEvent(teleportingToAnEvent);
        ThirdPersonPlayerController.instance.gameObject.transform.position = riftDestination.transform.position;
        ThirdPersonPlayerController.instance.verticalVelocity = 0;
        isInTeleporter = false;
        riftDestination.GetComponent<WarpCloseSequence>().ToggleFadeOut(true);
        Destroy(this.gameObject);
    }
}
