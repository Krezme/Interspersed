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

        Rigidbody rb = Instantiate(projectile, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>(); /// clones projectile prefab and makes it a rigidbody
        rb.AddForce(bulletSpawn.forward * speed, ForceMode.Impulse); /// shoots it forward at the chosen speed

    }
}
