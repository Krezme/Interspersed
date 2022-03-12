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
    public float directionalSphereCastRadiusMultiplier = 1.33f;

    public LayerMask obsticlesLayer;

    private Rigidbody rb;

    private bool goDetectedInDirectionToTargetSphere;
    private bool goDetectedInAvrageDirectionToTargetSphere;
    private bool goDetectedCenterSphere;

    private bool goDetectedRight;
    private bool goDetectedLeft;
    private bool goDetectedTop;
    private bool goDetectedBottom;

    private Vector3 directionToTarget;

    private List<Vector3> obsticlePositions;
    private RaycastHit directionToTargetShpereCastHit;
    private RaycastHit avrageDirectionToTargetSphereHit;
    private RaycastHit centerShpereCastHit;
    private Vector3 averageObsticlePositions;

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
        directionToTarget = (objectToFollow.transform.position - transform.position).normalized;

        PhysicsCasts();
        
        if (obsticlePositions.Count() > 0) {
            averageObsticlePositions = new Vector3(obsticlePositions.Average(x=>x.x), obsticlePositions.Average(y=>y.y), obsticlePositions.Average(z=>z.z));
        }
        Debug.Log("averageObsticlePositions: " + averageObsticlePositions + " directionToTarget: " + directionToTarget);
        if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && goDetectedCenterSphere) 
        {
            Turning(transform.position - (averageObsticlePositions - transform.position));
        }
        else if (goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom){}
        else
        {
            Turning(objectToFollow.transform.position);
        }
        Movement();
    }

    void PhysicsCasts() {
        obsticlePositions = new List<Vector3>();
        //Shperecasts
        goDetectedInDirectionToTargetSphere = PhysicsSpherecast(transform.position, transform.position + directionToTarget, rayLenght, sphereCastRadius * directionalSphereCastRadiusMultiplier, out directionToTargetShpereCastHit, obsticlesLayer);
        goDetectedInAvrageDirectionToTargetSphere = PhysicsSpherecast(transform.position, transform.position + CalculatePassThroughPosition(), rayLenght/3, sphereCastRadius * directionalSphereCastRadiusMultiplier, out avrageDirectionToTargetSphereHit, obsticlesLayer);
        goDetectedCenterSphere = PhysicsSpherecast(transform.position, transform.forward, rayLenght, sphereCastRadius, out centerShpereCastHit, obsticlesLayer);

        //Raycasts
        goDetectedRight = PhysicsRaycast(transform.position, transform.right * rayOffset, transform.forward, rayLenght, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedLeft = PhysicsRaycast(transform.position, - transform.right * rayOffset, transform.forward, rayLenght, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedTop = PhysicsRaycast(transform.position, transform.up * rayOffset, transform.forward, rayLenght, obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedBottom = PhysicsRaycast(transform.position, - transform.up * rayOffset, transform.forward, rayLenght, obsticlePositions, out obsticlePositions, obsticlesLayer);
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

    Vector3 CalculatePassThroughPosition()
    {
        return transform.forward.normalized + directionToTarget.normalized;
    }

    bool PhysicsRaycast (Vector3 centerPos, Vector3 rayOffsetPos, Vector3 direction, float rayCastLenght, List<Vector3> obsticlePos, out List<Vector3> newObsticlePos, LayerMask layer = default) {
        bool hasDetected = Physics.Raycast(centerPos + rayOffsetPos, direction, rayCastLenght, layer)?true:false;
        List<Vector3> tempObsticlePos = obsticlePos;
        if (hasDetected) {
            tempObsticlePos.Add(centerPos + rayOffsetPos);
        }
        newObsticlePos = tempObsticlePos;
        return hasDetected;
    }

    bool PhysicsSpherecast (Vector3 origin, Vector3 direction, float rayCastLenght , float sphereRadius, out RaycastHit hit, LayerMask layer = default) {
        bool hasDetected = Physics.SphereCast(origin, sphereRadius, direction, out hit, rayCastLenght, layer, QueryTriggerInteraction.UseGlobal);
        return hasDetected;
    }

#region Gizmos
    void OnDrawGizmos () {
        Gizmos.color = goDetectedInDirectionToTargetSphere?Color.magenta:Color.blue;
        float distance;
        if (goDetectedInDirectionToTargetSphere) {
            distance = directionToTargetShpereCastHit.distance;
        }
        else {
            distance = rayLenght;
        }
        Gizmos.DrawLine(transform.position, transform.position + directionToTarget * distance);
        Gizmos.DrawWireSphere(transform.position + directionToTarget * distance, sphereCastRadius * directionalSphereCastRadiusMultiplier);

        Gizmos.color = goDetectedInAvrageDirectionToTargetSphere?Color.yellow:Color.cyan;
        distance = 0;
        if (goDetectedInAvrageDirectionToTargetSphere) {
            distance = avrageDirectionToTargetSphereHit.distance;
        }
        else {
            distance = rayLenght/3;
        }
        Gizmos.DrawLine(transform.position, transform.position + CalculatePassThroughPosition() * distance);
        Gizmos.DrawWireSphere(transform.position + CalculatePassThroughPosition() * distance, sphereCastRadius * directionalSphereCastRadiusMultiplier);

        Gizmos.color = goDetectedRight?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + transform.right * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedLeft?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - transform.right * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedTop?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + transform.up * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedBottom?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - transform.up * rayOffset, transform.forward * rayLenght);

        Gizmos.color = goDetectedCenterSphere?Color.red:Color.green;
        distance = 0;
        if (goDetectedCenterSphere) {
            distance = centerShpereCastHit.distance;
        }
        else {
            distance = rayLenght;
        }
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * distance, sphereCastRadius);

        if (goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom){
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position - (averageObsticlePositions - transform.position),  0.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(averageObsticlePositions, 0.1f);
        }
    }
#endregion
}
