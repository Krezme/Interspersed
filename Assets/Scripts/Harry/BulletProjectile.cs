 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletStatistics
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float chargeStage;

    public float thisLifespan = 5f;
    public bool isElectric; // If the bullet is electric

    [HideInInspector]
    public float currentAge;

    public float speed = 10f;
}

public class BulletProjectile : MonoBehaviour
{
    public BulletStatistics statistics;

    public Rigidbody bulletRigidbody;

    private bool hasCollided;

    private void Start()
    {
        Debug.Log(statistics.chargeStage * statistics.speed);
        bulletRigidbody.velocity = transform.forward * (statistics.speed * statistics.chargeStage);
    }

    private void Update()
    {
        statistics.currentAge += Time.deltaTime; // Adds age to the bullet 

        if (statistics.currentAge >= statistics.thisLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos = transform.position;
        if (!hasCollided) {

            if (other.tag == "Enemy") { 
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                Destroy(gameObject);
                EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
                enemyStatisticsManager.TakeDamage(statistics.damage);
            }
            else if (other.tag == "WaterPuddle" && statistics.isElectric) {
                other.gameObject.GetComponent<WaterController>().waterProperties.isCharged = statistics.isElectric;
            }
            else if (other.gameObject.layer != this.gameObject.layer) // Resterts the age and lets it sit in the colided spot for a short time 
            {
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                bulletRigidbody.velocity = Vector3.zero;
                statistics.currentAge = 0;
                bulletRigidbody.isKinematic = true;
                transform.position = hitPos;
            }
        }
    }
}
