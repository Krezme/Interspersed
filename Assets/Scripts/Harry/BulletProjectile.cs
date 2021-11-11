using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public Rigidbody bulletRigidbody;

    public float lifeSpanInSecs;
    private float currentTimer;

    [HideInInspector]
    public float damage;

    private void Start()
    {
        float speed = 20f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void Update () {
        currentTimer += Time.deltaTime;
        if (currentTimer >= lifeSpanInSecs) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.tag == "Enemy") {
            EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
            enemyStatisticsManager.TakeDamage(damage);
        }
    }
}
