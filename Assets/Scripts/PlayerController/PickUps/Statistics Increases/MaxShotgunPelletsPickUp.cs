using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxShotgunPelletsPickUp : MonoBehaviour
{
    public int shotgunProjectileCount;
    public int eventIntex;
    public string pupUpText = "Max Shotgun Pellets";
    public Text displayText;
    public GameObject toEnable;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                PlayerStatisticsManager.instance.maxStatistics.combatStatistics.crystalArmShotgunStats.projectileCount += shotgunProjectileCount;
                SaveData.instance.SetEventToComplete(eventIntex);
                try {
                    Instantiate(SFXMaxStatisticsPickup);
                } 
                catch (System.Exception) {}
                toEnable.SetActive(true);
                displayText.text = ("+" + shotgunProjectileCount + " " + pupUpText);
                Destroy(this.gameObject);
            }
        }
    }
}
