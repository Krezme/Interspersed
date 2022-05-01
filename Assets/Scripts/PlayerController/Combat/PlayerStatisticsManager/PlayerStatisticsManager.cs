using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Extensions
{
    public static T DeepClone<T>(this T obj)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
 
            return (T) formatter.Deserialize(stream);
        }
    }
}

[System.Serializable]
public class PlayerStatistics {

    public PlayerResourcesStatistics resourcesStatistics;

    public PlayerCombatStatistics combatStatistics;

}

[System.Serializable]
public class PlayerResourcesStatistics {
    public float health;
    public float cystalEnergy;
    public float slimeEnergy;

    public PlayerRechargeEnergyStatistics energyRechargeStatistics;
}

[System.Serializable]
public class PlayerRechargeEnergyStatistics {
    public float crystalEnergyRechargeOverOneSecond;
    public float slimeEnergyRechargeOverOneSecond;
}

[System.Serializable]
public class PlayerCombatStatistics {
    public float meleeDamage;

    public CrystalArmSingleShotStats crystalArmStats;

    public CrystalArmShotgunStats crystalArmShotgunStats;

    public SlimeArmStats slimeArmStats;

}

[System.Serializable]
public class CrystalArmSingleShotStats {
    public float[] chargeShotsDamage;

    public float[] chargeShotsSpeed;

    public float[] chargeShotsEnergyCost;

    public float[] chargeShotStages;

    public float electricRechargeTime;
}

[System.Serializable]
public class CrystalArmShotgunStats{
    public float damagePerPellet;
    public int projectileCount;
    public float shotgunEnergyCost;
    public float shotgunCooldown;
    public float projectileDispersionX; //The spread of the projectiles horizontally
    public float projectileDispersionY; //The spread of the projectiles vertically
}

[System.Serializable]
public class SlimeArmStats {
    public float grabbingCooldown = 1;
    public float maxGrabDistance = 10f;
    public float throwforce = 100f;
    public float shieldHealth = 30; // the health of the shield when the object is used as a shield
    public float holdObjectEnergyCost;
    public float throwEnergyCost;
}

public class PlayerStatisticsManager : MonoBehaviour
{

    #region Singleton

    public static PlayerStatisticsManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("THERE ARE TWO OR MORE PlayerStatisticsManager INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }
        else {
            instance = this;
        }
    }
    #endregion

    public PlayerStatistics currentStatistics;

    public PlayerStatistics maxStatistics;

    public RandomAudioPlayer PlayerDamaged;

    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerStatistics();
        SetSlidersValues();
    }

    // Update is called once per frame
    void Update()
    {
        CrystalEnergyRecharge(currentStatistics.resourcesStatistics.energyRechargeStatistics.crystalEnergyRechargeOverOneSecond * Time.deltaTime);
        SlimeEnergyRecharge(currentStatistics.resourcesStatistics.energyRechargeStatistics.slimeEnergyRechargeOverOneSecond * Time.deltaTime);
    }

    public void SetSlidersValues()
    {
        SetSlidersMaxValue();

        ResetSlidersValue();
    }

    public void SetSlidersMaxValue() {
        Healthbar.instance.slider.maxValue = maxStatistics.resourcesStatistics.health;
        CrystalEnergybar.instance.slider.maxValue = maxStatistics.resourcesStatistics.cystalEnergy;
        SlimeEnergybar.instance.slider.maxValue = maxStatistics.resourcesStatistics.slimeEnergy;
    }

    public void ResetSlidersValue() {
        Healthbar.instance.slider.value = maxStatistics.resourcesStatistics.health;
        CrystalEnergybar.instance.slider.value = maxStatistics.resourcesStatistics.cystalEnergy;
        SlimeEnergybar.instance.slider.value = maxStatistics.resourcesStatistics.slimeEnergy;
    }

    public void ResetPlayerStatistics()
    {
        currentStatistics = maxStatistics.DeepClone();
    }

    public void TakeDamage(float damage)
    {
        currentStatistics.resourcesStatistics.health -= damage;

        Healthbar.instance.slider.value = currentStatistics.resourcesStatistics.health;

        PlayerDamaged.PlayRandomClip();

        if (currentStatistics.resourcesStatistics.health <= 0) {
            if (SaveData.instance != null) {
                SaveData.instance.ReloadScene();
            }
            else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void HealthRestore(float health) {
        currentStatistics.resourcesStatistics.health += health;

        Healthbar.instance.slider.value = currentStatistics.resourcesStatistics.health;
    }

    public void CrystalEnergyRecharge(float energy) {
        currentStatistics.resourcesStatistics.cystalEnergy += energy;

        if (currentStatistics.resourcesStatistics.cystalEnergy > maxStatistics.resourcesStatistics.cystalEnergy) {
            currentStatistics.resourcesStatistics.cystalEnergy = maxStatistics.resourcesStatistics.cystalEnergy;
        }
        else if (currentStatistics.resourcesStatistics.cystalEnergy < 0) {
            currentStatistics.resourcesStatistics.cystalEnergy = 0;
        }
        
        CrystalEnergybar.instance.slider.value = currentStatistics.resourcesStatistics.cystalEnergy;
    }

    public void SlimeEnergyRecharge(float energy) {
        currentStatistics.resourcesStatistics.slimeEnergy += energy;

        if (currentStatistics.resourcesStatistics.slimeEnergy > maxStatistics.resourcesStatistics.slimeEnergy) {
            currentStatistics.resourcesStatistics.slimeEnergy = maxStatistics.resourcesStatistics.slimeEnergy;
        }

        SlimeEnergybar.instance.slider.value = currentStatistics.resourcesStatistics.slimeEnergy;
    }
}
