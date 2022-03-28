using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomInAirPosition : ActionNode
{

    public float wanderRadius;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Randomising the next position
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate() {
        blackboard.moveToPosition = context.characterInAirPathFinding.spawnedPostion + Random.insideUnitSphere * wanderRadius;
        return State.Success;
    }
}
