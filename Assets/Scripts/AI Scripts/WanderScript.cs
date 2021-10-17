using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderScript : MonoBehaviour
{
    [SerializeField]
    private float wanderRadius;
    [SerializeField]
    private float wanderTimer;

    private Transform targetLoc;
    private NavMeshAgent agent;
    private float timer;

    // this function is called when the object is enabled
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; /// increases timer every second

        if(timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas); ///sets a new position to move to within the wznder radius
            agent.SetDestination(newPos); /// tells the AI to move to the new position
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layerMask);

        return navHit.position;
    }
}
