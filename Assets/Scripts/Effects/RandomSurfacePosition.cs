using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSurfacePosition : MonoBehaviour
{
    public RayCastsDirectionsInASphere rayCastsDir;

    private RaycastHit hit;
    
    private int currentTries;

    void OnEnable() {
        InvokeRepeating(nameof(LookForSurface), 0, 0.5f);
    }

    void OnDisable() {
        Invoke(nameof(SetActiveTrue), 4f);
    }

    void SetActiveTrue() {
        // turn the effect on
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTries < 10) {
            transform.position = hit.point;
        }else {
            transform.position = rayCastsDir.lightningDefaultTarget.transform.position;
        }
        transform.rotation = Quaternion.Euler(hit.normal);
    }

    void LookForSurface() {
        currentTries = 0;
        PerformRandomRayCast();
    }

    void PerformRandomRayCast() {
        if (currentTries < 10) {
            int rnd = Random.Range(0, rayCastsDir.rayDirections.Count);
            if (Physics.Raycast(rayCastsDir.spherecastOrigin, rayCastsDir.rayDirections[rnd], out hit, rayCastsDir.maxRadius, rayCastsDir.layerMask, QueryTriggerInteraction.UseGlobal)) {}
            else{
                currentTries++;
                PerformRandomRayCast();
            }
        }
    }

}