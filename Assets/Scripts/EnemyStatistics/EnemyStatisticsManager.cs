using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentStats {
    public float health;
}

public class EnemyStatisticsManager : MonoBehaviour
{

    public EnemyStatisticsSO statisticsSO;

    public CurrentStats currentStats;
    public EnemyHealthbar enemyHealthbar;

    // Start is called before the first frame update
    void Start()
    {
        SetStatsFromSO();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStatsFromSO () {
        currentStats.health = statisticsSO.health;
    }

    public void TakeDamage (float damage) {
        currentStats.health -= damage;
        enemyHealthbar.SetwarpHealth(currentStats.health / statisticsSO.health);
        if (currentStats.health <= 0) {
            Death();
        }
    }

    void Death() {
        RagdollController ragdollController;
        TryGetComponent<RagdollController>(out ragdollController);
        if (ragdollController != null) {
            ragdollController.RagdollOn();
        }
    }
}
