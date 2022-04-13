using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerResourcesStatistics {
    public float health;
    public float energy;
}

[System.Serializable]
public class PlayerCombatStatistics {
    public float meleeDamage;
    
    public float[] chargeShotsDamage;

    public float[] chargeStages;

}

[System.Serializable]
public class PlayerStatistics {

    public PlayerResourcesStatistics resourcesStatistics;

    public PlayerCombatStatistics combatStatistics;

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
        currentStatistics = maxStatistics;
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
