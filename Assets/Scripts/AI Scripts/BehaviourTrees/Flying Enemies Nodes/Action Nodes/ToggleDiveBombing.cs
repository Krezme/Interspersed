using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleDiveBombing : ActionNode
{
    // update the blackboard with this value
    public bool isDiveBombing;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // Setting the blackboard to the variable
        blackboard.isCurrentlyDiveBombing = isDiveBombing;
        return State.Success;
    }
}
