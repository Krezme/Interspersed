using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSingleShotDamagePickUp : MonoBehaviour
{
    public float[] chargeShotsDamage;
    public int eventIntex;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                for (int i = 0; i < PlayerStatisticsManager.instance.maxStatistics.combatStatistics.crystalArmStats.chargeShotsDamage.Length; i++) {
                    PlayerStatisticsManager.instance.maxStatistics.combatStatistics.crystalArmStats.chargeShotsDamage[i] += chargeShotsDamage[i];
                }
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
