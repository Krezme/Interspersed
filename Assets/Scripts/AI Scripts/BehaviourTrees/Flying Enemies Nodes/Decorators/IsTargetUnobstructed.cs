using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsTargetUnobstructed : DecoratorNode
{

    public float raycastLenght = 100;

    public Vector3 targetPositionOffset;

    public LayerMask targetLayer;

    private RaycastHit hit;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector3 rayDirection = ((context.playerObject.transform.position + targetPositionOffset) - context.gameObject.transform.position).normalized;
        Debug.DrawLine(context.gameObject.transform.position, context.gameObject.transform.position + (rayDirection * raycastLenght), Color.blue);
        if (Physics.Raycast(context.gameObject.transform.position, rayDirection, out hit , raycastLenght)) {
            if (1<< hit.transform.gameObject.layer == targetLayer.value) {
                blackboard.directionToTarget = rayDirection;
                return RunChildren();
            }
        }
        return State.Running;
    }

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
