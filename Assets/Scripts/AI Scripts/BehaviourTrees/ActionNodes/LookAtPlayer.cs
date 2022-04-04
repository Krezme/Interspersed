using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class LookAtPlayer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        context.gameObject.transform.LookAt(context.playerObject.transform); /// makes the AI look at the player
        return State.Success;
    }
}
