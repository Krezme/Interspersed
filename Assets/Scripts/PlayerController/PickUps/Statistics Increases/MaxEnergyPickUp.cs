using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnergyPickUp : MonoBehaviour
{
    public float maxCystalEnergy;
    public float maxSlimeEnergy;
    public int eventIntex;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                PlayerStatisticsManager.instance.maxStatistics.resourcesStatistics.cystalEnergy += maxCystalEnergy;
                PlayerStatisticsManager.instance.maxStatistics.resourcesStatistics.slimeEnergy += maxSlimeEnergy;
                SaveData.instance.SetEventToComplete(eventIntex);
                try {
                    Instantiate(SFXMaxStatisticsPickup);
                } 
                catch (System.Exception) {}
                Destroy(this.gameObject);
            }
        }
    }
}
