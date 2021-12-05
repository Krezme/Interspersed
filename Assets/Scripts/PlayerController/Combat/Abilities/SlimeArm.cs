using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeArm : PlayerAbility
{
    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    public GameObject changeToArm;

    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwforce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;
    
    Rigidbody grabbedRB;

    public override void MorthToTarget()
    {
        base.MorthToTarget();
        changeToArm.SetActive(!changeToArm.activeSelf);
    }

    public override void AimingAbility()
    {
        base.AimingAbility();

    }
    public override void AditionalAbilities()
    {
        base.AditionalAbilities();
        PickUpAbility();
        LaunchGrabbed();
    }

    private void PickUpAbility() {
        if (OnPlayerInput.instance.onFire2) {
            if (OnPlayerInput.instance.onAbility1 && cooldownTimer == 0) {
                print("Ability1Cast");
                if (grabbedRB) //if there is a grabbed rigidbody when we press the key, drop it.
                {
                    grabbedRB.isKinematic = false;
                    grabbedRB = null;
                    PlayerAbilitiesController.instance.isAbilityActive = false;
                }
                else //if there is not a grabbed rigid body, raycast for one and 'grab it' (setting grabbedrb to it)
                {
                    RaycastHit hit;
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    if (Physics.Raycast(ray, out hit, maxGrabDistance))
                    {
                        
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        Debug.Log(grabbedRB);
                        if (grabbedRB.gameObject.transform.root.TryGetComponent<RagdollController>(out RagdollController grabbedRagdoll)) {
                            grabbedRagdoll.RagdollOn();
                            Debug.Log("Works?");
                        }
                        if (Physics.Raycast(ray, out hit, maxGrabDistance))
                        {
                            grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                            Debug.Log(grabbedRB);
                            if (grabbedRB)
                            {
                                grabbedRB.isKinematic = true;
                                PlayerAbilitiesController.instance.isAbilityActive = true;
                            }
                        }
                    }
                }

                //start the cooldown 
                cooldownTimer = cooldownMaxTime;
                StartCoroutine(Ability1Cooldown());
            }
        }
        else {
            if (PlayerAbilitiesController.instance.isAbilityActive) {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;
            }
        }
    }

    /// <summary>
    /// It launches the currently grabbed item with the slime arm ability
    /// </summary>
    private void LaunchGrabbed() {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(objectHolder.transform.position);
            
            if (OnPlayerInput.instance.onFire1)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwforce, ForceMode.VelocityChange);
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;
            }
        }
    }

    private IEnumerator Ability1Cooldown()
    {
        //after the cooldown time, set the cooldown timer to 0 allowing the ability to be cast again
        yield return new WaitForSeconds(cooldownMaxTime);
        cooldownTimer = 0;
    }
}
