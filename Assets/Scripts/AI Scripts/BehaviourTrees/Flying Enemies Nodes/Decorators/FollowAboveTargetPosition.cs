using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class FollowAboveTargetPosition : DecoratorNode
{

    public float heightAboveTarget = 10f;

    private float randomisedHeightOffset;

    protected override void OnStart() {
        randomisedHeightOffset = (heightAboveTarget);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.moveToPosition = new Vector3(context.playerObject.transform.position.x, context.playerObject.transform.position.y + randomisedHeightOffset, context.playerObject.transform.position.z);
        return RunChildren();
    }

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
