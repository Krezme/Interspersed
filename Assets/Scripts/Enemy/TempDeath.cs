using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDeath : MonoBehaviour
{
    public EnemyStatisticsManager statisticsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(statisticsManager.currentStats.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
