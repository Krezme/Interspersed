using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxSingleShotDamagePickUp : MonoBehaviour
{
    public float[] chargeShotsDamage;
    public int eventIntex;
    public string pupUpText = "Max Single Shot Damage";
    public Text displayText;
    public GameObject toEnable;

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
                toEnable.SetActive(true);
                displayText.text = ("+" + chargeShotsDamage[0] + " " + pupUpText);
                Destroy(this.gameObject);
            }
        }
    }
}
