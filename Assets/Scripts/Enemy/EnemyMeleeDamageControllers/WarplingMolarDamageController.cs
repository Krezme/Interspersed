using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarplingMolarDamageController : MonoBehaviour
{
    public EnemyStatisticsManager statisticsManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStatisticsManager>().TakeDamage(statisticsManager.currentStats.damage);
        }
    }
}
