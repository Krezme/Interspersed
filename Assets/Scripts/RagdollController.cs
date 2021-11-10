using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RagdollController : MonoBehaviour
{
    private Collider thisCollider;
    private Rigidbody thisRigidbody;
    private Animator thisAnimatior;
    private NavMeshAgent agent;

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    void Start()
    {
        TryGetComponent<Collider>(out thisCollider);
        TryGetComponent<Rigidbody>(out thisRigidbody);
        TryGetComponent<Animator>(out thisAnimatior);
        TryGetComponent<NavMeshAgent>(out agent);
        GatherRagdollColliders();
        RagdollOff();
    }

    void Update()
    {
        
    }

    private void GatherRagdollColliders () {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void RagdollOff(){
        for (int i = 0; i < ragdollColliders.Length; i++) {
            ragdollColliders[i].enabled = false;
            ragdollRigidbodies[i].isKinematic = true;
        }
        if (thisCollider != null) {
            thisCollider.enabled = true;
        }
        if (thisRigidbody != null) {
            thisRigidbody.isKinematic = false;
        }
        if (thisAnimatior != null) {
            thisAnimatior.enabled = true;
        }
        if (agent != null) {
            agent.enabled = true;
        }
    }

    public void RagdollOn(){
        for (int i = 0; i < ragdollColliders.Length; i++) {
            ragdollColliders[i].enabled = true;
            ragdollRigidbodies[i].isKinematic = false;
        }
        if (thisCollider != null) {
            thisCollider.enabled = false;
        }
        if (thisRigidbody != null) {
            thisRigidbody.isKinematic = true;
        }
        if (thisAnimatior != null) {
            thisAnimatior.enabled = false;
        }
        if (agent != null) {
            agent.enabled = false;
        }
    }
}
