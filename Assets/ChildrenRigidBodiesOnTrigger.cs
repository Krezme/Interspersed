using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenRigidBodiesOnTrigger : MonoBehaviour
{
    public BoxCollider Shelter;

    public GameObject DestructableObject;

    public List<GameObject> RigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Rigidbody[] rigidbodiesOfAllChild = DestructableObject.GetComponentsInChildren<Rigidbody>();
             for (int i = 0; i < rigidbodiesOfAllChild.Length; i++) 
             {
                 rigidbodiesOfAllChild[i].isKinematic = false;
             }
        }
    }
}