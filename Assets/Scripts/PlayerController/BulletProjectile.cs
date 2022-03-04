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

    public GameObject electricSparks;

    private WaterController inWaterController; 

    private bool inPuddle;

    private bool hasCollided;

    public RandomAudioPlayer BulletImpact; //Rhys - Adds functionality for bullet to play impact sound, also opens up the possibility of different surface impact sounds



    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * (statistics.speed * statistics.chargeStage);
        if (statistics.isElectric) {
            electricSparks.SetActive(true);
        }
    }

    private void Update()
    {
        statistics.currentAge += Time.deltaTime; // Adds age to the bullet 

        if (statistics.currentAge >= statistics.thisLifespan)
        {
            if (inPuddle) {
                inWaterController.waterProperties.numberOfProjectiles--;
                inWaterController.waterProperties.damage -= statistics.damage / 2;
                if (inWaterController.waterProperties.numberOfProjectiles <= 0) {
                    inWaterController.waterProperties.isCharged = false;
                }
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPos = transform.position;
        if (!hasCollided) {

            BulletImpact.PlayRandomClip();

            if (other.tag == "Enemy") { 
                hasCollided = true; // Effectively dissables the OnTriggerEnter
                Destroy(gameObject);
                EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
                enemyStatisticsManager.TakeDamage(statistics.damage);
            }
            else if (other.tag == "Chargeable" && statistics.isElectric) {
                other.gameObject.GetComponent<ChargableManager>().OnCharged();
            }
            else if (other.tag == "WaterPuddle") {
                if (statistics.isElectric){
                    inPuddle = true;
                    inWaterController = other.gameObject.GetComponent<WaterController>();
                    inWaterController.waterProperties.isCharged = statistics.isElectric;
                }
            }
            else if (other.gameObject.layer != this.gameObject.layer) // Resterts the age and lets it sit in the colided spot for a short time 
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