using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{
    public Vector2 playerMovement; //player movement for the X and Z axis

    public void OnPlayerMovement (InputValue value) {
        //PlayerMovementInput(value.Get<Vector2>());
        Debug.Log(value.Get<Vector2>());
    }

    private void PlayerMovementInput (Vector2 newPlayerDirection) {
        playerMovement = newPlayerDirection;
    }

}
