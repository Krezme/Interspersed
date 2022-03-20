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
    [Range(0, 2)]
    public float rayBackwardsOffsetMultiplier = 0.5f;
    public float directionalSphereCastRadiusMultiplier = 1.33f;

    public LayerMask obsticlesLayer;

    private float currentRotationSpeed;

    private Vector3 blockedPathBackDir = new Vector3(); // The backwards position of the enemy when forward path is blocked

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
    private List<float> obsticleDistances;
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
        if (other.tag != "Player") {
            // ! Trigger Ragdoll when it hits a obsitcle with enough speed
            Debug.Log("Hit");
        }
    }

    /// <summary>
    /// Main function that calls all of the turning conditions and movement code
    /// </summary>
    void ObstacleManeuvering() {
        // Calculates the diraction from the enemy to the target
        directionToTarget = (objectToFollow.transform.position - transform.position).normalized;
        // Performs all physics casts (Ray casts and Sphere Casts) needed for Maneuvering
        PhysicsCasts();
        
        // Calculates the avrage obsicle position. Used to make the player turn in the correct direction
        if (obsticlePositions.Count() > 0) {
            averageObsticlePositions = new Vector3(obsticlePositions.Average(x=>x.x), obsticlePositions.Average(y=>y.y), obsticlePositions.Average(z=>z.z));
        }

        // Checking ray cast data and steering the enemy in corresponding direction
        TurningConditions();
        
        // Moving the enemy
        Movement();
    }

    void PhysicsCasts() {
        obsticlePositions = new List<Vector3>();
        obsticleDistances = new List<float>();
        //Shperecasts
        goDetectedInDirectionToTargetSphere = PhysicsSpherecast(transform.position, directionToTarget, rayLenght, sphereCastRadius * directionalSphereCastRadiusMultiplier, out directionToTargetShpereCastHit, obsticlesLayer);
        goDetectedInAvrageDirectionToTargetSphere = PhysicsSpherecast(transform.position, CalculatePassThroughPosition(), rayLenght/3, sphereCastRadius * directionalSphereCastRadiusMultiplier, out avrageDirectionToTargetSphereHit, obsticlesLayer);
        goDetectedCenterSphere = PhysicsSpherecast(transform.position, transform.forward, rayLenght, sphereCastRadius, out centerShpereCastHit, obsticlesLayer);

        //Raycasts
        goDetectedRight = PhysicsRaycast(transform.position, (transform.right - (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedLeft = PhysicsRaycast(transform.position, - (transform.right + (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedTop = PhysicsRaycast(transform.position, (transform.up - (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        goDetectedBottom = PhysicsRaycast(transform.position, - (transform.up + (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
    }

    void Movement () {
        float distanceToTarget = Vector3.Distance(transform.position, objectToFollow.transform.position);
        if (distanceToTarget > stoppingDistance) {
            rb.velocity = (speed * Time.deltaTime) * transform.forward;
        }else {
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Contails all of the contidions and functions for steering the enemy
    /// </summary>
    void TurningConditions() {
        
        if (goDetectedRight && goDetectedLeft && goDetectedTop && goDetectedBottom) {
            speed = 0;
            if (blockedPathBackDir == new Vector3()) {
                blockedPathBackDir = -this.transform.forward;
            }
            Turning(transform.position + blockedPathBackDir);
        }
        else {
            speed = 100;
            if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && goDetectedCenterSphere) 
            {
                Turning(transform.position - averageObsticlePositions); //Turn in the oposite direction of the obsicles
            }
            else if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && (!goDetectedInDirectionToTargetSphere && !goDetectedInAvrageDirectionToTargetSphere)) {
                Turning(objectToFollow.transform.position); //Turn towards the target
            }
            else if (goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom){}
            else
            {
                Turning(objectToFollow.transform.position); //Turn towards the target
            }
        }
    }

    /// <summary>
    /// Steering the enemy to the passed position
    /// </summary>
    /// <param name="targetPos"> The target position for the enemy to look at</param>
    void Turning(Vector3 targetPos) {
        Debug.Log("Turning(transform.position - blockedPathBackDir);");
        Vector3 position = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(position);
        if (obsticleDistances.Count > 0) {
            currentRotationSpeed = (1 - (obsticleDistances.Min() / rayLenght)) * rotationSpeed; //rotation speed calculated from the distance to closest obsticle
        }else {
            currentRotationSpeed = rotationSpeed;
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    Vector3 CalculatePassThroughPosition()
    {
        return (transform.forward.normalized + directionToTarget.normalized).normalized;
        
    }

    bool PhysicsRaycast (Vector3 centerPos, Vector3 rayOffsetPos, Vector3 direction, float rayCastLenght, List<Vector3> obsticlePos, out List<Vector3> newObsticlePos, LayerMask layer) {
        RaycastHit hit;
        // centerPos + rayOffsetPos, direction, rayCastLenght, layer
        bool hasDetected = Physics.Raycast(centerPos + rayOffsetPos, direction, out hit, rayCastLenght, layer)?true:false;
        List<Vector3> tempObsticlePos = obsticlePos;
        if (hasDetected) {
            tempObsticlePos.Add(rayOffsetPos);
            obsticleDistances.Add(hit.distance);
        }
        newObsticlePos = tempObsticlePos;
        return hasDetected;
    }

    bool PhysicsSpherecast (Vector3 origin, Vector3 direction, float rayCastLenght , float sphereRadius, out RaycastHit hit, LayerMask layer) {
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
            distance = rayLenght/2;
        }
        Gizmos.DrawLine(transform.position, transform.position + CalculatePassThroughPosition() * distance);
        Gizmos.DrawWireSphere(transform.position + CalculatePassThroughPosition() * distance, sphereCastRadius * directionalSphereCastRadiusMultiplier);

        Gizmos.color = goDetectedRight?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + (transform.right - (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward * (rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2))));

        Gizmos.color = goDetectedLeft?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - (transform.right + (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward * (rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2))));

        Gizmos.color = goDetectedTop?Color.red:Color.green;
        Gizmos.DrawRay(transform.position + (transform.up - (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward * (rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2))));

        Gizmos.color = goDetectedBottom?Color.red:Color.green;
        Gizmos.DrawRay(transform.position - (transform.up + (transform.forward * rayBackwardsOffsetMultiplier)) * rayOffset, transform.forward * (rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2))));

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
            Gizmos.DrawSphere(transform.position - averageObsticlePositions,  0.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + averageObsticlePositions, 0.1f);
        }
    }
#endregion
}
