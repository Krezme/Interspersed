using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallenInVoid : MonoBehaviour
{
    public float verticalFallingDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ThirdPersonPlayerController.instance.gameObject.transform.position.y <= verticalFallingDistance)
        {
            ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].playerSpawnPos.position + CheckpointManager.instance.checkpoints[CheckpointManager.instance.currentCheckpointIndex].offset;
            ThirdPersonPlayerController.instance.verticalVelocity = 0;
        }
    }
}
