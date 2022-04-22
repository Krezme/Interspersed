using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour
{
    private Collider thisCollider; // The Collider in the parent game object
    private Rigidbody thisRigidbody; // The Rigidbody in the parent game object
    private Animator thisAnimatior; // The Animator in the parent game object
    private NavMeshAgent agent; // The NavMeshAgent in the parent game object


    private Transform rigPositionOffset; 

    [HideInInspector]
    public Collider[] ragdollColliders; // all colliders used for the ragdoll in the joints
    [HideInInspector]
    public Rigidbody[] ragdollRigidbodies; // all rigidbodies used for the ragdoll in the joints

    public GameObject rig; // rig of the character
    public bool pickedUpByPlayer; // if it is picked up by the player
    public bool ragdolling; // if it is currently ragdolling
    public EnemyStatisticsManager enemyStatisticsManager; // The enemy statistics manager attacked to the root game object
    public MonoBehaviour[] monoBehaviourToggle; // scripts to toggle (on and off) when ragdolling
    public bool objectMovesWithRigidbodyPhysics;

    public GameObject rigCentre; // the centre of the rig
    public Renderer meshRenderer;
    public float slimeSphereSizeMultyplier = 1;

    public LayerMask groundLayerMasks;
    public bool needsGround;

    private int standUpTries;

    void Start()
    {
        // Finding and assigning all required components
        TryGetComponent<Collider>(out thisCollider);
        TryGetComponent<Rigidbody>(out thisRigidbody);
        TryGetComponent<Animator>(out thisAnimatior);
        TryGetComponent<NavMeshAgent>(out agent);

        GatherRagdollColliders();
        // Makes sure that the ragdoll is turned off
        RagdollOff();
    }

    void Update()
    {
        //thisRigidbody.isKinematic = true;
    }

    /// <summary>
    /// Finding all colliders and rigidbodies used for the ragdolling and saves them
    /// </summary>
    private void GatherRagdollColliders () {
        ragdollColliders = rig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = rig.GetComponentsInChildren<Rigidbody>();
    }

    /// <summary>
    /// Turn's off the ragdoll by; enableing the agent, animator, collider and disabling the ragdoll colliders
    /// This is so the enemy can function properly and in one piece when the ragdoll is OFF
    /// </summary>
    [ContextMenu("RagdollOff")]
    public void RagdollOff(Vector3 standUpPosition = new Vector3()){
        standUpTries = 0;
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
            thisRigidbody.isKinematic = !objectMovesWithRigidbodyPhysics;
        }
        if (thisAnimatior != null) {
            thisAnimatior.enabled = true;
        }
        if (agent != null) {
            agent.enabled = true;
        }
        ragdolling = false;
        if (standUpPosition != new Vector3()) {
            transform.position = standUpPosition;
            Debug.Log("Works");
        }else {
            transform.position = rig.transform.position;
        }
    }

    /// <summary>
    /// Turn's on ragdoll by: disabling the agent, animator, collider and enabling the ragdoll colliders
    /// This is so the enemy can now be affected by gravity and the individual pieces as well
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

    /// <summary>
    /// Turning off the ragdoll after a small down time
    /// </summary>
    /// <returns></returns>
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
                if (needsGround) {
                    CheckForGround();
                }else {
                    RagdollOff();
                }
                break;
            }
            else if (!ragdolling) {
                break;
            }
        }
    } 

    void CheckForGround() {
        Debug.Log("CheckForGround");
        if (Physics.Raycast(rig.transform.position, Vector3.down, out RaycastHit hit, 2f, groundLayerMasks, QueryTriggerInteraction.UseGlobal)) {
            Debug.Log("CheckForGround 1");
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 3.0f, NavMesh.AllAreas)) {
                Debug.Log("CheckForGround 2");
                RagdollOff(standUpPosition: navHit.position);
            }else {
                standUpTries++;
                Debug.Log("CheckForGround -1 " + standUpTries);
                if (standUpTries <= 10){
                    StartCoroutine(PauseBeforeRagdollOff());
                }
                else {
                    Destroy(this.gameObject);
                }
            }
        }
        else{
            standUpTries++;
            Debug.Log("CheckForGround -2 " + standUpTries);
            if (standUpTries <= 10){
                StartCoroutine(PauseBeforeRagdollOff());
            }
            else {
                Destroy(this.gameObject);
            }
        }
    }
}
