using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
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
            GetComponent<Enemy_NavWander>().CheckIfIShouldWander();
        }
    }

    void ChasePlayer()
    {
        ////List<Transform> visibleTargets = GetComponent<FieldOfView>().visibleTargets;
        //Transform target = ;
        

        //agent.SetDestination(target.position);
        
    }
}
