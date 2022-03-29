using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class StartCarrierAttackCooldown : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.carrierAttacksManager.attackCurrentCooldown = context.carrierAttacksManager.attackCooldown;
        return State.Success;
    }
}
