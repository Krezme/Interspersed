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
    public float energy;
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

    public float[] chargeShotStages;

    public float electricRechargeTime;
}

[System.Serializable]
public class CrystalArmShotgunStats{
    public float damagePerPellet;
    public int projectileCount;
    public float shotgunCooldown;
    public float projectileDispersionX; //The spread of the projectiles horizontally
    public float projectileDispersionY; //The spread of the projectiles vertically
}

[System.Serializable]
public class SlimeArmStats {
    public float grabbingCooldown = 1;
    public float maxGrabDistance = 10f;
    public float throwforce = 100f;
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
        SetSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSliderValues()
    {
        Healthbar.instance.slider.maxValue = maxStatistics.resourcesStatistics.health;
        Energybar.instance.slider.maxValue = maxStatistics.resourcesStatistics.energy;

        Healthbar.instance.slider.value = maxStatistics.resourcesStatistics.health;
        Energybar.instance.slider.value = maxStatistics.resourcesStatistics.energy;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void HealthRestore(float health) {
        currentStatistics.resourcesStatistics.health += health;

        Healthbar.instance.slider.value = currentStatistics.resourcesStatistics.health;
    }
}
