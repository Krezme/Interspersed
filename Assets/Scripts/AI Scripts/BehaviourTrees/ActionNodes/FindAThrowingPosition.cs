using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class FindAThrowingPosition : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        LayerMask targetMask = context.fieldOfView.targetMask;

        blackboard.playerInAttackRange = Physics.CheckSphere(context.agent.transform.position, context.fieldOfView.viewRadius, targetMask);

        if (blackboard.isFollowingPlayer)
        {
            if (blackboard.playerInAttackRange)
            {
                blackboard.moveToPosition = context.playerObject.transform.position;
                return State.Running;
            }
        }
        return State.Success;
    }
}
