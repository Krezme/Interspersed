using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToCheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("APIFJIAEJFIPJAEPFJAOPEJFOPAJE");
            ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].playerSpawnPos.position + CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].offset;
        }
    }
}
