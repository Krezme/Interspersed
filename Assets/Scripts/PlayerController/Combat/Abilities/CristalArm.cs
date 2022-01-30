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

    //public AudioClip CrystalShot; //Rhys - These have been commented out as I have upgraded the audio system to be handled by seperate RandomAudioPlayer objects

    public GameObject changeToArm;

    public Slider crosshair;

    public float[] chargeStages;

    public float projectileDamage; // This is a temp variable and will be changed when we decide if we are going with scritable objects or an other method

    private float timePassed;

    private float currentChargeStage;

    private GameObject currentBullet;

    public RandomAudioPlayer ChargeOn; //Rhys - Plays when electrical shot is activated

    public RandomAudioPlayer ChargeOff; //Rhys - Plays when electrical shot is deactivated without firing

    public RandomAudioPlayer ChargingPlayer; //Rhys - Crystal arm charging sound

    public AudioSource ChargingSource;

    public RandomAudioPlayer Regular; //Rhys - This enables my RandomAudioPlayer script to be created as an inspector window element to allow random sound variations for every sound handled by seperate sources - This allows for high customisation and ease of use

    public RandomAudioPlayer Electric; //Rhys - Assign to RandomAudioPlayer for Electrical shot sound

    private bool IsHold = false; //Rhys - Fixes an issue which would cause the RandomAudioPlayer to rapidly cycle through each charge sound variation instead of selecting one


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
        timePassed += Time.deltaTime;
        if (crosshair != null) {
            crosshair.value = timePassed / chargeStages[chargeStages.Length - 1];
        }
        for (int i = chargeStages.Length -1; i >= 0; i--) {
            if (chargeStages[i] <= timePassed) {
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


            //audioSource.PlayOneShot(ChargeSwap);
            if (statistics.isElectric == true)
            {
                ChargeOn.PlayRandomClip(); //Rhys - Plays charge on sound when is.Electric == True
            }
            else
            {
                //isElectric = false;
                ChargeOff.PlayRandomClip(); //Rhys - Plays charge off sound when is.Electric == false
            }


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

                    if (statistics.isElectric == true) //Rhys - Plays electric shot if isElectric == true                    
                    {
                        //audioSource.PlayOneShot(ElectricShot);
                        Electric.PlayRandomClip();
                        Debug.Log("Electric");
                    }

                }


                statistics.isElectric = false;
                OnPlayerInput.instance.onFire1 = false;


                if (statistics.isElectric == false) //Rhys - Plays electric shot if isElectric == false - I used to have an if/else statement here however it seemed to ignore the else and only played the regular sound, so I seperated the sounds into 2 if statements for now
                {
                    //audioSource.PlayOneShot(CrystalShot);
                    Regular.PlayRandomClip();
                    Debug.Log("Regular");
                    IsHold = false; //Rhys - Resets IsHold so that charging sound will be played when fire key is held again
                }     
              

            }
            if (crosshair != null){
                crosshair.value = 0;
            }
            projectileDamage = 10;
            timePassed = 0;
        }


        if (OnPlayerInput.instance.onFire1) //Rhys - Performs the opposite of the above code so that the charging sound plays 
        {
            if (IsHold == false) //Rhys - Checks to see if fire key has been held down already 
            {
                ChargingPlayer.PlayRandomClip();
                //Debug.Log("Charging...");
                IsHold = true; //Rhys - Once the fire key has been held down, IsHeld is set to true to prevent the RandomAudioPlayer from constantly cycling through each variantion of the charge sound instead of selecting one
            }
        }
        else
        {
            ChargingSource.Stop();
            //Debug.Log("Stopping...");
        }

        }
}
