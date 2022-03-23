using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// The movement statistics for this enemy
/// </summary>
[System.Serializable]
public class MovementStatistics {
    public float maxSpeed = 100; // The max speed the enemy is allowed to move at
    [HideInInspector]
    public float currentSpeed; // The current speed of the enemy
    public float minSpeed = 50; // The minimum move speed when (When it needs to be slow but not stopped)
    public float changeSpeedMultiplier = 1; // a multiplier used to change the speed of the enemy while moving around
    public float rotationSpeed = 2; // the rotation speed of the enemy
    [HideInInspector]
    public float currentRotationSpeed; // the current rotation speed of the enemy
    public float stoppingDistance = 1; // the distance it will stop when it has reached the destination
}

[RequireComponent(typeof(Rigidbody))]
public class InAirPathFinding : MonoBehaviour
{
    public GameObject objectToFollow; // target for the enemy
    public MovementStatistics movementStatistics; 
    public float rayLenght = 3; // the forward lenght from local position Z 0 for all rays 
    public float rayOffset = 0.5f; // a multipliyer to change the sideways offset for the forward 4 rays
    [Range(0, 2)]
    public float rayBackwardsOffsetMultiplier = 1.12f; // The multiplier for the ray lenght to stretch backwards (works like peripheral vision) and continue avoiding obsticles that are close, but still behind the enemy
    public float sphereCastRadius = 0.3f; // initial radius size for all sphere casts
    public float directionalSphereCastRadiusMultiplier = 1.12f; // multiplier for spherecasts that need to be slightly different size, but still controlled by normal sphere cast
    
    public LayerMask obsticlesLayer; // all layers that need to be avoided

    public bool showGizmos = false; // if true it will draw gizmos (mostly for debugging purposes and showcase)

    private Vector3 blockedPathBackDir = new Vector3(); // The backwards position of the enemy when forward path is blocked

    private Rigidbody rb; // This rigid body that has all of the force

    private bool goDetectedInDirectionToTargetSphere; // if the direcational sphere cast has detected any obsticles (Always points in direction of the target)
    private bool goDetectedInAvrageDirectionToTargetSphere; // if the avrage directional sphere cast has detected any obsticles (Always is in the exact middle between the central and directional sphere cast)
    private bool goDetectedCenterSphere; // if the central sphere cast has detected any obsticles (Always points right infornt of the enemy)

    private bool goDetectedRight; // if the forward right ray cast has detected anything 
    private bool goDetectedLeft; // if the forward left ray cast has detected anything 
    private bool goDetectedTop; // if the forward top ray cast has detected anything 
    private bool goDetectedBottom; // if the bottom right ray cast has detected anything 

    private Vector3 directionToTarget; // the direction to the target

    private List<Vector3> obsticlePositions; // a list of the positions of all the currently detected obsticles from the forward: right, left, bottom and top rays
    private List<float> obsticleDistances; // a list of the distances of all the currently detected obsticles from the forward: right, left, bottom and top rays
    private RaycastHit directionToTargetShpereCastHit; // hit data from the directional sphere cast
    private RaycastHit avrageDirectionToTargetSphereHit; // hit data from the avrage directional sphere cast
    private RaycastHit centerShpereCastHit; // hit data from the forward central sphere cast
    private Vector3 averageObsticlePositions; // the avrage obsicle position (calculated from obsticlePositions)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        ObstacleManeuvering();
    }

    /// <summary>
    /// If this game object has hit something
    /// </summary>
    /// <param name="other"> the other game object's collider </param>
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

    /// <summary>
    /// Preforming all physics casts
    /// </summary>
    void PhysicsCasts() {
        obsticlePositions = new List<Vector3>();
        obsticleDistances = new List<float>();

        // Shperecasts
        // Target directional sphere cast
        goDetectedInDirectionToTargetSphere = PhysicsSpherecast(transform.position, directionToTarget, rayLenght, sphereCastRadius * directionalSphereCastRadiusMultiplier, out directionToTargetShpereCastHit, obsticlesLayer);
        // avrage directional shpere cast
        goDetectedInAvrageDirectionToTargetSphere = PhysicsSpherecast(transform.position, CalculatePassThroughPosition(), rayLenght/3, sphereCastRadius * directionalSphereCastRadiusMultiplier, out avrageDirectionToTargetSphereHit, obsticlesLayer);
        // central forward sphere cast
        goDetectedCenterSphere = PhysicsSpherecast(transform.position, transform.forward, rayLenght, sphereCastRadius, out centerShpereCastHit, obsticlesLayer);

        // Raycasts
        // Forward right ray cast
        goDetectedRight = PhysicsRaycast(transform.position, 1, transform.right, - (transform.forward * rayBackwardsOffsetMultiplier), rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        // Forward left ray cast
        goDetectedLeft = PhysicsRaycast(transform.position, -1, transform.right, (transform.forward * rayBackwardsOffsetMultiplier), rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        // Forward top ray cast
        goDetectedTop = PhysicsRaycast(transform.position, 1, transform.up, - (transform.forward * rayBackwardsOffsetMultiplier), rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
        // Forward bottom ray cast
        goDetectedBottom = PhysicsRaycast(transform.position, -1, transform.up, (transform.forward * rayBackwardsOffsetMultiplier), rayOffset, transform.forward, rayLenght * (1 + ((rayBackwardsOffsetMultiplier / rayLenght) / 2)), obsticlePositions, out obsticlePositions, obsticlesLayer);
    }

    /// <summary>
    /// Contails all of the contidions and functions for steering the enemy
    /// </summary>
    void TurningConditions() {
        
        // in case of dead end
        if ((goDetectedRight && goDetectedLeft && goDetectedTop && goDetectedBottom) && goDetectedCenterSphere) {
            AvoidingDeadEnd(); // perform avoidance
        }
        // in case of opposite forward rays are blocked (left and right) or (top and bottom)
        else if ((((goDetectedRight && goDetectedLeft) && !(goDetectedTop || goDetectedBottom)) && goDetectedCenterSphere) ^ (((goDetectedTop && goDetectedBottom) && !(goDetectedRight || goDetectedLeft)) && goDetectedCenterSphere)) {
            AvoidingDeadEnd(); // perform avoidance
        }
        else {
            // changing sleed depending on ray data
            movementStatistics.currentSpeed = ChangeMovementSpeed(false); 
            // restarting the reverse ridection of the enemy
            if (blockedPathBackDir != new Vector3()) {
                blockedPathBackDir = new Vector3();
            }
            
            // performing turning action in the opposite direction when a obsticle is detected and the center sphere cast is blocked
            if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && goDetectedCenterSphere) 
            {
                Turning(transform.position - averageObsticlePositions); //Turn in the oposite direction of the obsicles
            }
            // turning when the directional sphere casts are not blocked
            else if ((goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom) && (!goDetectedInDirectionToTargetSphere && !goDetectedInAvrageDirectionToTargetSphere)) {
                Turning(objectToFollow.transform.position); //Turn towards the target
            }
            else if (goDetectedRight || goDetectedLeft || goDetectedTop || goDetectedBottom){} // pass close to the obsticle
            else
            {
                Turning(objectToFollow.transform.position); //Turn towards the target
            }
        }
    }

    /// <summary>
    /// Rotating the enemy in the backwards direction when a path in front is not found
    /// </summary>
    void AvoidingDeadEnd() {
        movementStatistics.currentSpeed = ChangeMovementSpeed(true); // stopping enemy in place
        if (blockedPathBackDir == new Vector3()) {
            blockedPathBackDir = -this.transform.forward; // setting back direction
        }
        Turning(transform.position + blockedPathBackDir); // perform turning in the backwards direction
    }

    /// <summary>
    /// dinamically reduces moving speed when obsticles are derected and increses the speed when the path is clear
    /// </summary>
    /// <param name="forceStop"> if the enemy needs to force stop in place </param>
    /// <returns> new speed of the caracter </returns>
    float ChangeMovementSpeed (bool forceStop) {
        float newSpeed;
        if (forceStop) { // fully stops the enemy
            newSpeed = 0;
            return newSpeed;
        }
        else{
            if (!goDetectedRight && !goDetectedLeft && !goDetectedTop && !goDetectedBottom) { // reduces speed when obsitcles are detected
                newSpeed = Mathf.Lerp(movementStatistics.currentSpeed, movementStatistics.maxSpeed, movementStatistics.changeSpeedMultiplier * Time.deltaTime);
            }
            else{ // increses speed when obsticles are not detected
                newSpeed = Mathf.Lerp(movementStatistics.currentSpeed, movementStatistics.minSpeed, movementStatistics.changeSpeedMultiplier * (Time.deltaTime * 10));
            }
            return newSpeed;
        }
    }

    /// <summary>
    /// moving the enemy forward
    /// </summary>
    void Movement () {
        float distanceToTarget = Vector3.Distance(transform.position, objectToFollow.transform.position);
        if (distanceToTarget > movementStatistics.stoppingDistance) {
            rb.velocity = (movementStatistics.currentSpeed * Time.deltaTime) * transform.forward;
        }else {
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Steering the enemy to the passed position
    /// </summary>
    /// <param name="targetPos"> The target position for the enemy to look at</param>
    void Turning(Vector3 targetPos) {
        Vector3 position = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(position);
        if (obsticleDistances.Count > 0) {
            movementStatistics.currentRotationSpeed = (1 - (obsticleDistances.Min() / rayLenght)) * movementStatistics.rotationSpeed; //rotation speed calculated from the distance to closest obsticle
        }else {
            movementStatistics.currentRotationSpeed = movementStatistics.rotationSpeed;
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, movementStatistics.rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the pass through position (avrage position between the target direction and the forward direction)
    /// </summary>
    /// <returns> returns avrage direction </returns>
    Vector3 CalculatePassThroughPosition()
    {
        return (transform.forward.normalized + directionToTarget.normalized).normalized;
        
    }

    /// <summary>
    /// Performs a Ray cast depending on parameters
    /// </summary>
    /// <param name="centerPos"> current position of the enemy</param>
    /// <param name="possitiveOrNegativeMultiplier"> multiplier that is used for starting the ray from opposite side of the given starting position</param>
    /// <param name="rayOffsetPos"></param>
    /// <param name="rayOffsetBackwardsPos"></param>
    /// <param name="rayOffset"></param>
    /// <param name="direction"></param>
    /// <param name="rayCastLenght"></param>
    /// <param name="obsticlePos"></param>
    /// <param name="newObsticlePos"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    bool PhysicsRaycast (Vector3 centerPos, int possitiveOrNegativeMultiplier, Vector3 rayOffsetPos, Vector3 rayOffsetBackwardsPos, float rayOffset, Vector3 direction, float rayCastLenght, List<Vector3> obsticlePos, out List<Vector3> newObsticlePos, LayerMask layer) {
        RaycastHit hit;
        // centerPos + rayOffsetPos, direction, rayCastLenght, layer
        bool hasDetected = Physics.Raycast(centerPos + (possitiveOrNegativeMultiplier * ((rayOffsetPos + rayOffsetBackwardsPos) * rayOffset)), direction, out hit, rayCastLenght, layer)?true:false;
        List<Vector3> tempObsticlePos = obsticlePos;
        if (hasDetected) {
            tempObsticlePos.Add(possitiveOrNegativeMultiplier * (rayOffsetPos * rayOffset));
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
        if (showGizmos) {
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
    }
#endregion
}
