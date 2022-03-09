using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject projectile;

    public float speed = 32f;
    public int timeBetweenAttacks = 5;

    [HideInInspector]
    public bool alreadyAttacked = false;

    public void FireBullet()
    {
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(bulletSpawn.forward * speed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
       
    }
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
