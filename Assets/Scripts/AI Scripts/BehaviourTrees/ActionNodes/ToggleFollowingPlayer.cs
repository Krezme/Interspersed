using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ToggleFollowingPlayer : ActionNode
{
    public bool newState;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.isFollowingPlayer = newState;
        blackboard.playerInAttackRange = newState;

        return State.Success;
    }
}
