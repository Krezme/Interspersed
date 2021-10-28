using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_NavWander : MonoBehaviour
{

    private NavMeshAgent agent;
    private float checkRate;
    private float nextCheck;
    [SerializeField]
    private float wanderRange = 10;
    private Transform myTransform;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;

    void OnEnable()
    {
        SetIntialReferences();

    }

    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfIShouldWander();
        }
    }

    void SetIntialReferences()
    {
        if (GetComponent<NavMeshAgent>() != null) /// checking to see if the object has a nav mesh component
        {
            agent = GetComponent<NavMeshAgent>();
        }

        checkRate = Random.Range(0.3f, 0.4f);
        myTransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if(RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
        {
            agent.SetDestination(wanderTarget);
            GetComponent<Enemy_Animation>().EnemyWalking();
        }
        else
        {
            GetComponent<Enemy_Animation>().EnemyIdle();
        }
    }
    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result) 
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange; /// picks a random location within the sphere
        if(NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas)) /// Finds a suitable place to walk to
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre; /// the AI wont move until it finds a suitable spot to move to
            return false;
        }
    }
    
    void DisableThis()
    {
        this.enabled = false;
    }
}
