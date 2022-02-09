using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarplingAnimationManager : EnemyAnimationManager
{
    public Collider molarCollider; // The collider for weapon 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWeaponCollider()
    {
        molarCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        molarCollider.enabled = false;
    }
}
