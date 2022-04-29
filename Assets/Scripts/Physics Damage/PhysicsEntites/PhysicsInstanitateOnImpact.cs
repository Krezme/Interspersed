using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInstanitateOnImpact : MonoBehaviour
{
    public float forceRequired;

    public GameObject Smoke;

    public GameObject Fire;


    public GameObject InstantiatedSphereCol;

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
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;


        //if the force of this object is higher than the force required on impact
        if (Mathf.Abs(thisRigidBody.velocity.x) + Mathf.Abs(thisRigidBody.velocity.y) + Mathf.Abs(thisRigidBody.velocity.z) >= forceRequired)
        {

            Instantiate(InstantiatedSphereCol, pos, rot);
            
            Instantiate(Smoke, pos, rot);

            Instantiate(Fire, pos, rot);


            Instantiate(ExplosionSFXSource, pos, rot);


            Destroy(this.gameObject);
        }
    }



}
