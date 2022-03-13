using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPlayerVisible : DecoratorNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        RaycastHit hit;

        float viewRange = context.fieldOfView.viewRadius;

        if (Physics.Raycast(context.gameObject.transform.position, context.gameObject.transform.TransformDirection(Vector3.forward), out hit, viewRange, context.playerObject.layer))
        {
            Debug.DrawRay(context.gameObject.transform.position, context.gameObject.transform.TransformDirection(Vector3.forward) * viewRange, Color.green);
            var state = child.Update();
            return state;
        }
        else
        {
            return State.Failure;
        }
    }
}
