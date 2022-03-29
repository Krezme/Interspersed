using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierAttacksManager : MonoBehaviour
{
    public GameObject projectile;

    public Transform projectileSpawnPos;

    public EnemyStatisticsManager enemyStatisticsManager;

    public float attackCooldown;

    [HideInInspector]
    public float attackCurrentCooldown;

    void Update(){
        if (attackCurrentCooldown > 0) {
            attackCurrentCooldown -= Time.deltaTime;
        }
    }

    public void ShootProjectile(Vector3 direction) {
        GameObject currentProjectile = Instantiate(projectile, projectileSpawnPos.position, Quaternion.LookRotation(direction, Vector3.up));
        currentProjectile.GetComponent<EnemyBulletProjectile>().SetProjectileStatistics(enemyStatisticsManager.currentStats.damage);
    }

}
