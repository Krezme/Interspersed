using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAbility : MonoBehaviour
{
    //public PlayerAbilityStats stats;
    
#region References in Child
    [HideInInspector]
    public Vector3 centerScreenToWorldPosition;
    public CinemachineVirtualCamera aimVirtualCamera;
#endregion

    public float aimingRotationSpeed = 20f;
   
    public virtual void MorthToTarget () {
        //functionality for morthing to correct arm
    }

    public void Ability () {
        AimingAt();
        AimingHold();
        AbilitiesAssembly();
    }

    /// <summary>
    /// This is for Raycasting and finding where the player is aiming
    /// </summary>
    public void AimingAt() {
        centerScreenToWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, PlayerAbilitiesController.instance.aimColliderLayerMask)) {
            PlayerAbilitiesController.instance.rayBitch.position = hit.point;
            centerScreenToWorldPosition = hit.point;
        }
        else {
            PlayerAbilitiesController.instance.rayBitch.position = Camera.main.ScreenToWorldPoint(screenCenterPoint) + ray.direction * 999f;
            centerScreenToWorldPosition = Camera.main.ScreenToWorldPoint(screenCenterPoint) + ray.direction * 999f;
        }
    }

    /// <summary>
    /// Switching between aiming and not. Camera pos is changed, sensitivity, arm position.
    /// </summary>
    public void AimingHold() {
        if (OnPlayerInput.instance.onFire2)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.mouseSensitivityAim;
            ThirdPersonPlayerController.instance.SetRotateOnMove(true);
            for (int i = 0; ThirdPersonPlayerController.instance.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 1 (Animation Rigging) "pointing the arm forwards"
                ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight = Mathf.Lerp(ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight, 1f, Time.deltaTime * 10f);
            }
            
            //Calculates where the player needs to aim depending on where the raycast (in the AimingAt function) hit
            Vector3 worldAimTarget = centerScreenToWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //Rotates the player character so it is pointing in the aim direction
            ThirdPersonPlayerController.instance.transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * aimingRotationSpeed);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.mouseSensitivity;
            ThirdPersonPlayerController.instance.SetRotateOnMove(false);
            for (int i = 0; ThirdPersonPlayerController.instance.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 0 (Animation Rigging)
                ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight = Mathf.Lerp(ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight, 0f, Time.deltaTime * 10f);
            }
        }
    }

    /// <summary>
    /// Single function that can call all of the abilities
    /// </summary>
    private void AbilitiesAssembly() {
        if (OnPlayerInput.instance.onFire2)
        {
            if (OnPlayerInput.instance.onFire1)
            {
                AimingAbility();
            }
        }
        AditionalAbilities();
    }

    /// <summary>
    /// This is an overridable function for custom functionality
    /// 
    /// This function is intended for any functionality that requires the player to be aiming (with right click) and shooting (with left click)
    /// </summary>
    public virtual void AimingAbility() {}

    /// <summary>
    /// This function is for any abilities apart from the once that require aiming and shooting
    /// </summary>
    public virtual void AditionalAbilities() {}
}
