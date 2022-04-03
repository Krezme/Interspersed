using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HasSeenPlayer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        if (context.fieldOfView.targetvisible) /// Checks to see if the player is visible using the field of view script (attatched to the AI object)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
