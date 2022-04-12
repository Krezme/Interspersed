using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamageScript : MonoBehaviour
{
    public float explosionDamage = 100f;

    

  
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {


            EnemyStatisticsManager enemyStatisticsManager = other.gameObject.GetComponent<EnemyStatisticsManager>();
            enemyStatisticsManager.TakeDamage(explosionDamage, true);
        }
    }
}
