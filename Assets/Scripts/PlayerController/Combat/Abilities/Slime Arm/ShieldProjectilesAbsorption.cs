using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectilesAbsorption : MonoBehaviour
{
    public ScaleToObjectSize scaleToObjectSize;

    public ShieldHealth thisObjectShieldHealth;

    public void InstantiateStart() {
        try {
            thisObjectShieldHealth = GetShieldHealth(scaleToObjectSize.ragdollControllerFound.gameObject);
        } catch (System.Exception) {
            thisObjectShieldHealth = GetShieldHealth(scaleToObjectSize.parent);
        }
    }

    ShieldHealth GetShieldHealth(GameObject gO) {
        if (gO.TryGetComponent<ShieldHealth>(out ShieldHealth shieldHealthFound)) {
            return shieldHealthFound;
        }
        else {
            ShieldHealth shieldHealthAdded = gO.AddComponent<ShieldHealth>();
            shieldHealthAdded.SetShieldHealth();
            return shieldHealthAdded;
        }
    }

    public void TakeDamage(float damage) {
        thisObjectShieldHealth.health -= damage;
        if (thisObjectShieldHealth.health <= 0) {
            Destroy(thisObjectShieldHealth.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
