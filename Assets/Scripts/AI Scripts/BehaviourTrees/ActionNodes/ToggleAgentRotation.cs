using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleAgentRotation : ActionNode
{
    public bool isUpdatingRotation;
    protected override void OnStart() {

        context.agent.updateRotation = isUpdatingRotation;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
