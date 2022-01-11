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

    public GameObject healthBarCanvas;
    
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
        if(currentStats.health < statisticsSO.health)
        {
            healthBarCanvas.SetActive(true);
        }
        enemyHealthbar.SetEnemyHealth(currentStats.health / statisticsSO.health);
        if (currentStats.health <= 0) {
            healthBarCanvas.SetActive(false);
            Death();
        }
        
    }

    void Death() {
        RagdollController ragdollController;
        TryGetComponent<RagdollController>(out ragdollController);
        healthBarCanvas.SetActive(false);
        if (ragdollController != null) {
            ragdollController.RagdollOn();
        }
    }
}
