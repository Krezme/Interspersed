using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Animation : ActionNode
{
    public float floatEntry;
    public int intEntry;
    public bool boolEntry;

    public bool setFloat, setInt, setBool, setTrigger;

    public string floatName, intName, boolName, triggerName;


    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (setFloat)
        {
            context.animator.SetFloat(floatName,floatEntry);
        }
        if (setInt)
        {
            context.animator.SetInteger(intName, intEntry);
        }
        if (setBool)
        {
            context.animator.SetBool(boolName, boolEntry);
        }
        if (setTrigger)
        {
            context.animator.SetTrigger(triggerName);
        }
        return State.Success;
    }
}
