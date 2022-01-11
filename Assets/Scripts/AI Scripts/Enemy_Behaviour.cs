using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        bool targetvisible = GetComponent<FieldOfView>().targetvisible;

        if(targetvisible == true)
        {
            ChasePlayer();
        }
        else
        {
            //GetComponent<Enemy_NavWander>().CheckIfIShouldWander();
        }
    }

    void ChasePlayer()
    {
        List<Transform> visibleTargets = GetComponent<FieldOfView>().visibleTargets;
        if (visibleTargets != null && visibleTargets.Count > 0)
        {
            int rnd = Random.Range(0, visibleTargets.Count);
            target = visibleTargets[rnd];
        }
        
        agent.updateRotation = true;
        agent.SetDestination(target.position);
    }
}
