using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float heal;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log(other);
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                if (playerStatisticsManager.maxStatistics.health > playerStatisticsManager.currentStatistics.health) {
                    playerStatisticsManager.HealthRestore(heal);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
