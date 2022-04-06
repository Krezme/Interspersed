using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAfterEnemiesDead : MonoBehaviour
{
 
    public BoxCollider EnemyZone;

    public LayerMask layers;
    public UnityEvent OnExit;

    [HideInInspector]
    public int EnemyCount = 5;

    void OnTriggerExit(Collider other)
    {
        EnemyCount--; //Count how mant enemies have left the area / died
        Debug.Log("Enemies left: " + EnemyCount);


        if (EnemyCount <= 0)
        {
            ExecuteOnExit(other); //Execute the ExecuteOnExit function when all enemies have gone
        }
    }

        protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }
}