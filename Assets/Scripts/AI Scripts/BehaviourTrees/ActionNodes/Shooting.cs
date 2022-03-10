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

        if (blackboard.distance <= 1f)
        {
            context.enemyshoot.FireBullet();
            return State.Success;
        }
        else
        {
            return State.Failure;
        }


        
        
    }
}
