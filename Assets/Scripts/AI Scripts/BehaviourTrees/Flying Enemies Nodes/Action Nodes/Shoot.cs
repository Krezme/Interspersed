using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Shoot : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.enemyAttacksManager.ShootProjectile(blackboard.directionToTarget); // Shooting projectile
        return State.Success;
    }
}
