using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Shooting : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        context.enemyshoot.FireBullet(); /// runs a function in EnemyShoot Script (attatched to AI)
        return State.Success;
  
    }
}
