using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonPlayerController : MonoBehaviour
{
    #region Singleton

    public static ThirdPersonPlayerController instance;

    CristalArm hold; //Rhys - Creating reference to 'CristalArm' script used to control 'IsHold' bool

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("THERE ARE 2 ThirdPersonPlayerController SCRIPTS IN EXISTANCE");
        }

        hold = GameObject.FindGameObjectWithTag("PlayerAbility").GetComponent<CristalArm>(); //Rhys - Used to control 'IsHold' variable present within 'CristalArm' script - Melee attack causes IsHold to incorrectly

    }

#endregion

    #region Public Player Vars
    [Header("Player Statistics")]
    public float walkSpeed; // Player walk speed
    public float jogSpeed; // Player jogging speed
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

    #region Knockback Vars
    Vector3 launchKnockbackDir;
    float launchKnockbackStrength;
    float launchKnockbackHeight;
    bool hasBeenLaunched;

    Vector3 additionalKnockbackTargetDir;
    float additionalKnockbackHeight;

    float stopStrength; // time incremental stopping multiplier
    public float stopStrengthMultiplier = 0.1f;
   
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
    public GameObject heightCheckFront;
    public GameObject heightCheckBack;
    public Animator animator;
    public RigBuilder rigBuilder;
    #endregion

    #region Private Cinemachine Vars
    private float camTargetYaw; // current target for the camera horizontal rotation
    private float camTargetPitch; // current target fot the camera vertical rotation
    private float camMoveThreshold; // threshold for minimum mouse movement required to move the camera
    #endregion

    // Current player stats (at the exact moment of the movement)
    private float speed;
    private float speedOnSlope;
    private float originalHeight;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    [HideInInspector]
    public float verticalVelocity; //public to be available for checkpoint teleportation
    private float jumpCooldownCurrent;
    private float fallCooldownCurrent;    
    private Vector3 groundedSpherePosition;
    private float currentFricton;
    private Vector3 inputDirection = Vector3.zero;
    private float animationBlend;
    private float slidingTime;
    private Vector3 moveDirection;
    private bool rotationChangedForThisFrame;
    private Vector3 surfaceHitPointNormal;
    private bool onOverLimitSlope;
    private float heightCheckDistanceFront;
    private float heightCheckDistanceFrontLast = 1f;
    private float heightCheckDistanceBack;
    private bool isAiming;
    private bool hasJumped;
    private float delayedJumpTime = 0.5f;
    private float delayedJumpCurrentTime = 0.5f;

    public RandomAudioPlayer PlayerDamaged; //Rhys - Player takes damage 
    public RandomAudioPlayer PlayerAttack; //Rhys - Player melee attack

    // References
    private CharacterController controller;
    private GameObject mainCamera;

    void Start()
    {
        //TryGetComponent(out animator);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
            
       

        //initialRequiredForceToMove = frictionStatic * (playerCharacterMass * -gravity);
        //secondaryRequiredForceToMove = frictionSlide * (playerCharacterMass * -gravity);
    }
    
    

    void Update()
    {
        GroundCheckSphere();
        GroundedCheck();
        CheckingIfGoingUpOrDownASlope();
        GetSurfaceAngleBelowPlayer();
        PlayerJumpAndGravity();
        Knockback();
        PlayerMovement();
        //PlayerSliding();
        Attacking();
    }

    void LateUpdate() {
        CameraOrbit();
    }

    /// <summary>
    /// The PlayerMovement controlls the horizontal movement of the player (WASD and the arrow keys)
    /// </summary>
    private void PlayerMovement() {
        float targetSpeed = OnPlayerInput.instance.isWalking ? walkSpeed : OnPlayerInput.instance.isSprinting ? runSpeed : jogSpeed;
        
        float currentSpeedChangeRate = isGrounded ? speedChangeRate : inAirSpeedChangeRate;

        targetSpeed = OnPlayerInput.instance.playerMovement == Vector2.zero ? 0f : targetSpeed; // if the player is not moving set it to 0 otherwise remain
        
        //float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        Vector3 currentInputDirection = inputDirection;

        float speedOffset = 0.1f;

        if (speed < targetSpeed - speedOffset || speed > targetSpeed + speedOffset) { // if the player is accelerating
            speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * currentSpeedChangeRate); // Smooting

            speed = Mathf.Round(speed * 1000f) / 1000f; // rounding to 3 decimal places
        }
        else {
            speed = targetSpeed;
        }
        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);

        Vector3 targetInputDirection = new Vector3(OnPlayerInput.instance.playerMovement.x, 0.0f, OnPlayerInput.instance.playerMovement.y).normalized; // normalized direction

        if (OnPlayerInput.instance.playerMovement != Vector2.zero) { // if the player is not moving
            targetRotation = Mathf.Atan2(targetInputDirection.x, targetInputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothness); // Smoothing the rotation of the player character

            if (!isAiming) {
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); // appying the rotation
            }
            rotationChangedForThisFrame = true;
        }

        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        if (OnPlayerInput.instance.isSliding && OnPlayerInput.instance.isSprinting) { // Overwriting the speed of the player with the slidning speed
            SlidingPhysicsCalculation();
            animator.SetBool("Slide", true);
        }
        else{
            //currentRequiredForceToMoveBefore = secondaryRequiredForceToMove;
            currentFricton = frictionSlide;
            heightCheckDistanceFrontLast = 1f;
            slidingTime = 0f;
            animator.SetBool("Slide", false);
        }
        
        //If the character is on a slope increase the downwards velocity to make up for the slope and reduce juddering
        StickingToSlopes();

        moveDirection = ((targetDirection.normalized * (speed * Time.deltaTime)) + additionalKnockbackTargetDir ) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;

        SlidingOnOverTheLimitSurface();

        controller.Move(moveDirection); // Applying the movement
        
        //Animating between animations while aiming and while not
        if (!isAiming) {  
            animator.SetFloat("Speed", animationBlend);
            animator.SetFloat("SideSpeed", 0f);
        }
        else { 
            animator.SetFloat("Speed", targetInputDirection.z * targetSpeed);
            animator.SetFloat("SideSpeed", targetInputDirection.x * targetSpeed);
        }
        
        // Keep this line at the bottom as it checks if the player is used rotation or not 
        rotationChangedForThisFrame = false;
    }

    /// <summary>
    /// Handles the player jump and gravity
    /// </summary>
    void PlayerJumpAndGravity(){
        if (isGrounded || !hasJumped) {
            fallCooldownCurrent = fallCooldown;

            

            animator.SetBool("Jump", false);
			animator.SetBool("Falling", false);

            // gravity reduction while grounded
            if (verticalVelocity <= 0.0f && isGrounded) {
                verticalVelocity = constGravityWhileGrounded;
            }

            // Calculating the vertical velocuty when the input is pressed and the cooldown is over
            if ((OnPlayerInput.instance.jumped && jumpCooldownCurrent <= 0.0f && !onOverLimitSlope) || (OnPlayerInput.instance.jumped && jumpCooldownCurrent <= 0.0f && delayedJumpCurrentTime < delayedJumpTime && !hasJumped)) {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);   // H * -2 * G to calculate how much velocity is required to reach the desired height
                delayedJumpCurrentTime = delayedJumpTime;
                Debug.Log("Has jumped and is it over time ");
                hasJumped = true;
                animator.SetBool("Jump", true);
            }
            else {
                OnPlayerInput.instance.jumped = false;
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
            else {
                animator.SetBool("Falling", true);
            }

            OnPlayerInput.instance.jumped = false; // when not grounded stop jumping
        }

        // increases the fall of the player until it reaches terminal velovity
        if (verticalVelocity < terminalVelocity) {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    /// <summary>
    /// Applying knockback in a direction, depending on strength and vertical height strength 
    /// </summary>
    /// <param name="enemyPosition"> The enemy position (Used to calculate direction)</param>
    /// <param name="strength"> directional strength (preferably from 0 to 1) </param>
    /// <param name="height"> vertical push strength (preferably from 0 to 1) </param>
    public void ApplyKnockback(Vector3 enemyPosition, float strength, float height)
    {
        launchKnockbackDir = transform.position - enemyPosition;

        launchKnockbackStrength = strength;

        launchKnockbackHeight = height;

        hasBeenLaunched = true;
        
    }

    /// <summary>
    /// Pushing back the player in direction and height
    /// </summary>
    void Knockback()
    {
        stopStrength += Time.deltaTime; // time passed from last knockback push

        additionalKnockbackTargetDir = Vector3.Lerp(additionalKnockbackTargetDir, Vector3.zero, stopStrength * stopStrengthMultiplier);
    
        if (hasBeenLaunched) // applies force backwards
        {
            stopStrength = 0; // start timer
            additionalKnockbackTargetDir = ((launchKnockbackDir.normalized * launchKnockbackStrength) + (transform.up*launchKnockbackHeight)); // calculates the knockback direction and height

            //restarting variables for next knockback
            hasBeenLaunched = false;
            launchKnockbackDir = new Vector3();
            launchKnockbackStrength = 0;
            launchKnockbackHeight = 0;

        }

        Debug.Log("has grounded = " + isGrounded);
    }

    /// <summary>
    /// If the character is on a slope increase the downwards velocity to make up for the slope and reduce juddering
    /// </summary>
    void StickingToSlopes () {
        if (OnPlayerInput.instance.jumped || !isGrounded) {}
        else if (surfaceAngle > 0.0f && OnPlayerInput.instance.isSprinting && isGrounded) { // Applying a lot of downward force when the player is running 
            verticalVelocity = (Vector3.down.y * Time.deltaTime * surfaceAngle * 1500f) * 2;
        }
        // Applying a lot of downward force (half of the force applied when running) to the player when walking on a slope
        else if (surfaceAngle > 0.0f && !OnPlayerInput.instance.isSprinting && isGrounded && !OnPlayerInput.instance.isSliding) { 
            verticalVelocity = (Vector3.down.y * Time.deltaTime * surfaceAngle * 1500f);
        }
    }
    
    /// <summary>
    /// When the player character is on a too steep of an angle they slide down it and face the direction of going down the slope
    /// </summary>
    void SlidingOnOverTheLimitSurface() {
        if (onOverLimitSlope && isGrounded) {
            speedOnSlope = Mathf.Lerp(speedOnSlope, 30f, Time.deltaTime * slideSpeedChangeRate);
            moveDirection = new Vector3(surfaceHitPointNormal.x, -surfaceHitPointNormal.y, surfaceHitPointNormal.z) * speedOnSlope * Time.deltaTime;
            if (!rotationChangedForThisFrame) {
                targetRotation = Mathf.Atan2(surfaceHitPointNormal.x, surfaceHitPointNormal.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothness);

                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }
        }else {
            speedOnSlope = 0f;
        }
    }

    /// <summary>
    /// Calculation the sliding Speed of the player when sliding
    /// </summary>
    void SlidingPhysicsCalculation () {
        
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        slidingTime += Time.deltaTime;
        currentFricton = Mathf.Lerp(currentFricton, frictionStatic, Time.deltaTime * slideSpeedChangeRate);

        float targetSlideSpeed = 0f;
        float slowDownMultiplier = 1f;
        
        if ((heightCheckDistanceFront - heightCheckDistanceBack !> 0.1f && heightCheckDistanceBack - heightCheckDistanceFront !< 0.1f) || (heightCheckDistanceFront - heightCheckDistanceBack !< 0.1f && heightCheckDistanceBack - heightCheckDistanceFront !> 0.1f)) {
            if (heightCheckDistanceFront > 1f) {
                targetSlideSpeed = ((Mathf.Sin(surfaceAngle) * -gravity) * (heightCheckDistanceFront - 1f)) * 2; // Calculating the target speed when the player is sliding down
            }else if (heightCheckDistanceFront < 0.95f) {
                slowDownMultiplier = Mathf.Sqrt(((Mathf.Sin(surfaceAngle) * -gravity) * currentFricton) * (heightCheckDistanceFront + 1f)) * 2;
            }
        }
        
        if (heightCheckDistanceFrontLast < heightCheckDistanceFront - 0.25f || heightCheckDistanceFrontLast > heightCheckDistanceFront + 0.25f) {
            slidingTime /= 10f;
            heightCheckDistanceFrontLast = heightCheckDistanceFront;
        }

        speed = Mathf.Lerp(currentHorizontalSpeed, targetSlideSpeed, ((currentFricton * slidingTime * Time.deltaTime) * Mathf.Pow(Mathf.Cos(surfaceAngle), 2)) * slowDownMultiplier);

        if (currentHorizontalSpeed <= slideSpeedToStopSliding) { // When the speed falls below "slideSpeedToStopSliding" the sliding will stop
            OnPlayerInput.instance.isSliding = false;
        }
        
    }

    /// <summary>
    /// Checking if the player is going up or down a slope
    /// </summary>
    void CheckingIfGoingUpOrDownASlope(){
        RaycastHit hit;
        if (isGrounded && Physics.Raycast(heightCheckFront.transform.position, Vector3.down, out hit, 3f, groundLayers)) {
            heightCheckDistanceFront = hit.distance;
            //Debug.Log(heightCheckDistance);
        }
        else{
            heightCheckDistanceFront = 1f;
        }
        RaycastHit hit1;
        if (isGrounded && Physics.Raycast(heightCheckBack.transform.position, Vector3.down, out hit1, 3f, groundLayers)) {
            heightCheckDistanceBack = hit1.distance;
            //Debug.Log(heightCheckDistance);
        }
        else{
            heightCheckDistanceBack = 1f;
        }
    }

    /// <summary>
    /// Calculating the rotation of the camera as well as liting it
    /// </summary>
    void CameraOrbit () {
        if (OnPlayerInput.instance.looking.sqrMagnitude >= camMoveThreshold && !isCameraLocked) {
            camTargetYaw += OnPlayerInput.instance.looking.x * Time.deltaTime;
            camTargetPitch += OnPlayerInput.instance.looking.y * Time.deltaTime;
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
        if (isGrounded) {
            hasJumped = false;
        }
        animator.SetBool("Grounded", isGrounded);

        if (isGrounded && !hasJumped) {
            if (delayedJumpCurrentTime >= delayedJumpTime) {
                
            }
            delayedJumpCurrentTime = 0;
        }

        if (delayedJumpCurrentTime < delayedJumpTime) {
            delayedJumpCurrentTime += Time.deltaTime;
        }

        if (delayedJumpCurrentTime >= delayedJumpTime && !isGrounded) {
            hasJumped = true;
        }
		
	}

    /// <summary>
    /// Raycasting from the middle of the player (downwards) to calculate the current angle slope
    /// This also checks if the player is on a too steep of a slope
    /// </summary>
    private void GetSurfaceAngleBelowPlayer() {
        RaycastHit hit;
        if (isGrounded && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + playerObjectCenterOffset, transform.position.z), Vector3.down, out hit, 10f, groundLayers)) {
            surfaceHitPointNormal = hit.normal;
            surfaceAngle = Vector3.Angle(surfaceHitPointNormal, Vector3.up);
            if (controller.slopeLimit <= surfaceAngle) {
                onOverLimitSlope = true;
            }
            else {
                onOverLimitSlope = false;
            }
        }else {
            surfaceAngle = 0f;
        }
    }

    /// <summary>
    /// This is the function for attacking (Melee) 
    /// </summary>
     private void Attacking() {
        if (OnPlayerInput.instance.onFire1 && !OnPlayerInput.instance.onFire2 && !PlayerAbilitiesController.instance.isAbilityActive){ // Only attacks if the fire 1 button is pressed without any other
            OnPlayerInput.instance.onFire1 = false;
            animator.SetTrigger("Attack");
            PlayerAttack.PlayRandomClip();
            hold.IsHold = false; //Rhys - Set 'IsHold' to false on melee to prevent melee from disabling CrystalArm chargeup sound after attack
        }
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

    /// <summary>
    /// Toggles the aiming state to mainly stop the player character form rotating when aiming
    /// </summary>
    /// <param name="aimState"></param>
    public void SetRotateOnMove (bool aimState) {
        isAiming = aimState;
    }

    /* void AddEnergy(int gain)
    {
        currentEnergy += gain;

        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        energybar.SetEnergy(currentEnergy);

    }
    
    void Heal(int heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbar.SetHealth(currentHealth);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Energy")
        {
            AddEnergy(1);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(20);

            if (currentHealth <= 0)
            {
                Destroy(gameObject);

            }
        }
        if (other.gameObject.tag == "HealthPickup")
        {
            Heal(50);
        }
    } */
}
