using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{
#region Public Player Vars
    [Header("Player Statistics")]
    public float walkSpeed; // Player walk speed
    public float runSpeed; // player run speed
    public float jumpHeight; // the desired player jump height
    [Range(0.0f, 0.5f)]
    public float rotationSmoothness = 0.05f;
    [Space(10)]
    public float speedChangeRate = 10f; // acceleration and deceleration of the player
    public float slideSpeedChangeRate = 5f; // the deceleration of the player when sliding
    public float inAirSpeedChangeRate = 2f;
    /* public float directionChangeRate = 10f;
    public float slideDirectionChangeRate = 5f;
    public float inAirDirectionChangeRate = 2f; */

    [Space(10)]
    public float jumpCooldown; // the cooldown between jumps
    public float fallCooldown;
#endregion

#region Phisics Vars
    [Header("Phisics")]
    public float gravity; // the vertival force that pulls the player character downwards
    public float constGravityWhileGrounded = -2f;
    public float terminalVelocity;

    [Space(10)]
    public float surfaceAngle;
    public float frictionStatic;
    public float frictionSlide;
    public float playerCharacterMass;
    public float slideSpeedToStopSliding; // When the slide speed falls below that number the slide will stop and the player will start walking

#endregion

#region Public Player Grounded Vars
    [Header("Grounded")]
    public bool isGrounded; // if the player is grounded
    public float groundedOffset = -0.14f; // small offset to make sure the player is prounded even on rough terrain
    public float groundedGizmoRadius = 0.5f; // the radius of the ground check sphere
    public float playerObjectCenterOffset; // variable that adjust the Center offset of a not animated character
    public LayerMask groundLayers; // the layer which the player can stand on and are counted as ground

#endregion

#region Public Cinemachine Vars
    [Header("Cinemachine")]
    public GameObject playerCameraRoot; // the camera root under the player object
    public float topClamp = 90f; // The top vertical camera rotation limit
    public float bottomClamp = -50f; // The bottom vertical camera rotation limit
    public bool isCameraLocked; // if the camera is locked 
#endregion

#region 
    [Header("Other References")]
    //public GameObject playerBody; // Needed for prototyping
#endregion

#region Private Cinemachine Vars
    private float camTargetYaw; // current target for the camera horizontal rotation
    private float camTargetPitch; // current target fot the camera vertical rotation
    private float camMoveThreshold; // threshold for minimum mouse movement required to move the camera
#endregion

    // Current player stats (at the exact moment of the movement)
    private float speed;
    private float originalHeight;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    private float jumpCooldownCurrent;
    private float fallCooldownCurrent;    
    private Vector3 groundedSpherePosition;
    private float initialRequiredForceToMove;
    private float secondaryRequiredForceToMove;
    private float currentRequiredForceToMoveBefore = 0.0f;
    private float currentRequiredForceToMoveAfter = 0.0f;
    private Vector3 inputDirection = Vector3.zero;
    private float animationBlend;

    // References
    private CharacterController controller;
    private OnPlayerInput onPlayerInput;
    private GameObject mainCamera;
    private Animator animator;
    
    void Awake() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        TryGetComponent(out animator);
        controller = GetComponent<CharacterController>();
        onPlayerInput = GetComponent<OnPlayerInput>();
        originalHeight = controller.height;

        initialRequiredForceToMove = frictionStatic * (playerCharacterMass * -gravity);
        secondaryRequiredForceToMove = frictionSlide * (playerCharacterMass * -gravity);
    }
    
    void Update()
    {
        GroundCheckSphere();
        GroundedCheck();
        PlayerJumpAndGravity();
        PlayerMovement();
        //PlayerSliding();
        GetSurfaceAngleBelowPlayer();
    }

    void LateUpdate() {
        CameraOrbit();
    }

    /// <summary>
    /// The PlayerMovement controlls the horizontal movement of the player (WASD and the arrow keys)
    /// </summary>
    private void PlayerMovement() {

        float targetSpeed = onPlayerInput.isSprinting ? runSpeed : walkSpeed;  // setting the target speed (walking or running)
        
        float currentSpeedChangeRate = isGrounded ? speedChangeRate : inAirSpeedChangeRate;

        targetSpeed = onPlayerInput.playerMovement == Vector2.zero ? 0f : targetSpeed; // if the player is not moving set it to 0 otherwise remain

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        Vector3 currentInputDirection = inputDirection;

        float speedOffset = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) { // if the player is accelerating
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * currentSpeedChangeRate); // Smooting

            speed = Mathf.Round(speed * 1000f) / 1000f; // rounding to 3 decimal places
        }
        else {
            speed = targetSpeed;
        }
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        Vector3 targetInputDirection = new Vector3(onPlayerInput.playerMovement.x, 0.0f, onPlayerInput.playerMovement.y).normalized; // normalized direction

        if (onPlayerInput.playerMovement != Vector2.zero) { // if the player is not moving
            targetRotation = Mathf.Atan2(targetInputDirection.x, targetInputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothness); // Smoothing the rotation of the player character

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); // appying the rotation
        }

        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        if (onPlayerInput.isSliding && onPlayerInput.isSprinting) { // Overwriting the speed of the player with the slidning speed
            SlidingPhysicsCalculation();
        }
        else{
            currentRequiredForceToMoveBefore = secondaryRequiredForceToMove;
        }

        //If the character is on a slope increase the downwards velocity to make up for the slope and reduce juddering 
        if (onPlayerInput.jumped) {}
        else if (surfaceAngle > 0.0f && onPlayerInput.isSprinting && isGrounded && !onPlayerInput.isSliding) {
            verticalVelocity = (Vector3.down.y * Time.deltaTime * surfaceAngle * 1500f) * 2;
        }
        else if (surfaceAngle > 0.0f && !onPlayerInput.isSprinting && isGrounded && !onPlayerInput.isSliding) {
            verticalVelocity = (Vector3.down.y * Time.deltaTime * surfaceAngle * 1500f);
        }

        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime); // Applying the movement

        animator.SetFloat("Speed", animationBlend);
    }

    /// <summary>
    /// Handles the player jump and gravity
    /// </summary>
    void PlayerJumpAndGravity(){
        if (isGrounded) {
            fallCooldownCurrent = fallCooldown;

            // gravity reduction while grounded
            if (verticalVelocity <= 0.0f) {
                verticalVelocity = constGravityWhileGrounded;
            }

            // Calculating the vertical velocuty when the input is pressed and the cooldown is over
            if (onPlayerInput.jumped && jumpCooldownCurrent <= 0.0f) {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);   // H * -2 * G to calculate how much velocity is required to reach the desired height
            }
            
            // cooling down the jump
            if (jumpCooldownCurrent >= 0.0f) {
                jumpCooldownCurrent -= Time.deltaTime;
            }
        }
        else{
            jumpCooldownCurrent = jumpCooldown;

            // cooling down the fall
            if (fallCooldownCurrent >= 0.0f) {
                fallCooldownCurrent -= Time.deltaTime;
            }

            onPlayerInput.jumped = false; // when not grounded stop jumping
        }

        // increases the fall of the player until it reaches terminal velovity
        if (verticalVelocity < terminalVelocity) {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    /// <summary>
    /// This is a temporary finction for prototyping
    /// </summary>
    /* void PlayerSliding () {
        if (onPlayerInput.isSliding && playerBody.transform.rotation != Quaternion.Euler(90f,0f,0f)) {
            playerBody.transform.rotation = Quaternion.Euler(90f,0f,0f);
            controller.height = controller.height/2;
        }
        else if ((!onPlayerInput.isSliding || !onPlayerInput.isSprinting) && playerBody.transform.rotation != Quaternion.Euler(0,0,0)){
            playerBody.transform.rotation = Quaternion.Euler(0,0,0);
            controller.height = originalHeight;
        }
    } */

    /// <summary>
    /// Calculation the sliding Speed of the player when sliding
    /// </summary>
    void SlidingPhysicsCalculation () {
        
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        speed = Mathf.Lerp(currentHorizontalSpeed, 0f, Mathf.Lerp(currentRequiredForceToMoveBefore, initialRequiredForceToMove, Time.deltaTime * slideSpeedChangeRate) * 0.01f * Time.deltaTime);

        if (currentHorizontalSpeed <= slideSpeedToStopSliding) { // When the speed falls below "slideSpeedToStopSliding" the sliding will stop
            onPlayerInput.isSliding = false;
        }
    }

    /// <summary>
    /// Calculating the rotation of the camera as well as liting it
    /// </summary>
    void CameraOrbit () {
        if (onPlayerInput.looking.sqrMagnitude >= camMoveThreshold && !isCameraLocked) {
            camTargetYaw += onPlayerInput.looking.x * Time.deltaTime;
            camTargetPitch += onPlayerInput.looking.y * Time.deltaTime;
        }

        camTargetYaw = ClampAngle(camTargetYaw, float.MinValue, float.MaxValue);
        camTargetPitch = ClampAngle(camTargetPitch, bottomClamp, topClamp);

        playerCameraRoot.transform.rotation = Quaternion.Euler(camTargetPitch, camTargetYaw, 0.0f);
    }

    /// <summary>
    /// Setting the ground check sphere position
    /// </summary>
    void GroundCheckSphere() {
        groundedSpherePosition = new Vector3(transform.position.x, (transform.position.y - playerObjectCenterOffset) - groundedOffset, transform.position.z);
    }

    /// <summary>
    /// Checking if the player is toching the ground
    /// </summary>
    private void GroundedCheck()
	{
		// Checking if player is grounded
		isGrounded = Physics.CheckSphere(groundedSpherePosition, groundedGizmoRadius, groundLayers, QueryTriggerInteraction.Ignore);

		// update animator if using character
		/* if (_hasAnimator)
		{
			_animator.SetBool(_animIDGrounded, Grounded);
		} */
	}

    /// <summary>
    /// Limits the camera movement
    /// </summary>
    /// <param name="angle">The angle to limit</param>
    /// <param name="min">The min angle to limit with</param>
    /// <param name="max">the max angle to limit with</param>
    /// <returns>The new produced angle (float)</returns>
    private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f) angle += 360f;
		if (angle > 360f) angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}

    private void GetSurfaceAngleBelowPlayer() {
        RaycastHit hit;
        if (isGrounded && Physics.Raycast(transform.position, Vector3.down, out hit, 2f, groundLayers)) {
            surfaceAngle = Vector3.Angle(hit.normal, Vector3.up);
        }
    }

    private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.3f); // Green colour
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.3f); // Red colour

            //Colouring the sphere
			if (isGrounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;
			
            //Drawing the Gizmo at the feet of the player character
			Gizmos.DrawSphere(groundedSpherePosition, groundedGizmoRadius); 
		}
}
