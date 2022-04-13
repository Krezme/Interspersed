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

    public void Validate () {
        //chargeShotsDamage = new float[CristalArm.insance.chargeStages.Length];
    }
}

[System.Serializable]
public class PlayerCurrentStatistics {

    public PlayerResourcesStatistics resourcesStatistics;

    public PlayerCombatStatistics combatStatistics;

}

[System.Serializable]
public class PlayerMaxStatistics
{
    public PlayerResourcesStatistics resourcesStatistics;

    public PlayerCombatStatistics combatStatistics;
}

public class PlayerStatisticsManager : MonoBehaviour
{

    public PlayerCurrentStatistics currentStatistics;
    public PlayerMaxStatistics maxStatistics;

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
        currentStatistics.resourcesStatistics.health = maxStatistics.resourcesStatistics.health;
        currentStatistics.resourcesStatistics.energy = maxStatistics.resourcesStatistics.energy;
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

    public void OnValidate() {
        //maxStatistics.combatStatistics.Validate();
    }
}
