using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public bool targetvisible = false;

    private void Start()
    {
        //StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    void OnEnable()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        Debug.Log("Hello --------------------------------------------------------------");
    }

    /* void OnDisable() {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    } */

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear(); /// clears list so there are no duplicate items stored in it
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; /// gets the transform for any target the enemy is looking at
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); /// gets the distance between the target and enemy

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) /// if the raycast doesn't hit an obstacle blocking the target
                {
                    visibleTargets.Add(target); /// adds the target to the visible targets list
                    targetvisible = true;
                }
            }
        }
    }

    
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
