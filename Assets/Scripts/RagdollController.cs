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

    public GameObject mesh;
    public bool pickedUpByPlayer;
    public bool ragdolling;

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
        thisRigidbody.isKinematic = true;
    }

    private void GatherRagdollColliders () {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    /// <summary>
    /// Turn's off the ragdoll by; enableing the agent, animator, collider and disabling the ragdoll colliders 
    /// </summary>
    [ContextMenu("RagdollOff")]
    public void RagdollOff(){
        for (int i = 0; i < ragdollColliders.Length; i++) {
            ragdollColliders[i].enabled = false;
            ragdollRigidbodies[i].useGravity = false;
            ragdollRigidbodies[i].isKinematic = true;
        }
        if (thisCollider != null) {
            thisCollider.enabled = true;
        }
        if (thisRigidbody != null) {
            thisRigidbody.useGravity = false;
            thisRigidbody.isKinematic = true;
        }
        if (thisAnimatior != null) {
            thisAnimatior.enabled = true;
        }
        if (agent != null) {
            agent.enabled = true;
        }
        ragdolling = false;
        transform.position = mesh.transform.position;
    }

    /// <summary>
    /// Turn's on ragdoll by: disabling the agent, animator, collider and enabling the ragdoll colliders 
    /// </summary>
    [ContextMenu("RagdollOn")]
    public void RagdollOn(){
        for (int i = 0; i < ragdollColliders.Length; i++) {
            ragdollColliders[i].enabled = true;
            ragdollRigidbodies[i].useGravity = true;
            ragdollRigidbodies[i].isKinematic = false;
        }
        if (thisCollider != null) {
            thisCollider.enabled = false;
        }
        if (thisRigidbody != null) {
            thisRigidbody.useGravity = false;
            thisRigidbody.isKinematic = true;
        }
        if (thisAnimatior != null) {
            thisAnimatior.enabled = false;
        }
        if (agent != null) {
            agent.enabled = false;
        }
        ragdolling = true;
        StartCoroutine(PauseBeforeRagdollOff());
    }

    IEnumerator PauseBeforeRagdollOff() {
        while (true) {
            yield return new WaitForSeconds(0.1f);
            if (!pickedUpByPlayer) { 
                yield return new WaitForSeconds(1f);
            }
            if (!pickedUpByPlayer && ragdolling) {
                RagdollOff();
                break;
            }
            else if (!ragdolling) {
                break;
            }
            if (ragdolling && pickedUpByPlayer && !PlayerAbilitiesController.instance.isAbilityActive) {
                pickedUpByPlayer = false;
                RagdollOff();
                break;
            }
        }
    } 
}
