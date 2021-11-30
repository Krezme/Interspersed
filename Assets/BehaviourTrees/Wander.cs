using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TheKiwiCoder;

public class Wander : ActionNode
{

    protected override void OnStart() {
        
        blackboard.owner.GetComponent<Enemy_NavWander>();
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
