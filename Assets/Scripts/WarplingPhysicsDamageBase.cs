using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarplingPhysicsDamageBase : MonoBehaviour
{

    public int warpmaxHealth = 50;
    public int warpcurrentHealth;
    public WarpHealthbar warphealthbar;

    private Vector3 incomingforceVector3; //the force of the incoming rigidbody in Vector3 form
    public float incomingforceFloat; //force of incoming in float form
    public float minimumforce = 2f; //minimum force to trigger damage
    public float physicsDamageMultiplier = 2f;

    void TakeDamage(int damage)
    {
        warpcurrentHealth -= damage;

        warphealthbar.SetwarpHealth(warpcurrentHealth);
    }

    void Update()
    {
        if (warpcurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        warpcurrentHealth = warpmaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            TakeDamage(20);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>()){}
        else
        {
            incomingforceVector3 = other.gameObject.GetComponent<Rigidbody>().mass * other.gameObject.GetComponent<Rigidbody>().velocity;
            incomingforceFloat = Mathf.Abs(incomingforceVector3.x + incomingforceVector3.y + incomingforceVector3.z);
            if(incomingforceFloat < minimumforce){}
            else
            {
                TakeDamage(Mathf.RoundToInt(incomingforceFloat * physicsDamageMultiplier));
                Debug.Log("Damage taken = " + (Mathf.RoundToInt(incomingforceFloat * physicsDamageMultiplier)));
            }
        }
    }
}
