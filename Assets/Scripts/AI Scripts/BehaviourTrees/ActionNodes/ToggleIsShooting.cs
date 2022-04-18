using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleIsShooting : ActionNode
{
    public bool isShooting;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // Setting the blackboard to the variable
        blackboard.isCurrentlyShooting = isShooting;
        return State.Success;
    }
}
