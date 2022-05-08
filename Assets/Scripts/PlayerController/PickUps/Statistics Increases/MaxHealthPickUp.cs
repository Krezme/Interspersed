using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxHealthPickUp : MonoBehaviour
{
    public float maxHealth;
    public int eventIntex;
    public string pupUpText = "Max Health";
    public Text displayText;
    public GameObject toEnable;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.TryGetComponent<PlayerStatisticsManager>(out PlayerStatisticsManager playerStatisticsManager)) { 
                PlayerStatisticsManager.instance.maxStatistics.resourcesStatistics.health += maxHealth;
                SaveData.instance.SetEventToComplete(eventIntex);
                try {
                    Instantiate(SFXMaxStatisticsPickup);
                } 
                catch (System.Exception) {}
                toEnable.SetActive(true);
                displayText.text = ("+" + maxHealth + " " + pupUpText);
                Destroy(this.gameObject);
            }
        }
    }
}
