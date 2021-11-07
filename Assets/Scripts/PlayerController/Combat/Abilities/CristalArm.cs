using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CristalArm : PlayerAbility
{
    public Transform spawnBulletPosition;
    public GameObject pfBulletProjectile;
    public AudioSource audioSource;
    public AudioClip abilitySound;

    public float[] chargeStages;

    private float time;

    private float timePassed;

    public override void AimingAbility ()
    {
        timePassed += Time.deltaTime;
        for (int i = chargeStages.Length -1; i >= 0; i--) {
            if (chargeStages[i] <= timePassed) {
                Debug.Log(i + " stage charged");
                // finctionality depending on different charge stage
                return;
            }
        }
        
    }

    public override void AditionalAbilities() {
        if (!OnPlayerInput.instance.onFire1) {
            
            if (timePassed > 0) {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                OnPlayerInput.instance.onFire1 = false;
                audioSource.PlayOneShot(abilitySound);
            }

            timePassed = 0;
        }
    }
}
