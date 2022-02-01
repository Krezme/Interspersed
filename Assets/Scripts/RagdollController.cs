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

    [HideInInspector]
    public Collider[] ragdollColliders;
    [HideInInspector]
    public Rigidbody[] ragdollRigidbodies;

    public GameObject rig;
    public bool pickedUpByPlayer;
    public bool ragdolling;
    public EnemyStatisticsManager enemyStatisticsManager;
    public MonoBehaviour[] monoBehaviourToggle;

    public GameObject rigCentre;

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
        for (int i = 0; i < monoBehaviourToggle.Length; i++) {
            monoBehaviourToggle[i].enabled = true;
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
        transform.position = rig.transform.position;
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
        for (int i = 0; i < monoBehaviourToggle.Length; i++) {
            monoBehaviourToggle[i].enabled = false;
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
            if (enemyStatisticsManager.currentStats.health <= 0) {
                break;
            }
            if (!pickedUpByPlayer) { 
                yield return new WaitForSeconds(5f);
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
