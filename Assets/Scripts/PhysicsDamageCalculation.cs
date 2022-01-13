using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatisticsManager))]
public class PhysicsDamageCalculation : MonoBehaviour
{

    private Vector3 incomingForceVector3; //the force of the incoming rigidbody in Vector3 form
    public float incomingForceFloat; //force of incoming in float form
    public float minimumForceRequired = 2f; //minimum force to trigger damage
    public float physicsDamageMultiplier = 2f;

    private EnemyStatisticsManager enemyStatisticsManager;
    void Start () {
        enemyStatisticsManager = GetComponent<EnemyStatisticsManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>()){}
        else
        {
            incomingForceVector3 = other.gameObject.GetComponent<Rigidbody>().mass * other.gameObject.GetComponent<Rigidbody>().velocity;
            incomingForceFloat = Mathf.Abs(incomingForceVector3.x + incomingForceVector3.y + incomingForceVector3.z);
            if(incomingForceFloat < minimumForceRequired){}
            else
            {
                enemyStatisticsManager.TakeDamage(Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier));
                Debug.Log("Damage taken = " + (Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier)));
            }
        }
    }
}
