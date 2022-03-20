using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrickenAnimationManager : EnemyAnimationManager
{
    public Collider sunderbussCollider; // The collider for weapon 

    public void EnableWeaponCollider()
    {
        sunderbussCollider.isTrigger = true;
        sunderbussCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        sunderbussCollider.isTrigger = true;
        sunderbussCollider.enabled = false;
    }
}
