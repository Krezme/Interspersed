using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class OnPlayerInput : MonoBehaviour
{
    public Vector2 playerMovement; //player movement for the X and Z axis
    public bool isSprintToggleable;
    public bool isSprinting; // Sprinting state
    public bool jumped; // if the player jumped
    public Vector2 looking; // the position of the player camera

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
    /// Setting the playerJumpInput to the jump state of the player
    /// </summary>
    /// <param name="jumpState">if the player has jumped</param>
    private void PlayerJumpInput(bool jumpState) {
        jumped = jumpState;
    }

    /// <summary>
    /// Setting the looking var to the provided value
    /// </summary>
    /// <param name="lookInput">The value to set the looking var</param>
    private void PlayerLookInput(Vector2 lookInput) {
        looking = lookInput;
    }
    
#endregion
}
