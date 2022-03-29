using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrickenMeleeDamageController : MonoBehaviour
{
    public EnemyStatisticsManager statisticsManager; /// holds the statistics for the enemy

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") /// checks to see if the collider is the player
        {
            /// Runs the take damage function and does whatever damage value is set on the AI's statistics
            other.GetComponent<PlayerStatisticsManager>().TakeDamage(statisticsManager.currentStats.damage);

            /// knocksback the player with values from the AI's statistics
            ThirdPersonPlayerController.instance.ApplyKnockback(statisticsManager.transform.position, statisticsManager.currentStats.knockbackStrength, statisticsManager.currentStats.knockbackHeight);
        }
    }
}
