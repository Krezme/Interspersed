using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaterProperties
{
    public bool isCharged;

    public float damage;
    
    public float numberOfProjectiles; // how many projectiles are currenlty stuck in the puddle and charging it

    public float tickDamageTime;

}

public class WaterController : MonoBehaviour
{

    public WaterProperties waterProperties;

    public List<GameObject> currentlyAffectedEnemies = new List<GameObject>();

    public GameObject electricSparks;

    private float tickTimer;

    public RandomAudioPlayer ElectrifiedPuddle; //Rhys - Puddle sound bank

    public AudioSource audiosource;

    public AudioClip PuddleBuzz;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyAffectedEnemies.Count > 0 && waterProperties.numberOfProjectiles > 0) {
            tickTimer += Time.deltaTime;

            if (tickTimer >= waterProperties.tickDamageTime) {
                tickTimer = 0;
                for (int i = 0; i < currentlyAffectedEnemies.Count; i++) {
                    currentlyAffectedEnemies[i].GetComponent<EnemyStatisticsManager>().TakeDamage(waterProperties.damage * waterProperties.tickDamageTime, true);
                }
            }
        }
        if (waterProperties.numberOfProjectiles <= 0) {
            waterProperties.isCharged = false;
            electricSparks.SetActive(false);
            audiosource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric) {
            waterProperties.numberOfProjectiles++;
            Debug.Log("Enter");
            waterProperties.damage += other.GetComponent<BulletProjectile>().statistics.damage / 2;
            if (waterProperties.numberOfProjectiles == 1){
                electricSparks.SetActive(true);
                //ElectrifiedPuddle.PlayRandomClip(); //Rhys enables pubble source to play
                try {
                    audiosource.PlayOneShot(PuddleBuzz);
                }catch (System.Exception) {}
            }
            
        }
        if (other.tag == "Enemy") {
            currentlyAffectedEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") {
            currentlyAffectedEnemies.Remove(other.gameObject);
        }
    }
}
