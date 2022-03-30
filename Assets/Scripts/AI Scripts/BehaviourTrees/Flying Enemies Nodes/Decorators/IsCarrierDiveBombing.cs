using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsCarrierDiveBombing : DecoratorNode
{

    public bool playWhenDiveBombing;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (playWhenDiveBombing && blackboard.isCurrentlyDiveBombing) {
            return RunChildren();
        }else if (!playWhenDiveBombing && !blackboard.isCurrentlyDiveBombing) {
            return RunChildren();
        }else {
            return State.Failure;
        }
    }

    State RunChildren() {
        switch (child.Update()) {
            case State.Running:
                break;
            case State.Failure:
                return State.Failure;
            case State.Success:
                return State.Success;    
        }
        return State.Running;
    }
}
