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
