using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject projectile;

    public float speed = 32f;

    public void FireBullet()
    {

        Rigidbody rb = Instantiate(projectile, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * speed, ForceMode.Impulse);

    }
}
