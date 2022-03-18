using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject projectile;
    public EnemyStatisticsManager enemyStatisticsManager;

    public void FireBullet()
    {

        GameObject currentBullet = Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation); ///clones the projectile
        currentBullet.GetComponent<EnemyProjectile>().statistics.damage = enemyStatisticsManager.currentStats.damage;

    }
}
