using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : MonoBehaviour
{
    public float energy;

    public GameObject SFXHealthPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                if (playerStatisticsManager.maxStatistics.resourcesStatistics.cystalEnergy > playerStatisticsManager.currentStatistics.resourcesStatistics.cystalEnergy || 
                playerStatisticsManager.maxStatistics.resourcesStatistics.slimeEnergy > playerStatisticsManager.currentStatistics.resourcesStatistics.slimeEnergy) {
                    
                    playerStatisticsManager.CrystalEnergyRecharge(energy);
                    playerStatisticsManager.SlimeEnergyRecharge(energy);
                    try {
                        Instantiate(SFXHealthPickup);
                    } 
                    catch (System.Exception) {}
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
