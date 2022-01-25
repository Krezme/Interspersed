using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

[System.Serializable]
public class CrystalArmStatistics
{
    public float damage;
    public float maxElectricShots;
    public float currentElectricShots;
    public float rechargeTime;
    public float currentRechrge;
    public bool isElectric;
}

[System.Serializable]
public class CrystalArmShotgunStatistics{
    public float damagePerPellet;
    public int projectileCount;
    public Vector2 projectileDispersion; //The spread of the projectiles
    public float shotgunCooldown;
}

public class CristalArm : PlayerAbility
{
    public CrystalArmStatistics statistics;

    public CrystalArmShotgunStatistics shotgunStatistics;

    public CrystalArmModes crystalArmModes;

    public Transform spawnBulletPosition;

    public GameObject[] pfBulletProjectileDef; //The prefabs for the single shot

    public GameObject pfPelletProjectileShotgun; //The prefab for the shotgun projectile

    public AudioSource audioSource;

    public AudioClip abilitySound;

    public GameObject changeToArm;

    public Slider crosshair;

    public float[] chargeStages;

    public float projectileDamage; // This is a temp variable and will be changed when we decide if we are going with scritable objects or an other method

    private float timePassed;

    private float cooldownBetweenShots;

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
            for (int i = CrosshairReferences.instance.chargesUI.Length -1; i >= 0 ; i--) {
                if (i > statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = 0;
                }
                else if (i == statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = statistics.currentRechrge / statistics.rechargeTime;
                }
                else if (i < statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = 1;
                }
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
        if (crystalArmModes == CrystalArmModes.Default) { 
            ChargeSingleShotAimingAbility();
        }
        else if (crystalArmModes == CrystalArmModes.Shotgun) { 
            ShotgunShotAimingAbility();
        }
    }

    void ChargeSingleShotAimingAbility() {
        timePassed += Time.deltaTime;
        if (crosshair != null) {
            crosshair.value = timePassed / chargeStages[chargeStages.Length - 1]; //Calculates the crosshair fill depending on the charges. It is chargeStages.Length - 1, because the first charge is default
        }
        for (int i = chargeStages.Length -1; i >= 0; i--) {
            if (chargeStages[i] <= timePassed) { //checking for the time and then setting the correct bullet prefab 
                currentBullet = pfBulletProjectileDef[i+1];
                currentChargeStage = i;
                // finctionality depending on different charge stage
                return;
            }else {
                currentBullet = pfBulletProjectileDef[0];
                currentChargeStage = 0;
            }
        }
    }

    void ShotgunShotAimingAbility() {
        if (cooldownBetweenShots <= 0) {
            for (int i = 0; i < shotgunStatistics.projectileCount; i++) {
                Vector3 aimDir = (centerScreenToWorldPosition - spawnBulletPosition.position).normalized;

                // The new Spawn Sosition of the pellet direction empty gameobject
                Vector3 newSpawnPos = new Vector3(UnityEngine.Random.Range(-1.0f * shotgunStatistics.projectileDispersion.x, 1.0f * shotgunStatistics.projectileDispersion.x), 
                    UnityEngine.Random.Range(-1.0f * shotgunStatistics.projectileDispersion.y, 1.0f * shotgunStatistics.projectileDispersion.y), 0);

                GameObject dispersionPoint = Instantiate(new GameObject(), spawnBulletPosition); //Spawning the empty gamebject that acts as the ditection spawn point of the object
                dispersionPoint.transform.localPosition = newSpawnPos; // Sets the local postion, of the direction gameobject, to the exact local posion related to the spawnBulletPosition
                dispersionPoint.transform.position += spawnBulletPosition.transform.forward*2; // Moves the direction gameobject forward to scale down the dispersion multiplication
                Vector3 newSpreadPoint = (dispersionPoint.transform.position - spawnBulletPosition.transform.position).normalized; //saves the new direction point
                Destroy(dispersionPoint);
                GameObject pellet = Instantiate(pfPelletProjectileShotgun, spawnBulletPosition.position, Quaternion.LookRotation(newSpreadPoint, Vector3.up)); // Instantiating the pellet going to the direction point
                
                //setting the projectile damage and speed multiplier
                BulletProjectile newPelletProjectile = pellet.GetComponent<BulletProjectile>();
                newPelletProjectile.statistics.damage = shotgunStatistics.damagePerPellet;
                newPelletProjectile.statistics.chargeStage = 1; 
            }
            
            audioSource.PlayOneShot(abilitySound);
            // Restarting the fire sequence
            OnPlayerInput.instance.onFire1 = false;
            cooldownBetweenShots = shotgunStatistics.shotgunCooldown;
        }
    }

    public override void AditionalAbilities() {
        ChangeMode();
        if (crystalArmModes == CrystalArmModes.Default) {
            ChargeShot();
            FiringTheProjectile();
        }
        if (crystalArmModes == CrystalArmModes.Shotgun) {
            ShotCooldown();
        }
    }

    void ChangeMode() {
        if (OnPlayerInput.instance.onArmMode) {
            if ((int)crystalArmModes == Enum.GetValues(typeof(CrystalArmModes)).Cast<int>().Max()){ // if the current item is the last item possible
                crystalArmModes = (CrystalArmModes)Enum.GetValues(typeof(CrystalArmModes)).Cast<int>().Min(); //set it to first item
            }
            else {
                crystalArmModes = (CrystalArmModes)((int)crystalArmModes+1); /* set it to the next item. */ // ! -----------IT DOES NOT WORK IF WE ASSIGN RANDOM IDs TO THE ENUM ITEMS-----------
            }
            OnPlayerInput.instance.onArmMode = false; //stop the button from being pressed and trigger the statement on the same press
        }
    }

    void ChargeShot() {
        if (OnPlayerInput.instance.onAbility1 && statistics.currentElectricShots > 0) { // Toggle the electric ability
            statistics.isElectric = !statistics.isElectric;
            OnPlayerInput.instance.onAbility1 = false;
        } 
    }

    void FiringTheProjectile() {
        //When the button is realiced ...
        if (!OnPlayerInput.instance.onFire1) {
            // ... but it has been pressed for some amount of time. Fire the according projectile
            if (timePassed > 0) {
                Vector3 aimDir = (centerScreenToWorldPosition - spawnBulletPosition.position).normalized;
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
            projectileDamage = statistics.damage;
            timePassed = 0;
        }
    }

    void ShotCooldown () {
        if (cooldownBetweenShots > 0) {
            cooldownBetweenShots -= Time.deltaTime;
            if (cooldownBetweenShots < 0) {
                cooldownBetweenShots = 0;
            }
        }
    }
}

public enum CrystalArmModes { // ! Dont assign random ID to this enum it will break the code. The IDs needs to be in order
    Default,
    Shotgun
}
