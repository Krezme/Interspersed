using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastsDirectionsInASphere : MonoBehaviour
{
    public GameObject root;

    public GameObject origin;

    public Vector3 originOffset;

    public float maxRadius;
    
    public LayerMask layerMask;

    [Range(15, 360)] // ! Please for the love of god do NOT enter 1 in the inverseResolution variable
    public float inverseResolution = 41f;

    [HideInInspector]
    public Vector3 spherecastOrigin;

    private Vector3 direction;

    private Ray ray;

    public List<Vector3> rayDirections;    // Start is called before the first frame update

    public bool showGizoms = false;

    void Start()
    {
        GetRayCastsDirectionsInASphere();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spherecastOrigin = origin.transform.position + ((root.transform.right * originOffset.x) + (root.transform.up * originOffset.y) + (root.transform.forward * originOffset.z));
    }

    void GetRayCastsDirectionsInASphere() {
        ray = new Ray();
        ray.origin = spherecastOrigin;
        direction = Vector3.one.normalized;
        int steps = Mathf.FloorToInt(360f / inverseResolution);
        Quaternion xRotation = Quaternion.Euler (Vector3.right * inverseResolution);
        Quaternion yRotation = Quaternion.Euler(Vector3.up * inverseResolution);
        Quaternion zRotation = Quaternion.Euler(Vector3.forward * inverseResolution);
        for(int i=0; i < steps; i++) {
            direction = xRotation * direction;
            for(int j=0; j < steps; j++) {
                direction = yRotation * direction;
                for(int k=0; k < steps; k++) {
                    direction = zRotation * direction;
                    rayDirections.Add(direction);
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        if (showGizoms){
            spherecastOrigin = origin.transform.position + ((root.transform.right * originOffset.x) + (root.transform.up * originOffset.y) + (root.transform.forward * originOffset.z));
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spherecastOrigin, maxRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spherecastOrigin, maxRadius/10);
        } 
    }
}
