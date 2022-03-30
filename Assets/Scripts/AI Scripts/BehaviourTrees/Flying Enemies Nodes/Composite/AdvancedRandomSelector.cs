using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AdvancedRandomSelector : CompositeNode
{

    public int[] numberOfChildrenAndTheirOdds;

    private List<int> allChildrenAndTheirOdds = new List<int>();

    int current;

    protected override void OnStart() {
        for (int i = 0; i < numberOfChildrenAndTheirOdds.Length; i++) {
            for (int j = 0; j < numberOfChildrenAndTheirOdds[i]; j++) {
                allChildrenAndTheirOdds.Add(i);
            }
        }
        current = allChildrenAndTheirOdds[Random.Range(0, allChildrenAndTheirOdds.Count)];
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        var child = children[current];
        return child.Update();
    }
}
