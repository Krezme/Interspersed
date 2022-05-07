using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxShieldHealthPickUp : MonoBehaviour
{
    public float shieldHealth;
    public int eventIntex;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                PlayerStatisticsManager.instance.maxStatistics.combatStatistics.slimeArmStats.shieldHealth += shieldHealth;
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
