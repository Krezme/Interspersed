using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxEnergyPickUp : MonoBehaviour
{
    public float maxEnergy;
    public int eventIntex;
    public string pupUpText = "Max Energy";
    public Text displayText;
    public GameObject toEnable;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                PlayerStatisticsManager.instance.maxStatistics.resourcesStatistics.cystalEnergy += maxEnergy;
                PlayerStatisticsManager.instance.maxStatistics.resourcesStatistics.slimeEnergy += maxEnergy;
                float tempHealth = PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.health;
                float tempCrystalEnergy = PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy;
                float tempSlimeEnergy = PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.slimeEnergy;
                PlayerStatisticsManager.instance.ResetPlayerStatistics();
                PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.health = tempHealth;
                PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.cystalEnergy = tempCrystalEnergy;
                PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.slimeEnergy = tempSlimeEnergy;
                SaveData.instance.SetEventToComplete(eventIntex);
                try {
                    Instantiate(SFXMaxStatisticsPickup);
                } 
                catch (System.Exception) {}
                toEnable.SetActive(true);
                displayText.text = ("+" + maxEnergy + " " + pupUpText);
                Destroy(this.gameObject);
            }
        }
    }
}
