using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class StartAttackCooldown : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.enemyAttacksManager.attackCurrentCooldown = context.enemyAttacksManager.attackCooldown; // Restarts attack cooldown
        return State.Success;
    }
}
