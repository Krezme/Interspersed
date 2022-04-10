using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementDetection : MonoBehaviour
{

    public List<EnemyStatisticsManager> thisSettlementEnemyStatisticsManagers = new List<EnemyStatisticsManager>();

    public float settlementRadius;

    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.settlementDetectionScripts.Add(this);
    }

    public void GetAllSettlementEnemies() {
        if (EnemyManager.instance != null) {
            foreach (EnemyStatisticsManager esm in EnemyManager.instance.enemyStatisticsManagers) {
                if (Vector3.Distance(esm.transform.position, this.transform.position) <= settlementRadius) {
                    thisSettlementEnemyStatisticsManagers.Add(esm);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, settlementRadius);
    }
}
