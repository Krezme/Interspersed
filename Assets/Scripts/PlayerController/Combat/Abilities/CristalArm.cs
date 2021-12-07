using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

[System.Serializable]
public class CrystalArmStatistics
{
    public float maxElectricShots;
    public float currentElectricShots;
    public float rechargeTime;
    public float currentRechrge;
    public bool isElectric;
}

public class CristalArm : PlayerAbility
{
    public CrystalArmStatistics statistics;

    public Transform spawnBulletPosition;

    public GameObject[] pfBulletProjectile;

    public AudioSource audioSource;

    public AudioClip abilitySound;

    public GameObject changeToArm;

    public Slider crosshair;

    public float[] chargeStages;

    public float projectileDamage; // This is a temp variable and will be changed when we decide if we are going with scritable objects or an other method

    private float timePassed;

    private float currentChargeStage;

    private GameObject currentBullet;

    void Update () {
        ElectricChargeCooldown();
    }

    /// <summary>
    /// If the currentElectricShots are not at capacity the timer is going to start and recarge them
    /// </summary>
    private void ElectricChargeCooldown () {
        if (statistics.currentElectricShots < statistics.maxElectricShots){
            statistics.currentRechrge += Time.deltaTime;
            if (statistics.currentRechrge >= statistics.rechargeTime) {   
                statistics.currentElectricShots++;
                statistics.currentRechrge = 0;
            }
        }
    }

    public override void MorthToTarget()
    {
        base.MorthToTarget();
        changeToArm.SetActive(!changeToArm.activeSelf);
    }

    public override void AimingAbility ()
    {
        timePassed += Time.deltaTime;
        if (crosshair != null) {
            crosshair.value = timePassed / chargeStages[chargeStages.Length - 1];
        }
        for (int i = chargeStages.Length -1; i >= 0; i--) {
            if (chargeStages[i] <= timePassed) {
                Debug.Log(i + " stage charged");
                currentBullet = pfBulletProjectile[i+1];
                currentChargeStage = i;
                // finctionality depending on different charge stage
                return;
            }else {
                currentBullet = pfBulletProjectile[0];
                currentChargeStage = 0;
            }
        }
        
    }

    public override void AditionalAbilities() {
        if (OnPlayerInput.instance.onAbility1 && statistics.currentElectricShots > 0) { // Toggle the electric ability
            statistics.isElectric = !statistics.isElectric;
            OnPlayerInput.instance.onAbility1 = false;
        } 
        if (!OnPlayerInput.instance.onFire1) {
            
            if (timePassed > 0) {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                GameObject bullet = Instantiate(currentBullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                BulletProjectile newBulletProjectile = bullet.GetComponent<BulletProjectile>();
                newBulletProjectile.statistics.damage = projectileDamage * (currentChargeStage +1);
                newBulletProjectile.statistics.chargeStage = currentChargeStage + 1;
                newBulletProjectile.statistics.isElectric = statistics.isElectric; // setting the projectile to electric 
                if (statistics.isElectric) { // Decreasing charged shots
                    statistics.currentElectricShots--;
                }
                statistics.isElectric = false;
                OnPlayerInput.instance.onFire1 = false;
                audioSource.PlayOneShot(abilitySound);
            }
            if (crosshair != null){
                crosshair.value = 0;
            }
            projectileDamage = 10;
            timePassed = 0;
        }
    }
}
