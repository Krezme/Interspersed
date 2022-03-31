using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsCarrierAttackCooldownOver : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Proceeds with the children if attack cooldown is over
    /// </summary>
    /// <returns> Node State </returns>
    protected override State OnUpdate() {
        if (context.carrierAttacksManager.attackCurrentCooldown <= 0) {
            return RunChildren();
        }
        return State.Failure;
    }

    /// <summary>
    /// Runs the children
    /// </summary>
    /// <returns></returns>
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
