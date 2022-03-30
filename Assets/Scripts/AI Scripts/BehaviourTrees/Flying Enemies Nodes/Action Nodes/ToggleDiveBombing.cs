using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleDiveBombing : ActionNode
{
    public bool isDiveBombing;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.isCurrentlyDiveBombing = isDiveBombing;
        return State.Success;
    }
}
