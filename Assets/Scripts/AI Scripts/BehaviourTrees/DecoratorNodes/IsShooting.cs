using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsShooting : DecoratorNode
{
    public bool playWhenShooting;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (playWhenShooting && blackboard.isCurrentlyShooting)
        {
            return RunChildren();
        }
        else if (!playWhenShooting && !blackboard.isCurrentlyShooting)
        {
            return RunChildren();
        }
        else
        {
            return State.Failure;
        }
    }

    /// <summary>
    /// Runs the children
    /// </summary>
    /// <returns> Child Node State </returns>
    State RunChildren()
    {
        switch (child.Update())
        {
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
