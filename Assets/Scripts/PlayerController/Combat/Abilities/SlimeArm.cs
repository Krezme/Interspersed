using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeArm : PlayerAbility
{
    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    public GameObject changeToArm;

    public GameObject scaleSlimeBall;
    
    GameObject slimeBallInstance;

    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwforce = 20f;
    [SerializeField] Transform objectHolder;
    
    Rigidbody grabbedRB;
    RagdollController grabbedRagdoll;

    public float grabbedFollowForce = 20f; // The force that is applied to pull the grabbed to the object holder.

    public float grabbedNewAngularFriction = 0.8f; //The amount of angular friction of a grabbled object

    ////private float currentGRBDefaultAngularFriction; //The current Grabbed Rigidbody Default Angular friction amount

    private List<Rigidbody> changedRigidBodies = new List<Rigidbody>();
    private List<float> currentRBDefaultAngularFriction = new List<float>(); //The current Rigidbodies' Default Angular friction amount

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
                        if (grabbedRB.gameObject.transform.root.TryGetComponent<RagdollController>(out grabbedRagdoll)) {
                            grabbedRagdoll.pickedUpByPlayer = true;
                            
                            grabbedRagdoll.RagdollOn();
                            if (Physics.Raycast(ray, out hit, maxGrabDistance))
                            {
                                grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>(); 
                                grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;
                            }
                            foreach (Rigidbody rb in grabbedRagdoll.ragdollRigidbodies) {
                                changedRigidBodies.Add(rb);
                                currentRBDefaultAngularFriction.Add(rb.angularDrag);
                                rb.angularDrag = grabbedNewAngularFriction;
                            }
                        }
                        else {
                            
                            if (grabbedRB.collisionDetectionMode == CollisionDetectionMode.Discrete) {
                                grabbedRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
                            }
                            changedRigidBodies.Add(grabbedRB);
                            currentRBDefaultAngularFriction.Add(grabbedRB.angularDrag);
                            grabbedRB.angularDrag = grabbedNewAngularFriction;
                        }
                        if (grabbedRagdoll != null) {
                            slimeBallInstance = Instantiate(scaleSlimeBall, grabbedRB.transform);
                            slimeBallInstance.GetComponent<ScaleToObjectSize>().objectScaleTo = grabbedRagdoll.rigCentre;
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
                }
                changedRigidBodies = new List<Rigidbody>();
                currentRBDefaultAngularFriction = new List<float>();
                if (grabbedRagdoll != null) { 
                    grabbedRagdoll.pickedUpByPlayer = false;
                    grabbedRagdoll = null;
                }
                Destroy(slimeBallInstance);
                slimeBallInstance = null;
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
            grabbedRB.velocity = (objectHolder.transform.position - grabbedRB.transform.position).normalized * Vector3.Distance(objectHolder.transform.position, grabbedRB.transform.position) * grabbedFollowForce;
            
            if (OnPlayerInput.instance.onFire1)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.useGravity = true;
                grabbedRB.constraints = RigidbodyConstraints.None;
                for (int i = 0; i < changedRigidBodies.Count; i++) {
                    changedRigidBodies[i].angularDrag = currentRBDefaultAngularFriction[i];
                }
                changedRigidBodies = new List<Rigidbody>();
                currentRBDefaultAngularFriction = new List<float>();
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
        if (OnPlayerInput.instance.onFire2) {
            if (grabbedRB && OnPlayerInput.instance.onAbility1) {
                if (grabbedRagdoll != null) {
                    foreach(Rigidbody rb in grabbedRagdoll.ragdollRigidbodies) {
                        rb.constraints  = RigidbodyConstraints.FreezeRotation;
                    }
                }
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
