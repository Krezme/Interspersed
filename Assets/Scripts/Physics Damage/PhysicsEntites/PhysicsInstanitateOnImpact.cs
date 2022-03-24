using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInstanitateOnImpact : MonoBehaviour
{
    public float forceRequired;

    public GameObject toInstantiate;

    private Rigidbody thisRigidBody;


    public RandomAudioPlayer ImpactSFX;

    //public RandomAudioPlayer ExplosionSFX;

    public GameObject ExplosionSFXSource;



    private void Start()
    {
        thisRigidBody = this.GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        //if the force of this object is higher than the force required on impact
        if (Mathf.Abs(thisRigidBody.velocity.x) + Mathf.Abs(thisRigidBody.velocity.y) + Mathf.Abs(thisRigidBody.velocity.z) >= forceRequired)
        {
            
            Instantiate(toInstantiate, transform.position, transform.rotation);

            Instantiate(ExplosionSFXSource, transform.position, transform.rotation);
            
            Destroy(this.gameObject);
        }
    }
}
