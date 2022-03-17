using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulletStatistics
{
    [HideInInspector]
    public float damage;

    public float thisLifespan = 5f;

    [HideInInspector]
    public float currentAge;

    public float speed = 10f;
}

public class EnemyProjectile : MonoBehaviour
{
    public EnemyBulletStatistics statistics;
    
    public Rigidbody bulletRigidbody;

    private bool hasCollided;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * statistics.speed;
    }

    // Update is called once per frame
    void Update()
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

        if (!hasCollided)
        {
            if (other.tag == "Player")
            {
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                Destroy(gameObject);
                PlayerStatisticsManager playerStatisticsManager = other.GetComponent<PlayerStatisticsManager>();
                playerStatisticsManager.TakeDamage(statistics.damage);
            }
            else if (other.gameObject.layer != this.gameObject.layer) // Restarts the age and lets it sit in the collided spot for a short time 
            {
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                bulletRigidbody.velocity = Vector3.zero;
                statistics.currentAge = 0;
                //bulletRigidbody.isKinematic = true;
                transform.position = hitPos;
            }
        }
        
    }
}
