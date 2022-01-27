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
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody otherRB)){
            float testTemp = Mathf.Abs(otherRB.velocity.x) + Mathf.Abs(otherRB.velocity.y) + Mathf.Abs(otherRB.velocity.z);
            //Debug.Log(testTemp);
            if (Mathf.Abs(otherRB.velocity.x) + Mathf.Abs(otherRB.velocity.y) + Mathf.Abs(otherRB.velocity.z) >= minimumForceRequired) {
                PhysicsDamageableObject physicsDamageableObject;
                if (other.gameObject.TryGetComponent<PhysicsDamageableObject>(out physicsDamageableObject)) {

                    Vector3 averageVelocity = physicsDamageableObject.AverageOfVelocities();

                    incomingForceVector3 = otherRB.mass * averageVelocity;
                    
                    incomingForceFloat = Mathf.Abs(Mathf.Abs(incomingForceVector3.x) + Mathf.Abs(incomingForceVector3.y) + Mathf.Abs(incomingForceVector3.z));
                    enemyStatisticsManager.TakeDamage(Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier));
                    Debug.Log("Damage taken = " + (Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier)));
                }
            }
        }
    }
}
