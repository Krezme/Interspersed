using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCountdown : MonoBehaviour
{
    private float Timer;

    public float TimeBeforeDestroy;

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= TimeBeforeDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}