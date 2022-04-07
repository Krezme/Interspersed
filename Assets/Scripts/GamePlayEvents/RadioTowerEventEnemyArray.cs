using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTowerEventEnemyArray : MonoBehaviour
{
    

    public GameObject[] EnemiesAlive;





    void Start()
    {
        EnemiesAlive[1].SetActive(false);
    }

    void Update()
    {
        
    }
}
