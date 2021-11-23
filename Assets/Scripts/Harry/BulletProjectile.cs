using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public Rigidbody bulletRigidbody;

    [HideInInspector]
    public float damage;

    public float thisLifespan = 5f;

    private float currentAge;

    private void Start()
    {
        float speed = 20f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void Update()
    {
        currentAge += Time.deltaTime;

        if (currentAge >= thisLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy") {
            Destroy(gameObject);
            EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
            enemyStatisticsManager.TakeDamage(damage);
        }
        else
        {
            bulletRigidbody.velocity = Vector3.zero;
            currentAge = 0;
            bulletRigidbody.isKinematic = true;
        }
    }
}
