using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour
{
    public Animator animator;
    float velocity = 0.0f;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    /* public void EnemyWalking(float speed)
    {
        Debug.Log(speed);
        //animator.SetBool("isWalking", true);
        animator.SetFloat("walking", speed);
    } */
    
    public void EnemyMovement(float speed)
    {
        //animator.SetBool("isWalking", false);
        animator.SetFloat("walking", speed, 0.1f, Time.deltaTime);
    }
}
