using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsInMeleeRange : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        blackboard.distance = Vector2.Distance(context.gameObject.transform.position, context.playerObject.transform.position);
        if (blackboard.distance <= blackboard.meleeRange)
        {
            var state = child.Update();
            return state;
        }
        else
        {
            return State.Failure;
        }

    }
}
