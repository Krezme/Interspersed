using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class InAirMoveToPosition : ActionNode
{
    // Distance between enemy and target position before the node is consideted complete
    public float distanceThreshold = 3;

    [Header("Changing movement statistics")]
    public bool stopInPlace; // if the enemy needs to do a full stop
    public bool changeMovementStats; // if the enemy's stats need changing from the default
    public InAirMovementStatistics newMovementStatistics; // The statistics used to change enemy's statistics

    /// <summary>
    /// Changing statistics if changeMovementStats is true
    /// </summary>
    protected override void OnStart() {
        if (changeMovementStats) {
            ChangeMovementStats();
        }else {
            context.characterInAirPathFinding.ResetMovementStatistics();
        }
    }

    protected override void OnStop() {
    }

    /// <summary>
    /// Setting the move to position and the In Air Pathfinding 
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate() {
        if (stopInPlace) {
            context.characterInAirPathFinding.moveToPosition = context.gameObject.transform.position;
        }else { 
            context.characterInAirPathFinding.moveToPosition = blackboard.moveToPosition;
        }

        if (Vector3.Distance(context.transform.position, blackboard.moveToPosition) <= distanceThreshold) {
            return State.Success;
        }
        return State.Running;
    }

    /// <summary>
    /// Sets the statistics to the InAirPathFinding script
    /// </summary>
    void ChangeMovementStats() {
        context.characterInAirPathFinding.currentMovementStatistics.maxSpeed = newMovementStatistics.maxSpeed;
        context.characterInAirPathFinding.currentMovementStatistics.minSpeed = newMovementStatistics.minSpeed;
        context.characterInAirPathFinding.currentMovementStatistics.changeSpeedMultiplier = newMovementStatistics.changeSpeedMultiplier;
        context.characterInAirPathFinding.currentMovementStatistics.rotationSpeed = newMovementStatistics.rotationSpeed;
        context.characterInAirPathFinding.currentMovementStatistics.stoppingDistance = newMovementStatistics.stoppingDistance;
    }
}
