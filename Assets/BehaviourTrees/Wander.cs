using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TheKiwiCoder;

public class Wander : ActionNode
{
    private float checkRate;
    private float nextCheck;

    protected override void OnStart() {
        
        checkRate = Random.Range(0.3f, 0.4f);

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            blackboard.owner.GetComponent<Enemy_NavWander>().CheckIfIShouldWander();
        }
        blackboard.owner.GetComponent<Enemy_NavWander>().UpdateAnimations();
        return State.Success;
    }
}
