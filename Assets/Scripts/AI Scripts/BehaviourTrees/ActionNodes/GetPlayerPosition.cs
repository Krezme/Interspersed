using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GetPlayerPosition : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        blackboard.moveToPosition = context.playerObject.transform.position;
        return State.Success;
       
    }
}
