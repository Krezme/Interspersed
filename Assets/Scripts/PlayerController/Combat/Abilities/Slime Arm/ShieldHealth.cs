using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public float health = 1;

    public void SetShieldHealth() {
        health = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.shieldHealth;
    }
}
