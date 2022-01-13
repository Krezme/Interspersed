using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColTriggers : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> enemiesHit;

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Enemy") {
            EnemyStatisticsManager otherEnemyStatisticsManager;
            otherEnemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
            if (otherEnemyStatisticsManager != null) {
                if (enemiesHit.Count > 0) {
                    for(int i = 0; i < enemiesHit.Count; i++) {
                        if (enemiesHit[i] == other.gameObject) {
                            return;
                        }
                    }
                }
                if (otherEnemyStatisticsManager.currentStats.health > 0) {
                    otherEnemyStatisticsManager.TakeDamage(PlayerAbilitiesController.instance.meleeStats.damage);
                    enemiesHit.Add(other.gameObject);
                }
            }
        }
    }
}
