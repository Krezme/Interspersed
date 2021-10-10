using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{
    public Vector2 playerMovement; //player movement for the X and Z axis

    /// <summary>
    /// Takes the player input and records it
    /// </summary>
    /// <param name="value">The input value of PlayerMovement input action map</param>
    public void OnPlayerMovement (InputValue value) {
        PlayerMovementInput(value.Get<Vector2>());
    }

    /// <summary>
    /// Setting the playerMovement Vector2 to the input
    /// </summary>
    /// <param name="newPlayerDirection">The new player direction</param>
    private void PlayerMovementInput (Vector2 newPlayerDirection) {
        playerMovement = newPlayerDirection;
    }

}
