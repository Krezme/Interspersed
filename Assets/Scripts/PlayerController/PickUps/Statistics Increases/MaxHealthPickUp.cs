using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPickUp : MonoBehaviour
{
    public float maxHealth;
    public int eventIntex;

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
                Destroy(this.gameObject);
            }
        }
    }
}
