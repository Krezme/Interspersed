using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsCarrierDiveBombing : DecoratorNode
{

    // if the child is allowed to run depending if the carrier is currently dive bombing
    public bool playWhenDiveBombing;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Checks if the child is allowed to run depending on playWhenDiveBombing and the blackboard's isCurrentlyDiveBombing
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate() {
        if (playWhenDiveBombing && blackboard.isCurrentlyDiveBombing) {
            return RunChildren();
        }else if (!playWhenDiveBombing && !blackboard.isCurrentlyDiveBombing) {
            return RunChildren();
        }else {
            return State.Failure;
        }
    }

    /// <summary>
    /// Runs the children
    /// </summary>
    /// <returns> Child Node State </returns>
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
