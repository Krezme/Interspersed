using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.VFX;

[System.Serializable]
public class CrystalArmStatistics
{
    public float maxElectricShots;
    public float currentElectricShots;
    public float currentRechrge;
    public bool isElectric;
}

public class CristalArm : PlayerAbility
{

    #region Singleton
    public static CristalArm instance;
    void Awake() {
        if (instance != null) {
            Debug.LogError("THERE ARE TWO OR MORE CristalArm INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }
        else {
            instance = this;
        }
    } 

    #endregion
    public ArmAbilities[] armAbilities;

    public CrystalArmStatistics statistics;

    public CrystalArmModes crystalArmModes;

    public Transform spawnBulletPosition;

    public GameObject[] pfBulletProjectileDef; //The prefabs for the single shot

    public GameObject pfPelletProjectileShotgun; //The prefab for the shotgun projectile

    public AudioSource audioSource;

    //public AudioClip CrystalShot; //Rhys - These have been commented out as I have upgraded the audio system to be handled by seperate RandomAudioPlayer objects

    public GameObject changeToArm;

    public VisualEffect lightningVFXEffect;

    public SkinnedMeshToMesh lightningMeshToMesh;

    public Slider crosshair;

    private float timePassed = -1;

    private float cooldownBetweenShots;

    private float currentChargeStage;

    private GameObject currentBullet;

    public RandomAudioPlayer ChargeOn; //Rhys - Plays when electrical shot is activated

    public RandomAudioPlayer ChargeOff; //Rhys - Plays when electrical shot is deactivated without firing

    public RandomAudioPlayer ChargingPlayer; //Rhys - Crystal arm charging sound

    public AudioSource ChargingSource;

    public RandomAudioPlayer Regular; //Rhys - This enables my RandomAudioPlayer script to be created as an inspector window element to allow random sound variations for every sound handled by seperate sources - This allows for high customisation and ease of use

    public RandomAudioPlayer Electric; //Rhys - Assign to RandomAudioPlayer for Electrical shot sound

    public bool IsHold = false; //Rhys - Fixes an issue which would cause the RandomAudioPlayer to rapidly cycle through each charge sound variation instead of selecting one - Changed from private to public to allow access from ThirdPersonPlayerController

    public RandomAudioPlayer ShotgunShoot; //Rhys - Shotgun shoot sound bank

    public RandomAudioPlayer ShotgunSwapTrue; //Rhys - Swapping to shotgun

    public RandomAudioPlayer ShotgunSwapFalse; //Rhys - Swapping from shotgun;

    public GameObject disPointForCalculation;

    public AudioSource electricIdle;

    public AudioSource ammoDepleted;

    /// <summary>
    /// Hard Coding the armAbilities array
    /// </summary>
    void Start () {
    }

    void Update () {
        ElectricChargeCooldown();
    }

    /// <summary>
    /// If the currentElectricShots are not at capacity the timer is going to start and recarge them
    /// </summary>
    private void ElectricChargeCooldown () {
        if (statistics.currentElectricShots < statistics.maxElectricShots){
            statistics.currentRechrge += Time.deltaTime;
            if (statistics.currentRechrge >= PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.electricRechargeTime) {   
                statistics.currentElectricShots++;
                statistics.currentRechrge = 0;
            }
            for (int i = CrosshairReferences.instance.chargesUI.Length -1; i >= 0 ; i--) {
                if (i > statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = 0;
                }
                else if (i == statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = statistics.currentRechrge / PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.electricRechargeTime;
                }
                else if (i < statistics.currentElectricShots) {
                    CrosshairReferences.instance.chargesUI[i].value = 1;
                }
            }
        }
    }

    /* This needs to be changed */ // ! ONLY PLACE HOLDER UNTIL PLAYER CHARACTER WITH MORTH TARGETS IS IMPORTED
    public override void MorthToTarget()
    {
        base.MorthToTarget();
        changeToArm.SetActive(!changeToArm.activeSelf);
    }

    // Switching between modes
    public override void AimingAbility ()
    {
        if (crystalArmModes == CrystalArmModes.Default) { 
            if (armAbilities[0].isActive) { // Locking the Single Shot ability
                ChargeSingleShotAimingAbility();
            }
        }
        else if (crystalArmModes == CrystalArmModes.Shotgun) {
            if (armAbilities[2].isActive) { // Locking the Shotgun ability
                ShotgunShotAimingAbility();
            }
        }
    }

    void ChargeSingleShotAimingAbility() {
        if (PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotsEnergyCost[0] <= PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy) {
            if (timePassed < 0) {
                timePassed = 0;
            }
            
            try {
                if (PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy >= PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotsEnergyCost[(int)currentChargeStage + 1]) {
                    timePassed += Time.deltaTime;
                }
                else {
                    timePassed = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages[(int)currentChargeStage];
                }
            }
            catch (System.Exception) {
                timePassed = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages[PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages.Length - 1];
            }
            
            if (crosshair != null) {
                crosshair.value = timePassed / PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages[PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages.Length - 1]; //Calculates the crosshair fill depending on the charges. It is chargeStages.Length - 1, because the first charge is default
            }
            // when the first charge is not in the array
            for (int i = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages.Length -1; i >= 0; i--) {
                if (PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotStages[i] <= timePassed) { //checking for the time and then setting the correct bullet prefab 
                    currentBullet = pfBulletProjectileDef[i];
                    currentChargeStage = i;
                    // finctionality depending on different charge stage
                    break;
                }
            }
        
        }
        else
        {  
            ammoDepleted.Play();
            OnPlayerInput.instance.onFire1 = false;
        }
    }

    void ShotgunShotAimingAbility() {
        if (cooldownBetweenShots <= 0 && PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy >= PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.shotgunEnergyCost) {
            for (int i = 0; i < PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.projectileCount; i++) {
                Vector3 aimDir = (centerScreenToWorldPosition - spawnBulletPosition.position).normalized;

                // The new Spawn Sosition of the pellet direction empty gameobject
                Vector3 newSpawnPos = new Vector3(UnityEngine.Random.Range(-1.0f * PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.projectileDispersionX, 1.0f * PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.projectileDispersionX), 
                    UnityEngine.Random.Range(-1.0f * PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.projectileDispersionY, 1.0f * PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.projectileDispersionY), 0);
                
                GameObject dispersionPoint = Instantiate(disPointForCalculation, spawnBulletPosition); //Spawning the empty gamebject that acts as the ditection spawn point of the object
                dispersionPoint.transform.localPosition = newSpawnPos; // Sets the local postion, of the direction gameobject, to the exact local posion related to the spawnBulletPosition
                dispersionPoint.transform.position += spawnBulletPosition.transform.forward*2; // Moves the direction gameobject forward to scale down the dispersion multiplication
                Vector3 newSpreadPoint = (dispersionPoint.transform.position - spawnBulletPosition.transform.position).normalized; //saves the new direction point
                Destroy(dispersionPoint);
                GameObject pellet = Instantiate(pfPelletProjectileShotgun, spawnBulletPosition.position, Quaternion.LookRotation(newSpreadPoint, Vector3.up)); // Instantiating the pellet going to the direction point
                ShotgunShoot.PlayRandomClip(); //Rhys - Plays from shotgun shoot sound bank
                
                //setting the projectile damage and speed multiplier
                BulletProjectile newPelletProjectile = pellet.GetComponent<BulletProjectile>();
                newPelletProjectile.statistics.damage = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.damagePerPellet;
                newPelletProjectile.statistics.chargeStage = 1; 
            }
            
            //audioSource.PlayOneShot(abilitySound);
            // Restarting the fire sequence
            PlayerStatisticsManager.instance.CrystalEnergyRecharge(-PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.shotgunEnergyCost); // spends energy
            OnPlayerInput.instance.onFire1 = false;
            cooldownBetweenShots = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.shotgunCooldown;
        }
        else if(PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy < PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmShotgunStats.shotgunEnergyCost)
        {
            ammoDepleted.Play();
            OnPlayerInput.instance.onFire1 = false;
        }
        /* else
        {
            !FOR SHOTGUN COOLDOWN SOUND
        } */

    }

    // Switching between modes
    public override void AditionalAbilities() {
        ChangeMode();
        if (armAbilities[1].isActive) {
            ElectricShot();
        }
        if (crystalArmModes == CrystalArmModes.Default) {
            FiringTheProjectile();
        }
        if (crystalArmModes == CrystalArmModes.Shotgun) {
            ShotCooldown();
        }
    }

    void ChangeMode() {
        if (OnPlayerInput.instance.onArmMode) {
            NextMode();
            OnPlayerInput.instance.onArmMode = false; //stop the button from being pressed and trigger the statement on the same press
        }
    }

    void NextMode() {
        if ((int)crystalArmModes == Enum.GetValues(typeof(CrystalArmModes)).Cast<int>().Max()){ // if the current item is the last item possible
            DefaultMode(); //set it to first item
        }
        else {
            crystalArmModes = (CrystalArmModes)((int)crystalArmModes+1); /* set it to the next item. */ // ! -----------IT DOES NOT WORK IF WE ASSIGN RANDOM IDs TO THE ENUM ITEMS-----------
            bool isNextLocked = false;
            for (int i = 0; i < armAbilities.Length; i++) {
                if (crystalArmModes.ToString() == armAbilities[i].abilityName && !armAbilities[i].isActive) {
                    isNextLocked = true;
                    break;
                }
            }
            if (isNextLocked) {
                NextMode();
            }else {
                SetIsElectric(false);
                ShotgunSwapTrue.PlayRandomClip();
            }
        }
    }

    void DefaultMode() {
        crystalArmModes = (CrystalArmModes)Enum.GetValues(typeof(CrystalArmModes)).Cast<int>().Min(); //set it to first item
        if (armAbilities[2].isActive) {
            ShotgunSwapFalse.PlayRandomClip();
        }
    }

    void ElectricShot() {
        if (OnPlayerInput.instance.onAbility1 && statistics.currentElectricShots > 0) { // Toggle the electric ability
            if (crystalArmModes == CrystalArmModes.Shotgun) {
                DefaultMode();
            }
            SetIsElectric(!statistics.isElectric);
        }
    }

    void SetIsElectric (bool state) {
        lightningVFXEffect.enabled = state;
        lightningMeshToMesh.enabled = state;
        statistics.isElectric = state;
        OnPlayerInput.instance.onAbility1 = false;

        //audioSource.PlayOneShot(ChargeSwap);
        if (statistics.isElectric == true)
        {
            ChargeOn.PlayRandomClip(); //Rhys - Plays charge on sound when is.Electric == True
            electricIdle.Play();
        }
        else
        {
            //isElectric = false;
            ChargeOff.PlayRandomClip(); //Rhys - Plays charge off sound when is.Electric == false
            electricIdle.Pause();
        }
    }

    void FiringTheProjectile() {
        //When the button is realiced ...
        if (!OnPlayerInput.instance.onFire1) {
            // ... but it has been pressed for some amount of time. Fire the according projectile
            if (timePassed >= 0) {
                Vector3 aimDir = (centerScreenToWorldPosition - spawnBulletPosition.position).normalized;
                GameObject bullet = Instantiate(currentBullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                BulletProjectile newBulletProjectile = bullet.GetComponent<BulletProjectile>();
                newBulletProjectile.statistics.damage = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotsDamage[(int)currentChargeStage];
                newBulletProjectile.statistics.chargeStage = currentChargeStage;
                newBulletProjectile.statistics.speed = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotsSpeed[(int)currentChargeStage];
                newBulletProjectile.statistics.isElectric = statistics.isElectric; // setting the projectile to electric 
                PlayerStatisticsManager.instance.CrystalEnergyRecharge(-PlayerStatisticsManager.instance.currentStatistics.combatStatistics.crystalArmStats.chargeShotsEnergyCost[(int)currentChargeStage]);
                if (statistics.isElectric) { // Decreasing charged shots
                    statistics.currentElectricShots--;

                    if (statistics.isElectric == true) //Rhys - Plays electric shot if isElectric == true                    
                    {
                        //audioSource.PlayOneShot(ElectricShot);
                        Electric.PlayRandomClip();
                        electricIdle.Pause();
                    }

                }

                if (statistics.isElectric == false) //Rhys - Plays electric shot if isElectric == false - I used to have an if/else statement here however it seemed to ignore the else and only played the regular sound, so I seperated the sounds into 2 if statements for now
                {
                    //audioSource.PlayOneShot(CrystalShot);
                    Regular.PlayRandomClip();
                    IsHold = false; //Rhys - Resets IsHold so that charging sound will be played when fire key is held again
                }
                lightningVFXEffect.enabled = false;
                lightningMeshToMesh.enabled = false;
                statistics.isElectric = false;
                OnPlayerInput.instance.onFire1 = false;

            }
            if (crosshair != null){
                crosshair.value = 0;
            }
            timePassed = -1;
            currentChargeStage = 0;
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

    void ShotCooldown () {
        if (cooldownBetweenShots > 0) {
            cooldownBetweenShots -= Time.deltaTime;
            if (cooldownBetweenShots < 0) {
                cooldownBetweenShots = 0;
            }
        }
    }

    public void EnableAbility(int index) {
        armAbilities[index].isActive = true;
    }

    public void DisableAbility(int index) {
        armAbilities[index].isActive = false;
    }

    void OnValidate() {
        for (int i = 0; i < armAbilities.Length; i++) {
            armAbilities[i].Validate();
        }
    }
}

public enum CrystalArmModes { // ! Dont assign random ID to this enum it will break the code. The IDs needs to be in order
    Default,
    Shotgun
}
