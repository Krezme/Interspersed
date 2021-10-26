using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(animator.ani);
    }
}
