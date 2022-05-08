using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxShieldHealthPickUp : MonoBehaviour
{
    public float shieldHealth;
    public int eventIntex;
    public string pupUpText = "Max Shield Health";
    public Text displayText;

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
                displayText.text = ("+" + shieldHealth + " " + pupUpText);
                Destroy(this.gameObject);
            }
        }
    }
}
