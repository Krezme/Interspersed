using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{

    [Header("Player Statistics")]
    public float walkSpeed; // Player walk speed
    public float runSpeed; // player run speed
    public float speedChangeRate = 10f; // acceleration and deceleration of the player


    // Current player stats (at the exact moment of the movement)
    private float speed;
    private float targetRotation = 0.0f;


    // References
    private CharacterController controller;
    private OnPlayerInput onPlayerInput;


    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        onPlayerInput = GetComponent<OnPlayerInput>();
    }

    
    void Update()
    {
        PlayerMovement();
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
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        }

        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * (speed * Time.deltaTime)); // moving the character
        
    }
}
