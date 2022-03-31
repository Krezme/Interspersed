using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AdvancedRandomSelector : CompositeNode
{

    // number of children and what odds of running the coresponding child
    public int[] numberOfChildrenAndTheirOdds;

    // List to check which random child is selected
    private List<int> allChildrenAndTheirOdds = new List<int>();

    // child selected
    int current;

    protected override void OnStart() {
        // Preparing for randomisation
        for (int i = 0; i < numberOfChildrenAndTheirOdds.Length; i++) {
            for (int j = 0; j < numberOfChildrenAndTheirOdds[i]; j++) {
                allChildrenAndTheirOdds.Add(i);
            }
        }
        // Randomise and setting child to run
        current = allChildrenAndTheirOdds[Random.Range(0, allChildrenAndTheirOdds.Count)];
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        var child = children[current];
        return child.Update(); // running child
    }
}
