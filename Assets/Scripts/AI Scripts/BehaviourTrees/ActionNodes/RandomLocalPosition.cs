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
        //Vector3 newDest = new Vector3(Random.Range(-wanderRange, wanderRange) + context.transform.position.x, 0f, Random.Range(-wanderRange, wanderRange) + context.transform.position.z);
        Vector3 newDest;
        Vector3 randomPoint = context.transform.position + Random.insideUnitSphere * wanderRange; /// picks a random location within the sphere
        if(NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas)) /// Finds a suitable place to walk to
        {
            newDest = navHit.position;
            blackboard.moveToPosition = newDest;
            return State.Success;
        }
        return State.Running;
    }
}
