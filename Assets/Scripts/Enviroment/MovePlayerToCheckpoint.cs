using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToCheckpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log(ThirdPersonPlayerController.instance.gameObject.transform.position);
            ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].playerSpawnPos.position + CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].offset;
            ThirdPersonPlayerController.instance.verticalVelocity = 0;
            Debug.Log(ThirdPersonPlayerController.instance.gameObject.transform.position);
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].playerSpawnPos.position + CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].offset;
        }
    }*/
}
