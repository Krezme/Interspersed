using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAbility : MonoBehaviour
{
    //public PlayerAbilityStats stats;
    
#region References in Child
    [HideInInspector]
    public Vector3 mouseWorldPosition;
    public CinemachineVirtualCamera aimVirtualCamera;
    #endregion
   
    
    
   


    public virtual void MorthToTarget () {
        //functionality for morthing to correct arm
    }

    public void Ability () {
        AimingAt();
        AimingHold();
        AbilitiesAssemply();
    }

    /// <summary>
    /// This is for Raycasting and finding where the player is aiming
    /// </summary>
    public void AimingAt() {
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, PlayerAbilitiesController.instance.aimColliderLayerMask))
        {
            PlayerAbilitiesController.instance.rayBitch.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        else {
            PlayerAbilitiesController.instance.rayBitch.position = ray.direction * 999f;
            mouseWorldPosition = ray.direction * 999f;
        }
    }

    public void AimingHold() {
        if (OnPlayerInput.instance.onFire2)
        {
            



            aimVirtualCamera.gameObject.SetActive(true);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.instance.mouseSensitivityAim;
            ThirdPersonPlayerController.instance.SetRotateOnMove(true);
            for (int i = 0; ThirdPersonPlayerController.instance.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 1 (Animation Rigging)
                ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight = Mathf.Lerp(ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight, 1f, Time.deltaTime * 10f);
            }
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            ThirdPersonPlayerController.instance.transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.instance.mouseSensitivity;
            ThirdPersonPlayerController.instance.SetRotateOnMove(false);
            for (int i = 0; ThirdPersonPlayerController.instance.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 0 (Animation Rigging)
                ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight = Mathf.Lerp(ThirdPersonPlayerController.instance.rigBuilder.layers[i].rig.weight, 0f, Time.deltaTime * 10f);
            }
        }
    }

    private void AbilitiesAssemply() {
        if (OnPlayerInput.instance.onFire2)
        {
            if (OnPlayerInput.instance.onFire1)
            {
                AimingAbility();
            }
        }
        AditionalAbilities();
    }

    public virtual void AimingAbility() {}
    public virtual void AditionalAbilities() {}
}
