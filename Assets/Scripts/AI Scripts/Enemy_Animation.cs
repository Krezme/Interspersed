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

    public void EnemyWalking()
    {
        animator.SetBool("isWalking", true);
    }
    
    public void EnemyIdle()
    {
        animator.SetBool("isWalking", false);
    }
}
