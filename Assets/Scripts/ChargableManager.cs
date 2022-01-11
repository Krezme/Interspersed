using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableManager : MonoBehaviour
{
    public bool canRunOnlyOnce = true;

    [HideInInspector]
    public bool usedOnce = false;
    
    public virtual void OnCharged() {
        if (usedOnce) {
            return;
        }
        if (canRunOnlyOnce) {
            usedOnce = true;
        }
    }
}
