using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HasSeenTarget : DecoratorNode
{

    public bool playOnFoundTarget;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (playOnFoundTarget && context.fieldOfView.targetvisible) {
            return RunChildren();
        }
        else if (!playOnFoundTarget && !context.fieldOfView.targetvisible) {
            return RunChildren();
        }
        else {
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
