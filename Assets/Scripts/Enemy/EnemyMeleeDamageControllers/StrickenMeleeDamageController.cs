using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrickenMeleeDamageController : MonoBehaviour
{
    public EnemyStatisticsManager statisticsManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStatisticsManager>().TakeDamage(statisticsManager.currentStats.damage);

            ThirdPersonPlayerController.instance.ApplyKnockback(statisticsManager.transform.position, statisticsManager.currentStats.knockbackStrength, statisticsManager.currentStats.knockbackHeight);
        }
    }
}
