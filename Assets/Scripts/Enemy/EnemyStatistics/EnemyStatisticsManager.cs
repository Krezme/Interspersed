using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CurrentStats {
    public float health;
    public float damage;
    public float knockbackStrength;
    public float knockbackHeight;
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

    private bool healthBarCanvasTurnedOnOnce = false; // if the canvas has been turned on once it will not turn on again

    // Start is called before the first frame update
    void Start()
    {
        SetStatsFromSO();
        if (EnemyManager.instance != null) {
            EnemyManager.instance.enemyStatisticsManagers.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviourTreeRunner.context.agent != null) {
            behaviourTreeRunner.context.animator.SetFloat("speed", behaviourTreeRunner.context.agent.velocity.magnitude);
        }
    }

    public void SetStatsFromSO () {
        currentStats.health = statisticsSO.health;
        currentStats.damage = statisticsSO.damage;
        currentStats.knockbackStrength = statisticsSO.knockbackStrength;
        currentStats.knockbackHeight = statisticsSO.knockbackHeight;
    }

    public void TakeDamage (float damage) {
        currentStats.health -= damage;
        WarplingDamaged.PlayRandomClip();
        if(currentStats.health < statisticsSO.health && !healthBarCanvasTurnedOnOnce)
        {
            healthBarCanvas.SetActive(true);
            healthBarCanvasTurnedOnOnce = true;
        }
        enemyHealthbar.SetEnemyHealth(currentStats.health / statisticsSO.health);
        if (currentStats.health <= 0) {
            Destroy(healthBarCanvas);
            Death();
        }
        
    }

    void Death() {
        RagdollController ragdollController;
        TryGetComponent<RagdollController>(out ragdollController);
        if (ragdollController != null) {
            ragdollController.RagdollOn();
        }else {
            Destroy(this.gameObject);
        }
        WarplingDeath.PlayRandomClip();
    }
}
