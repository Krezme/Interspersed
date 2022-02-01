using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class RandomLocalPosition : ActionNode
{

    public float wanderRange;

    private NavMeshHit navHit;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector3 newDest = new Vector3(Random.Range(-wanderRange, wanderRange) + context.transform.position.x, 0f, Random.Range(-wanderRange, wanderRange) + context.transform.position.z);
        blackboard.moveToPosition = newDest;
        return State.Success;
    }
}
