using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class InAirPathFinding : MonoBehaviour
{
    public GameObject objectToFollow;
    public float speed = 100;
    public float rotationSpeed = 1;
    public float stoppingDistance = 1;
    public float rayLenght = 3;
    public float rayOffset = 0.5f;
    public float sphereCastRadius = 0.25f;
    public LayerMask obsticlesLayer;

    private Rigidbody rb;

    private bool goDetectedCenterSphere;
    private bool goDetectedRight;
    private bool goDetectedLeft;
    private bool goDetectedTop;
    private bool goDetectedBottom;

    private List<Vector3> obsticlePositions;
    private RaycastHit centerShperecastHit;
    Vector3 averageObsticlePositions;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        ObstacleManeuvering();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Hit");
    }

    void ObstacleManeuvering() {
        obsticlePositions = new List<Vector3>();
        goDetectedRight = PhysicsRaycast(transform.position + transform.right * rayOffset, transform.forward, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedLeft = PhysicsRaycast(transform.position - transform.right * rayOffset, transform.forward, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedTop = PhysicsRaycast(transform.position + transform.up * rayOffset, transform.forward, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedBottom = PhysicsRaycast(transform.position - transform.up * rayOffset, transform.forward, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedCenterSphere = PhysicsSpherecast(transform.position, transform.forward, obsticlesLayer);
        
        if (obsticlePositions.Count() > 0) {
            averageObsticlePositions = new Vector3(obsticlePositions.Average(x=>x.x), obsticlePositions.Average(y=>y.y), obsticlePositions.Average(z=>z.z));
        }

        if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && goDetectedCenterSphere) 
        {
            Turning(-averageObsticlePositions);
        }
        else if (goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom){}
        else
        {
            Turning(objectToFollow.transform.position);
        }
        Movement();
    }

    void Movement () {
        float distanceToTarget = Vector3.Distance(transform.position, objectToFollow.transform.position);
        if (distanceToTarget > stoppingDistance) {
            rb.velocity = (transform.forward * speed) * Time.deltaTime;
        }else {
            rb.velocity = Vector3.zero;
        }
    }

    void Turning(Vector3 targetPos) {
        Vector3 position = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    bool PhysicsRaycast (Vector3 origin, Vector3 direction, List<Vector3> obsticlePos, out List<Vector3> newObsticlePos, LayerMask layer = default) {
        bool hasDetected = Physics.Raycast(origin, direction, rayLenght, layer)?true:false;
        List<Vector3> tempObsticlePos = obsticlePos;
        if (hasDetected) {
            tempObsticlePos.Add(origin);
        }
        newObsticlePos = tempObsticlePos;
        return hasDetected;
    }

    bool PhysicsSpherecast (Vector3 origin, Vector3 direction, LayerMask layer = default) {
        bool hasDetected = Physics.SphereCast(origin, sphereCastRadius, direction, out centerShperecastHit, rayLenght, layer, QueryTriggerInteraction.UseGlobal);
        return hasDetected;
    }

#region Gizmos
    void OnDrawGizmos () {
        Gizmos.color = goDetectedRight?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + transform.right * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedLeft?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - transform.right * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedTop?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + transform.up * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedBottom?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - transform.up * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedCenterSphere?Color.red:Color.green;
        float distance;
        if (goDetectedCenterSphere) {
            distance = centerShperecastHit.distance;
        }
        else {
            distance = rayLenght;
        }
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, sphereCastRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(averageObsticlePositions, 0.1f);
    }
#endregion
}
