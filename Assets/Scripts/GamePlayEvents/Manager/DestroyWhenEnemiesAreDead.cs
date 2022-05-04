using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnemiesAreDead : MonoBehaviour
{
    public List<EnemyStatisticsManager> enemyStatisticsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemyStatisticsManager.Count > 0) {
            for (int i = 0; i < enemyStatisticsManager.Count; i++) {
                if (enemyStatisticsManager[i] == null || enemyStatisticsManager[i].currentStats.health <= 0) {
                    enemyStatisticsManager.Remove(enemyStatisticsManager[i]);
                }
            }
        }
        else {
            Destroy(this.gameObject);
        }
    }
}
