using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerCurrentStatistics {
    public float health;
    public float energy;
}

[System.Serializable]
public class PlayerMaxStatistics
{
    public float health;
    public float energy;
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
        Healthbar.instance.slider.maxValue = maxStatistics.health;
        Energybar.instance.slider.maxValue = maxStatistics.energy;

        Healthbar.instance.slider.value = maxStatistics.health;
        Energybar.instance.slider.value = maxStatistics.energy;
    }

    public void ResetPlayerStatistics()
    {
        currentStatistics.health = maxStatistics.health;
        currentStatistics.energy = maxStatistics.energy;
    }

    public void TakeDamage(float damage)
    {
        currentStatistics.health -= damage;

        Healthbar.instance.slider.value = currentStatistics.health;

        PlayerDamaged.PlayRandomClip();

        if (currentStatistics.health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void HealthRestore(float health) {
        currentStatistics.health += health;

        Healthbar.instance.slider.value = currentStatistics.health;
    }
}
