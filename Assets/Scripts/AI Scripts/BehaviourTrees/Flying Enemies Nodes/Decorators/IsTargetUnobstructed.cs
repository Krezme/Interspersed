using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsTargetUnobstructed : DecoratorNode
{
    // check raycast lenght
    public float raycastLenght = 100;

    // The offset to make sure the direction of the ray cast is going to hit the player
    public Vector3 targetPositionOffset;

    // The target layer that is checked for
    public LayerMask targetLayer;

    // The hit information from the raycast
    private RaycastHit hit;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Raycasts and allows children to run when the area is clear
    /// </summary>
    /// <returns> Node State </returns>
    protected override State OnUpdate() {
        // Calculated direction to target with the offset
        Vector3 rayDirection = ((context.playerObject.transform.position + targetPositionOffset) - context.enemyAttacksManager.projectileSpawnPos.position).normalized;
        // Debug.DrawLine(context.gameObject.transform.position, context.gameObject.transform.position + (rayDirection * raycastLenght), Color.blue);
        // Performing the Raycast 
        if (Physics.Raycast(context.gameObject.transform.position, rayDirection, out hit , raycastLenght)) {
            if (1<< hit.transform.gameObject.layer == targetLayer.value) { // comparing the layers 
                blackboard.directionToTarget = rayDirection;
                return RunChildren();
            }
        }
        return State.Running;
    }

    /// <summary>
    /// Runs Child node
    /// </summary>
    /// <returns> Child Node State </returns>
    State RunChildren() {
        switch (child.Update()) {
            case State.Running:
                break;
            case State.Failure:
                return State.Failure;
            case State.Success:
                return State.Success;    
        }
        return State.Running;
    }
}
