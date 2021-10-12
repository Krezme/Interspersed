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
    public float speedChangeRate = 10f; // acceleration and deceleration of the player

    [Space(10)]
    public float jumpCooldown; // the cooldown between jumps
    public float fallCooldown;
#endregion

#region Phisics Vars
    [Header("Phisics")]
    public float gravity; // the vertival force that pulls the player character downwards
    public float constGravityWhileGrounded = -2f;
    public float terminalVelocity;
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
    public GameObject playerCameraRoot;
    public float sensitivityMultiplier = 1.0f;
    public float topClamp = 90f;
    public float bottomClamp = -50f;
    public bool isCameraLocked;
#endregion

#region Private Cinemachine Vars
    private float camTargetYaw;
    private float camTargetPitch;
    private float camMoveThreshold;
#endregion

    // Current player stats (at the exact moment of the movement)
    private float speed;
    private float targetRotation = 0.0f;
    private float verticalVelocity;
    private float jumpCooldownCurrent;
    private float fallCooldownCurrent;    
    private Vector3 groundedSpherePosition;

    // References
    private CharacterController controller;
    private OnPlayerInput onPlayerInput;
    private GameObject mainCamera;
    
    void Awake() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        onPlayerInput = GetComponent<OnPlayerInput>();
    }
    
    void Update()
    {
        GroundCheckSphere();
        GroundedCheck();
        PlayerJumpAndGravity();
        PlayerMovement();
    }

    void LateUpdate() {
        CameraOrbit();
    }

    /// <summary>
    /// The PlayerMovement controlls the horizontal movement of the player (WASD and the arrow keys)
    /// </summary>
    private void PlayerMovement() {

        float targetSpeed = onPlayerInput.isSprinting ? runSpeed : walkSpeed;  // setting the target speed (walking or running)

        targetSpeed = onPlayerInput.playerMovement == Vector2.zero ? 0f : targetSpeed; // if the player is not moving set it to 0 otherwise remain

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) { // if the player is accelerating
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate); // Smooting

            speed = Mathf.Round(speed * 1000f) / 1000f; // rounding to 3 decimal places
        }
        else {
            speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(onPlayerInput.playerMovement.x, 0.0f, onPlayerInput.playerMovement.y).normalized; // normalized direction

        if (onPlayerInput.playerMovement != Vector2.zero) { // if the player is not moving
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        }

        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime); // Applying the movement
        
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
    /// Calculating the rotation of the camera as well as clamping it
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

    private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f) angle += 360f;
		if (angle > 360f) angle -= 360f;
		return Mathf.Clamp(angle, min, max);
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
