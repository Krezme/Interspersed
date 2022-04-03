using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulletStatistics
{
    [HideInInspector]
    public float damage; /// damage will be calculated from whatever the AI's damage is

    public float thisLifespan = 5f; ///how long we want the bullet to stay before being destroyed

    [HideInInspector]
    public float currentAge; /// how long it's be spawned for

    public float speed = 10f; /// how fast we want the bullet to move
}

public class EnemyProjectile : MonoBehaviour
{
    public EnemyBulletStatistics statistics; // the bullet's stats
    
    public Rigidbody bulletRigidbody; // the rigidbody on the projectile

    private bool hasCollided; // checks to see if it has collided with anything

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * statistics.speed; // calculates the velocity of the bullet
    }

    // Update is called once per frame
    void Update()
    {
        statistics.currentAge += Time.deltaTime; // Adds age to the bullet 

        // after a certain amount of time the bullet gets destroyed
        if (statistics.currentAge >= statistics.thisLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos = transform.position; // sets the hit position to its position when it collided with something

        if (!hasCollided) // does these checks if it hasn't already collided with something
        {
            if (other.tag == "Player")
            {
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                Destroy(gameObject);
                PlayerStatisticsManager playerStatisticsManager = other.GetComponent<PlayerStatisticsManager>();
                playerStatisticsManager.TakeDamage(statistics.damage); // does damage to the player
            }
            else if (other.gameObject.layer != this.gameObject.layer) // Restarts the age and lets it sit in the collided spot for a short time 
            {
                hasCollided = true; // Effectively dissables the OnTriggerEnter

                // stops it from moving
                bulletRigidbody.velocity = Vector3.zero;
                statistics.currentAge = 0;
                //bulletRigidbody.isKinematic = true;
                transform.position = hitPos;
            }
            else if (!other.isTrigger){
                Destroy(this.gameObject);
            }
        }
        
    }
}
