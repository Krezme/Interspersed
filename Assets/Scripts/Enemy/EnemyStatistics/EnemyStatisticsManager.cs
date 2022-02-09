using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CurrentStats {
    public float health;
    public float damage;
}

public class EnemyStatisticsManager : MonoBehaviour
{

    public EnemyStatisticsSO statisticsSO;

    public CurrentStats currentStats;

    public GameObject healthBarCanvas;
    
    public EnemyHealthbar enemyHealthbar;

    public BehaviourTreeRunner behaviourTreeRunner;

    public RandomAudioPlayer WarplingDamaged;

    public RandomAudioPlayer WarplingAttack;

    public RandomAudioPlayer WarplingDeath;

    // Start is called before the first frame update
    void Start()
    {
        SetStatsFromSO();
    }

    // Update is called once per frame
    void Update()
    {
        behaviourTreeRunner.context.animator.SetFloat("speed", behaviourTreeRunner.context.agent.velocity.magnitude);
    }

    public void SetStatsFromSO () {
        currentStats.health = statisticsSO.health;
        currentStats.damage = statisticsSO.damage;
    }

    public void TakeDamage (float damage) {
        currentStats.health -= damage;
        WarplingDamaged.PlayRandomClip();
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
        WarplingDeath.PlayRandomClip();
    }
}
