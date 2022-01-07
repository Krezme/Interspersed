using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMeleeColController : MonoBehaviour
{
    public BoxCollider armCollider;

    public ArmColTriggers  armColTriggers;

    public void EnableMelee() {
        armColTriggers.enemiesHit = new List<GameObject>();
        armCollider.enabled = true;
    }

    public void DissableMelee() {
        armCollider.enabled = false;
    }
}
