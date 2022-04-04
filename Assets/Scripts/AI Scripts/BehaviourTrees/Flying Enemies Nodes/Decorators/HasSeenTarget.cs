using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HasSeenTarget : DecoratorNode
{
    // if the node is supposed to allow children to run depending if player is detected
    public bool playOnFoundTarget;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Running Children depening on playOnFoundTarget value
    /// </summary>
    /// <returns> Node State </returns>
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

    /// <summary>
    /// Start child node
    /// </summary>
    /// <returns> Node State </returns>
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
