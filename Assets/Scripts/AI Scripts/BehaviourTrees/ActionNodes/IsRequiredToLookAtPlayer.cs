using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsRequiredToLookAtPlayer : ActionNode
{

    public bool isRequiredToLookAtPlayer;

    protected override void OnStart() {
        blackboard.requiredToLookAtPlayer = isRequiredToLookAtPlayer;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
