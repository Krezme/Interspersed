using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public Rigidbody bulletRigidbody;

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float chargeStage;

    public float thisLifespan = 5f;

    private float currentAge;

    public float speed = 10f;

    private void Start()
    {
        Debug.Log(chargeStage * speed);
        bulletRigidbody.velocity = transform.forward * (speed * chargeStage);
    }

    private void Update()
    {
        currentAge += Time.deltaTime; // Adds age to the bullet 

        if (currentAge >= thisLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos = transform.position;
        if (other.tag == "Enemy") { 
            Destroy(gameObject);
            EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
            enemyStatisticsManager.TakeDamage(damage);
        }
        else if (other.gameObject.layer != this.gameObject.layer) // Resterts the age and lets it sit in the colided spot for a short time 
        {
            bulletRigidbody.velocity = Vector3.zero;
            currentAge = 0;
            bulletRigidbody.isKinematic = true;
            transform.position = hitPos;
        }
    }
}
