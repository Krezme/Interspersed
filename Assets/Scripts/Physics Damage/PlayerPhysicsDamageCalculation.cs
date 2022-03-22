using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatisticsManager))]
public class PlayerPhysicsDamageCalculation : MonoBehaviour
{

    private Vector3 incomingForceVector3; //the force of the incoming rigidbody in Vector3 form
    public float incomingForceFloat; //force of incoming in float form
    public float minimumForceRequired = 2f; //minimum force to trigger damage
    public float physicsDamageMultiplier = 2f;

    private PlayerStatisticsManager playerStatisticsManager;
    void Start()
    {
        playerStatisticsManager = GetComponent<PlayerStatisticsManager>();
    }

    /// <summary>
    /// When registering a collision it gets the current velocity of the object and compares it to the minimum force required
    /// when the condition is met this calculates the avrage velocity of the object trough its lunched state to get a consitent damage
    /// </summary>
    /// <param name="other">The collided game object</param>
    private void OnCollisionEnter(Collision other)
    {

        Debug.Log("PlayerHit By physics");
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody otherRB))
        {
            if (Mathf.Abs(otherRB.velocity.x) + Mathf.Abs(otherRB.velocity.y) + Mathf.Abs(otherRB.velocity.z) >= minimumForceRequired)
            {
                PhysicsDamageableObject physicsDamageableObject;
                if (other.gameObject.TryGetComponent<PhysicsDamageableObject>(out physicsDamageableObject))
                {

                    Vector3 averageVelocity = physicsDamageableObject.AverageOfVelocities();

                    incomingForceVector3 = otherRB.mass * averageVelocity;

                    incomingForceFloat = Mathf.Abs(Mathf.Abs(incomingForceVector3.x) + Mathf.Abs(incomingForceVector3.y) + Mathf.Abs(incomingForceVector3.z));
                    playerStatisticsManager.TakeDamage(Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier));
                    //Debug.Log("Damage taken = " + (Mathf.RoundToInt(incomingForceFloat * physicsDamageMultiplier)));
                }
            }
        }
    }
}
