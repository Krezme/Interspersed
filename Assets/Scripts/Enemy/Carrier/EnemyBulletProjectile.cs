using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBulletStatisticsPlaceHolder {
    public float damage;

    public float speed = 10;
}

[RequireComponent(typeof(Rigidbody))]
public class EnemyBulletProjectile : MonoBehaviour
{
    public EnemyBulletStatisticsPlaceHolder enemyBulletStatistics;

    public Rigidbody projectileRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody.velocity = transform.forward * enemyBulletStatistics.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProjectileStatistics(float damage) {
        enemyBulletStatistics.damage = damage;
    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerStatisticsManager>().TakeDamage(enemyBulletStatistics.damage);
            Destroy(this.gameObject);
            //Debug.Log("EnemyBulletProjectile");
        }
        else if (!other.isTrigger){
            Destroy(this.gameObject);
        }
    }
}
