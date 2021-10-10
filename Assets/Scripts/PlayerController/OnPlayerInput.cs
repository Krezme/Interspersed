using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{
    public Vector2 playerMovement; //player movement for the X and Z axis
    public bool isSprinting; // Sprinting state

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
        isSprinting = sprintState;
    }
}
