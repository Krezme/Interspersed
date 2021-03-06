using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float heal;

    public GameObject SFXHealthPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                if (playerStatisticsManager.maxStatistics.resourcesStatistics.health > playerStatisticsManager.currentStatistics.resourcesStatistics.health) {
                    playerStatisticsManager.HealthRestore(heal);
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
