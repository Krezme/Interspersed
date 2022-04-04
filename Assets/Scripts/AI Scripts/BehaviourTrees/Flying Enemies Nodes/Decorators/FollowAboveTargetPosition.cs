using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class FollowAboveTargetPosition : DecoratorNode
{

    // Additional height to make the enemy hover above player
    public float heightAboveTarget = 10f;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // Updating the move to position every frame
        blackboard.moveToPosition = new Vector3(context.playerObject.transform.position.x, context.playerObject.transform.position.y + heightAboveTarget, context.playerObject.transform.position.z);
        return RunChildren(); // Returns child State
    }

    /// <summary>
    /// Run child nodes
    /// </summary>
    /// <returns></returns>
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
