using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsInAttackRangeAN : ActionNode
{
    public float attackRange; /// can adjust the attack range in the behaviour tree

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        /// Gets the distance betweem the player and AI 
        blackboard.distance = Vector3.Distance(context.gameObject.transform.position, context.playerObject.transform.position);

        if (blackboard.distance <= attackRange) /// Compares distance to attack range
        {
            return State.Success;
        }
        else /// otherwise it fails this check
        {
            return State.Failure;
        }
    }
}
