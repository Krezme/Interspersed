using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform bulletSpawn; /// the location the bullet will spawn from
    public GameObject projectile; /// the projectile prefab
    public EnemyStatisticsManager enemyStatisticsManager; /// AI stats

    public void FireBullet()
    {
        GameObject currentBullet = Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation); ///clones the projectile
        currentBullet.GetComponent<EnemyProjectile>().statistics.damage = enemyStatisticsManager.currentStats.damage; /// gets how much dmage the bullet should do from the AI's statistics
    }
}
