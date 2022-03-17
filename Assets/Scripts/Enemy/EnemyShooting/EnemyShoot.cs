using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject projectile;

    public float speed = 32f; /// change this variable to adjust shoot speed

    public void FireBullet()
    {

        Instantiate(projectile, bulletSpawn.position, Quaternion.identity); ///clones the projectile

    }
}
