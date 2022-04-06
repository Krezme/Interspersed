using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAfterEnemiesDead : MonoBehaviour
{
 
    public BoxCollider EnemyZone;

    public LayerMask layers;
    public UnityEvent OnExit;

    private int EnemyCount = 5;


    /* void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyCount++; //Count how many enemies have entered the area
        }
    } */

    void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
            EnemyCount--; //Count how mant enemies have left the area / died
        //}

        if (EnemyCount <= 0)
        {
            ExecuteOnExit(other); //Execute the ExecuteOnExit function when all enemies have gone
        }
    }

    void update()
    {
        Debug.Log("Enemy Count: " + EnemyCount);
    }

    /* void Update()
    {
        if (EnemyCount <= 0)
        {
            ExecuteOnExit(other); //Execute the ExecuteOnExit function when all enemies have gone
        }
    } */

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }
}