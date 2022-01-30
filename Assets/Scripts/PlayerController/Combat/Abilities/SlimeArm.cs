using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeArm : PlayerAbility
{
    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    public GameObject changeToArm; 

    public RandomAudioPlayer Pickup; //Rhys - RandomAudioPlayer for when the slime arm picks something up

    public RandomAudioPlayer Drop; //Rhys - RandomAudioPlayer for when the slime arm drops something

    public RandomAudioPlayer Throw; //Rhys - RandomAudioPlayer for when the slime arm throws something


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
            if (OnPlayerInput.instance.onFire1 && cooldownTimer == 0) {
                if (!grabbedRB) {
                    RaycastHit hit;
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                    if (Physics.Raycast(ray, out hit, maxGrabDistance))
                    {                        
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        Debug.Log(grabbedRB);
                        if (grabbedRB.gameObject.transform.root.TryGetComponent<RagdollController>(out RagdollController grabbedRagdoll)) {
                            grabbedRagdoll.RagdollOn();
                        }
                        if (Physics.Raycast(ray, out hit, maxGrabDistance))
                        {
                            grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                            Debug.Log(grabbedRB);
                            if (grabbedRB)
                            {
                                Pickup.PlayRandomClip(); //Rhys - Plays sound only once an object has been successfully been pickup up by the slime arm 
                                grabbedRB.isKinematic = true;
                                PlayerAbilitiesController.instance.isAbilityActive = true;
                            }
                        }
                    }
                    OnPlayerInput.instance.onFire1 = false; // Sets the onFire1 button to false to require for another press
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
                Drop.PlayRandomClip(); //Plays sound when held object is dropped without throwing
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
                Throw.PlayRandomClip(); //Rhys - Plays sound when held object is thrown
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwforce, ForceMode.VelocityChange);
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;
                OnPlayerInput.instance.onFire1 = false;
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
