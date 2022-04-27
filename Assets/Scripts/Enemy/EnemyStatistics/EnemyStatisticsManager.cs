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

    /* public RandomAudioPlayer enemyDamaged;

    public RandomAudioPlayer enemyAttack;

    public RandomAudioPlayer enemyDeath;
 */


    public RandomAudioPlayerV2 warplingBank;

    public GameObject carrieFlapSourcePH;




    [HideInInspector]
    public SettlementDetection thisEnemySettlement;

    [HideInInspector]
    public FieldOfView fieldOfView;

    public EnemyDrops enemyDrops;

    private bool healthBarCanvasTurnedOnOnce = false; // if the canvas has been turned on once it will not turn on again

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<FieldOfView>(out fieldOfView);
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

    public void TakeDamage (float damage, bool hasTakenDamageFromPlayer) {
        currentStats.health -= damage;
        if (hasTakenDamageFromPlayer) {
            try {
                fieldOfView.TargetSpoted();
            }catch (System.Exception){}
        }
        try {
            //enemyDamaged.PlayRandomClip();
            warplingBank.PlayRandomClip(defaultBankIndex: 1);
        }catch (System.Exception) {
            Debug.LogWarning("Sound not assigned");
        }
        if(currentStats.health < statisticsSO.health && !healthBarCanvasTurnedOnOnce)
        {
            healthBarCanvas.SetActive(true);
            healthBarCanvasTurnedOnOnce = true;
        }
        enemyHealthbar.SetEnemyHealth(currentStats.health / statisticsSO.health);
        if (currentStats.health <= 0) {
            enemyDrops.RandomiseDrops();
            DestroyComponentsPreDeath();
            Death();
        }
    }

    void DestroyComponentsPreDeath() {
        Destroy(healthBarCanvas);
        Destroy(this);
        if (TryGetComponent<Collider>(out Collider thisCol)){
            Destroy(thisCol);
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

        try {
            carrieFlapSourcePH.SetActive(false);
            //enemyDeath.PlayRandomClip();
            warplingBank.PlayRandomClip(defaultBankIndex: 2);
        }catch (System.Exception) {
            Debug.LogWarning("Sound not assigned");
        }
    }

    void OnDestroy() {
        EnemyManager.instance.enemyStatisticsManagers.Remove(this);
    }
}
