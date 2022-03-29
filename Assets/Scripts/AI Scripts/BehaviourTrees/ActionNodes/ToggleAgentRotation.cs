using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleAgentRotation : ActionNode
{
    public bool isUpdatingRotation;
    protected override void OnStart() {

        context.agent.updateRotation = isUpdatingRotation; /// updates the rotation as it's moving
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        return State.Success;

    }
}
