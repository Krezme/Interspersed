using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomInAirPosition : ActionNode
{

    public float wanderRadius;

    [Range(0f,1f)]
    public float sphereHeightMultiplier = 1f;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Randomising the next position
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate() {
        Vector3 randomPositionInSphere = Random.insideUnitSphere * wanderRadius;
        blackboard.moveToPosition = context.characterInAirPathFinding.spawnedPostion + new Vector3(randomPositionInSphere.x, randomPositionInSphere.y * sphereHeightMultiplier, randomPositionInSphere.z);
        return State.Success;
    }
}
