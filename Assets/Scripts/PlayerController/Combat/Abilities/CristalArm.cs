using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CristalArm : PlayerAbility
{
    public Transform spawnBulletPosition;
    public GameObject[] pfBulletProjectile;
    public AudioSource audioSource;
    public AudioClip abilitySound;
    private GameObject currentBullet;

    public float[] chargeStages;

    public float projectileDamage; // This is a temp variable and will be changed when we decide if we are going with scritable objects or an other method

    private float time;

    private float timePassed;

    public GameObject changeToArm;

    public override void MorthToTarget()
    {
        base.MorthToTarget();
        changeToArm.SetActive(!changeToArm.activeSelf);
    }

    public override void AimingAbility ()
    {
        timePassed += Time.deltaTime;
        for (int i = chargeStages.Length -1; i >= 0; i--) {
            if (chargeStages[i] <= timePassed) {
                Debug.Log(i + " stage charged");
                currentBullet = pfBulletProjectile[i+1];
                projectileDamage *= i + 1;
                // finctionality depending on different charge stage
                return;
            }else {
                currentBullet = pfBulletProjectile[0];
            }
        }
        
    }

    public override void AditionalAbilities() {
        if (!OnPlayerInput.instance.onFire1) {
            
            if (timePassed > 0) {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                GameObject bullet = Instantiate(currentBullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                bullet.GetComponent<BulletProjectile>().damage = projectileDamage;
                OnPlayerInput.instance.onFire1 = false;
                audioSource.PlayOneShot(abilitySound);
            }
            projectileDamage = 10;
            timePassed = 0;
        }
    }
}
