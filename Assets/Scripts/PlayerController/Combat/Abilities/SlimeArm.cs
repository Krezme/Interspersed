using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlimeArm : PlayerAbility
{
    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    public GameObject changeToArm; 

    public GameObject scaleSlimeBall;
    
    GameObject slimeBallInstance;
    public RandomAudioPlayer Pickup; //Rhys - RandomAudioPlayer for when the slime arm picks something up

    public RandomAudioPlayer Drop; //Rhys - RandomAudioPlayer for when the slime arm drops something

    public RandomAudioPlayer Throw; //Rhys - RandomAudioPlayer for when the slime arm throws something

    public GameObject FadeIn;

    public GameObject FadeOut;


    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwforce = 20f;
    [SerializeField] Transform objectHolder;
    [SerializeField] Transform objectHolderShielding;
    
    Rigidbody grabbedRB;
    RagdollController grabbedRagdoll;

    public float grabbedFollowForce = 20f; // The force that is applied to pull the grabbed to the object holder.

    public float grabbedNewAngularFriction = 0.8f; //The amount of angular friction of a grabbled object

    private List<Rigidbody> changedRigidBodies = new List<Rigidbody>();
    private List<float> currentRBDefaultAngularFriction = new List<float>(); //The current Rigidbodies' Default Angular friction amount
    private List<LayerMask> currentRBDefaultLayerMask = new List<LayerMask>();

    private bool isShielding = false;
    private bool unShield = false;

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
        Shielding();
        GrabbedFollowing();
        ShieldWithGrabbed();
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
                        Debug.Log(grabbedRB.gameObject.transform.root.GetComponent<RagdollController>());
                        
                        if (grabbedRB.gameObject.transform.root.TryGetComponent<RagdollController>(out grabbedRagdoll)) {
                            grabbedRagdoll.pickedUpByPlayer = true;
                            Debug.Log("Running Ragdoll");
                            grabbedRagdoll.RagdollOn();
                            if (Physics.Raycast(ray, out hit, maxGrabDistance))
                            {
                                grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>(); 
                                grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;
                                Debug.Log(grabbedRB.constraints);
                                Pickup.PlayRandomClip(); //Rhys - Plays sound only once an object has been successfully been pickup up by the slime arm
                                FadeIn.SetActive(true); //Rhys - Enables a script that fades in a looping sound that plays while an object is held                         
                                FadeOut.SetActive(false);                               
                            }
                            foreach (Rigidbody rb in grabbedRagdoll.ragdollRigidbodies) {
                                changedRigidBodies.Add(rb);
                                currentRBDefaultAngularFriction.Add(rb.angularDrag);
                                currentRBDefaultLayerMask.Add(rb.gameObject.layer);
                                rb.angularDrag = grabbedNewAngularFriction;
                                rb.gameObject.layer = 2;
                            }
                        }
                        else {
                            
                            if (grabbedRB.collisionDetectionMode == CollisionDetectionMode.Discrete) {
                                grabbedRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
                            }
                            changedRigidBodies.Add(grabbedRB);
                            currentRBDefaultAngularFriction.Add(grabbedRB.angularDrag);
                            currentRBDefaultLayerMask.Add(grabbedRB.gameObject.layer);
                            grabbedRB.angularDrag = grabbedNewAngularFriction;
                            grabbedRB.gameObject.layer = 2;
                        }
                        if (grabbedRagdoll != null) {
                            slimeBallInstance = Instantiate(scaleSlimeBall, grabbedRB.transform);
                            slimeBallInstance.GetComponent<ScaleToObjectSize>().objectScaleTo = grabbedRagdoll.rigCentre;
                            FadeIn.SetActive(true);
                            FadeOut.SetActive(false);
                        }
                        else {
                            slimeBallInstance = Instantiate(scaleSlimeBall, grabbedRB.transform);
                            slimeBallInstance.GetComponent<ScaleToObjectSize>().objectScaleTo = grabbedRB.gameObject;
                        }
                        
                        if (grabbedRB)
                        {
                            grabbedRB.useGravity = false;
                            PlayerAbilitiesController.instance.isAbilityActive = true;
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
                grabbedRB.useGravity = true;
                grabbedRB.constraints = RigidbodyConstraints.None;
                for (int i = 0; i < changedRigidBodies.Count; i++) {
                    changedRigidBodies[i].angularDrag = currentRBDefaultAngularFriction[i];
                    changedRigidBodies[i].gameObject.layer = currentRBDefaultLayerMask[i];
                }
                UnShieldWithGrabbed();
                changedRigidBodies = new List<Rigidbody>();
                currentRBDefaultAngularFriction = new List<float>();
                currentRBDefaultLayerMask = new List<LayerMask>();
                if (grabbedRagdoll != null) { 
                    grabbedRagdoll.pickedUpByPlayer = false;
                    grabbedRagdoll = null;
                }
                Destroy(slimeBallInstance);
                slimeBallInstance = null;
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;
                Drop.PlayRandomClip(); //Plays sound when held object is dropped without throwing
                FadeIn.SetActive(false);
                FadeOut.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Toggles the ability to shild with the grabbed
    /// </summary>
    private void Shielding(){
        if (grabbedRB)
        {
            if (OnPlayerInput.instance.onAbility1) {
                if (isShielding) {
                    unShield = true;
                }
                isShielding = !isShielding;
                OnPlayerInput.instance.onAbility1 = false;
            }
        }
        else{
            isShielding = false;
        }
    }

    private void GrabbedFollowing(){
        if (grabbedRB)
        {
            if (!isShielding) {
                grabbedRB.velocity = (objectHolder.transform.position - grabbedRB.transform.position).normalized * Vector3.Distance(objectHolder.transform.position, grabbedRB.transform.position) * grabbedFollowForce;
            }
            else{
                grabbedRB.velocity = (objectHolderShielding.transform.position - grabbedRB.transform.position).normalized * Vector3.Distance(objectHolderShielding.transform.position, grabbedRB.transform.position) * grabbedFollowForce;
            }
        }
    }

    /// <summary>
    /// It launches the currently grabbed item with the slime arm ability
    /// </summary>
    private void LaunchGrabbed() {
        if (grabbedRB)
        {
            if (OnPlayerInput.instance.onFire1)
            {
                Throw.PlayRandomClip(); //Rhys - Plays sound when held object is thrown
                FadeIn.SetActive(false);
                FadeOut.SetActive(true);
                grabbedRB.isKinematic = false;
                grabbedRB.useGravity = true;
                for (int i = 0; i < changedRigidBodies.Count; i++) {
                    changedRigidBodies[i].angularDrag = currentRBDefaultAngularFriction[i];
                    changedRigidBodies[i].gameObject.layer = currentRBDefaultLayerMask[i];
                }
                UnShieldWithGrabbed();
                grabbedRB.constraints = RigidbodyConstraints.None;
                changedRigidBodies = new List<Rigidbody>();
                currentRBDefaultAngularFriction = new List<float>();
                currentRBDefaultLayerMask = new List<LayerMask>();
                grabbedRB.gameObject.AddComponent<PhysicsDamageableObject>(); // ? Maybe add this component to the chest of the enemy, if grabbedRagdoll is not null, so the actual object that needs to collide is the chest and NOT the grabbed one
                grabbedRB.AddForce((PlayerAbilitiesController.instance.rayBitch.transform.position - grabbedRB.transform.position).normalized * throwforce, ForceMode.VelocityChange);
                if (grabbedRagdoll != null) { 
                    grabbedRagdoll.pickedUpByPlayer = false;
                    grabbedRagdoll = null;
                }
                Destroy(slimeBallInstance);
                slimeBallInstance = null;
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;
                OnPlayerInput.instance.onFire1 = false;
            }
        }
    }

    private void ShieldWithGrabbed() {
        if (isShielding) {
            if (grabbedRagdoll != null) {
                foreach(Rigidbody rb in grabbedRagdoll.ragdollRigidbodies) {
                    rb.constraints  = RigidbodyConstraints.FreezeRotation;
                    rb.GetComponent<Collider>().isTrigger = true;
                }
            }
            else{
                grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;
                grabbedRB.GetComponent<Collider>().isTrigger = true;

            }
        }
        else {
            if (grabbedRB && unShield) {
                unShield = false;
                UnShieldWithGrabbed();
            }
        }
    }
    private void UnShieldWithGrabbed() {
        if (grabbedRagdoll != null) {
            foreach(Rigidbody rb in grabbedRagdoll.ragdollRigidbodies) {
                rb.constraints  = RigidbodyConstraints.None;
                rb.GetComponent<Collider>().isTrigger = false;
            }
            grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else{
            grabbedRB.constraints = RigidbodyConstraints.None;
            grabbedRB.GetComponent<Collider>().isTrigger = false;
        }
    }

    private IEnumerator Ability1Cooldown()
    {
        //after the cooldown time, set the cooldown timer to 0 allowing the ability to be cast again
        yield return new WaitForSeconds(cooldownMaxTime);
        cooldownTimer = 0;
    }
}
