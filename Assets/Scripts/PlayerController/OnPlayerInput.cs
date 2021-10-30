using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class OnPlayerInput : MonoBehaviour
{
    [Header("Options")]
    public bool isSprintToggleable; // if the player wants toggleable sprinting or just hold to sprint
    [Range(0.1f, 5.0f)]
    public float mouseSensitivity = 1f;
    [Range(0.1f, 5.0f)]
    public float mouseSensitivityAim = 0.5f;
    
    

#region Values
    [HideInInspector]
    public Vector2 playerMovement; //player movement for the X and Z axis
    [HideInInspector]
    public bool isSprinting; // Sprinting state
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

    [HideInInspector]
    public float mouseSensitivityCurrent;

    private ThirdPersonPlayerController thirdPersonPlayerController;

    void Start () {
        thirdPersonPlayerController = GetComponent<ThirdPersonPlayerController>();
        mouseSensitivityCurrent = mouseSensitivity;
    }

#endregion
    /// <summary>
    /// Takes the player input and records it
    /// </summary>
    /// <param name="value">The input value of PlayerMovement input action map</param>
    public void OnPlayerMovement (InputValue value) {
        PlayerMovementInput(value.Get<Vector2>());
    }

    /// <summary>
    /// Takes the player jump input
    /// </summary>
    /// <param name="value">Sprint input value from Sprint input action map</param>
    public void OnSprint (InputValue value) {
        PlayerSprintInput(value.isPressed);
    }

    public void OnSlide (InputValue value) {
        PlayerSlideInput(value.isPressed);
    }

    /// <summary>
    /// Takes the player input and records it
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

    public void OnFire1(InputValue value) {
        PlayerFire1Input(value.isPressed);
    }
    public void OnFire2(InputValue value) {
        PlayerFire2Input(value.isPressed);
    }

    public void OnAbility1 (InputValue value)
    {
        PlayerAbility1Input(value.isPressed);
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

    private void PlayerSlideInput (bool slidingState) {
        if (thirdPersonPlayerController.isGrounded) {
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
    }

    private void PlayerFire1Input(bool fire1State) {
        onFire1 = fire1State;
    }

    private void PlayerFire2Input(bool fire2State) {
        onFire2 = fire2State;
    }

    private void PlayerAbility1Input(bool ability1State)
    {
        onAbility1 = ability1State;
    }
#endregion
}
