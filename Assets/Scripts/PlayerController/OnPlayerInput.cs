using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class OnPlayerInput : MonoBehaviour
{
#region Singleton

    /// <summary>
    /// This is a singleton to allow this script to be accessed by all other scripts
    /// </summary>
    public static OnPlayerInput instance;

    void Awake () {
        if (instance == null) {
            instance = this;
        }
        else{
            Debug.LogError("THERE ARE 2 OnPlayerInput SCRIPTS IN EXISTANCE");
        }
    }

#endregion 

    [Header("Options")]
    public static bool isWalkingToggleable = true; // if the player wants toggleable walking or just hold to sprint
    public static bool isSprintToggleable; // if the player wants toggleable sprinting or just hold to sprint
    [Range(0.1f, 5.0f)]
    public static float mouseSensitivity = 1f;
    [Range(0.1f, 5.0f)]
    public static float mouseSensitivityAim = 0.5f;

    public static bool invertXBool, invertYBool;

#region Values
    [HideInInspector]
    public Vector2 playerMovement; //player movement for the X and Z axis
    [HideInInspector]
    public bool isSprinting; // Sprinting state
    [HideInInspector]
    public bool isWalking;
    [HideInInspector]
    public bool isSliding;
    [HideInInspector]
    public bool jumped; // if the player jumped
    [HideInInspector]
    public Vector2 looking; // the position of the player camera
    [HideInInspector]
    public bool onFire1;
    [HideInInspector]
    public bool onFire2;
    [HideInInspector]
    public bool onAbility1;
    public bool onArmMode;
    [HideInInspector]
    public bool isESC;

    [HideInInspector]
    public float mouseSensitivityCurrent;

    void Start () {
        mouseSensitivityCurrent = mouseSensitivity;
    }

#endregion
    /// <summary>
    /// Takes the player move input and records it
    /// </summary>
    /// <param name="value">The input value of PlayerMovement input action map</param>
    public void OnPlayerMovement (InputValue value) {
        PlayerMovementInput(value.Get<Vector2>());
    }

    /// <summary>
    /// Takes the player Sprint input
    /// </summary>
    /// <param name="value">Sprint input value from Sprint input action map</param>
    public void OnSprint (InputValue value) {
        PlayerSprintInput(value.isPressed);
    }

    /// <summary>
    /// Takes the player walk input
    /// </summary>
    /// <param name="value">Walk input value</param>
    public void OnWalk (InputValue value) {
        PlayerWalkInput(value.isPressed);
    }

    /// <summary>
    /// Takes the player Slide input
    /// </summary>
    /// <param name="value">Slide input value</param>
    public void OnSlide (InputValue value) {
        PlayerSlideInput(value.isPressed);
    }

    /// <summary>
    /// Takes the player jump input and records it
    /// </summary>
    /// <param name="value">Jump input value</param>
    public void OnJump(InputValue value){
        PlayerJumpInput(value.isPressed);
    }

    /// <summary>
    /// Takes the player camera input from the mouse movement and records it
    /// </summary>
    /// <param name="value">Mouse value</param>
    public void OnLook(InputValue value) {
        PlayerLookInput(value.Get<Vector2>());
    }

    /// <summary>
    /// Takes the player left mouse click input
    /// </summary>
    /// <param name="value">Fire 1 input value</param>
    public void OnFire1(InputValue value) {
        PlayerFire1Input(value.isPressed);
    }
    
    /// <summary>
    /// Takes the player right mouse click input
    /// </summary>
    /// <param name="value">Fire 2 input value</param>
    public void OnFire2(InputValue value) {
        PlayerFire2Input(value.isPressed);
    }

    /// <summary>
    /// Takes the player Ability1 input
    /// </summary>
    /// <param name="value">Ability1 input value</param>
    public void OnAbility1 (InputValue value)
    {
        PlayerAbility1Input(value.isPressed);
    }

    /// <summary>
    /// Takes the player arm 1 input choise 
    /// </summary>
    /// <param name="value">Arm 1 input value</param>
    public void OnArm1 (InputValue value) {
        if (value.isPressed && PlayerAbilitiesController.instance.selectedAbility != 0) { 
            PlayerArmInput(0);
        }
    }

    /// <summary>
    /// Takes the player arm 2 input choise
    /// </summary>
    /// <param name="value">Arm 2 input value</param>
    public void OnArm2 (InputValue value) {
        if (value.isPressed && PlayerAbilitiesController.instance.selectedAbility != 1) {
            PlayerArmInput(1);
        }
    }

    public void OnArmMode(InputValue value) {
        ArmModeInput(value.isPressed);
    }

    public void OnESC(InputValue value) {
        ESCInput(value.isPressed);
    }

#region Recording Functions
    /// <summary>
    /// Setting the playerMovement Vector2 to the input
    /// </summary>
    /// <param name="newPlayerDirection">The new player direction</param>
    private void PlayerMovementInput (Vector2 newPlayerDirection) {
        playerMovement = newPlayerDirection;
    }

    /// <summary>
    /// Setting the isSprining to the sprinting state
    /// </summary>
    /// <param name="sprintState">The sprinting state</param>
    private void PlayerSprintInput(bool sprintState) {
        if (!isSprintToggleable) {
            isSprinting = sprintState;
            return;
        }
        if (sprintState) {
            isSprinting = !isSprinting;
        }
    }

    /// <summary>
    /// Setting the isWalking to the walking state and if it is toggleable it reverses the state
    /// </summary>
    /// <param name="sprintState">The walking state</param>
    private void PlayerWalkInput(bool walkingState) {
        if (!isWalkingToggleable) {
            isWalking = walkingState;
            return;
        }
        if (walkingState) {
            isWalking = !isWalking;
        }
    }

    /// <summary>
    /// Records the slide input if the player is grunded
    /// </summary>
    /// <param name="slidingState"></param>
    private void PlayerSlideInput (bool slidingState) {
        if (ThirdPersonPlayerController.instance.isGrounded) {
            isSliding = slidingState;
        }
    }

    /// <summary>
    /// Setting the playerJumpInput to the jump state of the player
    /// </summary>
    /// <param name="jumpState">if the player has jumped</param>
    private void PlayerJumpInput(bool jumpState) {
        jumped = jumpState;
    }

    /// <summary>
    /// Setting the looking var to the provided value and applying sensitivity
    /// </summary>
    /// <param name="lookInput">The value to set the looking var</param>
    private void PlayerLookInput(Vector2 lookInput) {

        looking = lookInput * mouseSensitivityCurrent;

        if (invertXBool)
        {
            looking.x = -looking.x;
        }
        if (invertYBool)
        {
            looking.y = -looking.y;
        }
        
    }
    
    /// <summary>
    /// Setting the Fire 1 state
    /// </summary>
    /// <param name="fire1State">Fire 1 state</param>
    private void PlayerFire1Input(bool fire1State) {
        onFire1 = fire1State;
    }

    /// <summary>
    /// Setting the Fire 2 state
    /// </summary>
    /// <param name="fire2State">Fire 2 state</param>
    private void PlayerFire2Input(bool fire2State) {
        onFire2 = fire2State;
    }

    /// <summary>
    /// Setting the Ability 1 state
    /// </summary>
    /// <param name="ability1State">Ability 1 state</param>
    private void PlayerAbility1Input(bool ability1State)
    {
        onAbility1 = ability1State;
    }

    /// <summary>
    /// Switching between arms 
    /// </summary>
    /// <param name="armIndex"></param>
    private void PlayerArmInput(int armIndex) {
        PlayerAbilitiesController.instance.selectedAbility = armIndex;
        PlayerAbilitiesController.instance.ChangeArm();
    }

    private void ArmModeInput(bool modeState) {
        onArmMode = modeState;
    }

    private void ESCInput(bool escState) {
        isESC = escState;
    }

#endregion
}
