using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsInAttackRange : DecoratorNode
{
    public float attackRange;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        /// Gets the distance betweem the player and AI 
        blackboard.distance = Vector2.Distance(context.gameObject.transform.position, context.playerObject.transform.position);

        if (blackboard.distance <= attackRange) /// Compares distance to attack range
        {
            var state = child.Update(); /// returns the state of the child node (the node attatched to this)
            return state;
        }
        else
        {
            return State.Failure;
        }
    }
}
