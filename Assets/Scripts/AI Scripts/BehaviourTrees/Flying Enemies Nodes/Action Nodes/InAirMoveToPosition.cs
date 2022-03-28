using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class InAirMoveToPosition : ActionNode
{

    public float distanceThreshold = 3;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Setting the move to position and the In Air Pathfinding 
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate() {
        context.characterInAirPathFinding.moveToPosition = blackboard.moveToPosition;

        if (Vector3.Distance(context.transform.position, blackboard.moveToPosition) <= distanceThreshold) {
            return State.Success;
        }
        return State.Running;
    }
}
