using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectilesAbsorption : MonoBehaviour
{
    public ScaleToObjectSize scaleToObjectSize;

    public ShieldHealth thisObjectShieldHealth;


    private RandomAudioPlayerV2 shieldSource;


    public void InstantiateStart() {
        try {
            thisObjectShieldHealth = GetShieldHealth(scaleToObjectSize.ragdollControllerFound.gameObject);
        } catch (System.Exception) {
            thisObjectShieldHealth = GetShieldHealth(scaleToObjectSize.parent);
        }
         shieldSource = thisObjectShieldHealth.GetComponentInChildren<RandomAudioPlayerV2>();
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
            SlimeArm.insance.shieldSoundBank.PlayRandomClip(defaultBankIndex: 1);
            Destroy(thisObjectShieldHealth.gameObject);
        }
        else
        {
            SlimeArm.insance.shieldSoundBank.PlayRandomClip(defaultBankIndex: 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
