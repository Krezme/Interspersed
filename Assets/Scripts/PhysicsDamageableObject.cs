using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//If the last velocity is heigher then the minimum set then this scripts will be needed
public class PhysicsDamageableObject : MonoBehaviour
{
    [HideInInspector]
    public List<float> velocityX = new List<float>();
    [HideInInspector]
    public List<float> velocityY = new List<float>();
    [HideInInspector]
    public List<float> velocityZ = new List<float>();

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentNormalizedVelocity = rb.velocity.normalized;
        velocityX.Add(Mathf.Abs(currentNormalizedVelocity.x));
        velocityY.Add(Mathf.Abs(currentNormalizedVelocity.y));
        velocityZ.Add(Mathf.Abs(currentNormalizedVelocity.z));
    }

    public void ShowAvrageOfVelocities() {
        Debug.Log(Mathf.Abs(Queryable.Average(velocityX.AsQueryable())) + ", " + Mathf.Abs(Queryable.Average(velocityY.AsQueryable())) + ", " + Mathf.Abs(Queryable.Average(velocityZ.AsQueryable())));
    }
}
