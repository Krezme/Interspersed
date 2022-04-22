using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlimeArm : PlayerAbility
{

    #region Singleton
    public static SlimeArm insance;
    void Awake() {
        if (insance != null) {
            Debug.LogError("THERE ARE TWO OR MORE SlimeArm INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }
        else {
            insance = this;
        }
    }

    #endregion

    public ArmAbilities[] armAbilities = new ArmAbilities[2] {new ArmAbilities() {abilityName = "Pick Up", isActive = true}, new ArmAbilities() {abilityName = "Shield"}};

    //Cooldown variables
    public float cooldownTimer = 0;

    public GameObject changeToArm; 

    public GameObject scaleSlimeBall;
    
    GameObject slimeBallInstance;
    public RandomAudioPlayer Pickup; //Rhys - RandomAudioPlayer for when the slime arm picks something up

    public RandomAudioPlayer Drop; //Rhys - RandomAudioPlayer for when the slime arm drops something

    public RandomAudioPlayer Throw; //Rhys - RandomAudioPlayer for when the slime arm throws something

    public GameObject FadeIn;

    public GameObject FadeOut;

    public RandomAudioPlayerV2 shieldSoundBank;


    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] Transform objectHolder;
    [SerializeField] Transform objectHolderShielding;
    
    Rigidbody grabbedRB;
    RagdollController grabbedRagdoll;

    public float grabbedFollowForce = 20f; // The force that is applied to pull the grabbed to the object holder.

    public float grabbedNewAngularFriction = 0.8f; //The amount of angular friction of a grabbled object

    private List<Rigidbody> changedRigidBodies = new List<Rigidbody>();
    private List<float> currentRBDefaultAngularFriction = new List<float>(); //The current Rigidbodies' Default Angular friction amount
    private List<LayerMask> currentRBDefaultLayerMask = new List<LayerMask>();

    public bool isShielding = false;
    private bool unShield = false;

    private Renderer grabbedRenderer;

    /// <summary>
    /// Hard Coding the armAbilities array
    /// </summary>
    void Start () {
        armAbilities = new ArmAbilities[2] {  
            new ArmAbilities() {abilityName = "Pick Up", isActive = true},
            new ArmAbilities() {abilityName = "Shield", isActive = armAbilities[1].isActive}
        };
    }
    
    void Update () {
        
    }

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
        OnPlayerInput.instance.onArmMode = false; // ! This makes sure that while the Slime arm is active the onArmMode button will not do anything
        base.AditionalAbilities();
        PickUpAbility();
        Shielding();
        GrabbedFollowing();
        ShieldWithGrabbed();
        LaunchGrabbed();
    }

    private void PickUpAbility() {
        if (armAbilities[0].isActive) { // Locking the Pick Up ability
            try {
                if (OnPlayerInput.instance.onFire2) {            
                    if (OnPlayerInput.instance.onFire1 && cooldownTimer == 0) {
                        if (!grabbedRB) {
                            RaycastHit hit;
                            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                            if (Physics.Raycast(ray, out hit, PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.maxGrabDistance))
                            {   
                                if (hit.collider.gameObject.TryGetComponent<GrabbedEnviromentReplacer>(out GrabbedEnviromentReplacer grabbedEnviromentReplacer)) {
                                    grabbedRB = grabbedEnviromentReplacer.Replace();
                                    Destroy(grabbedEnviromentReplacer.gameObject);
                                    OnPlayerInput.instance.onFire1 = false;
                                }
                                else if (hit.collider.gameObject.TryGetComponent<GrabbingEnviroment>(out GrabbingEnviroment grabbingEnviroment)) {
                                    grabbedRB = grabbingEnviroment.GrabObject();
                                    Destroy(grabbingEnviroment);
                                    OnPlayerInput.instance.onFire1 = false;
                                }
                                else {
                                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                                }

                                if (grabbedRB.gameObject.transform.root.TryGetComponent<RagdollController>(out grabbedRagdoll)) { // ! This is the reason why the enemies cannot go into a parent game object
                                    if (grabbedRagdoll.rig.TryGetComponent<PhysicsDamageableObject>(out PhysicsDamageableObject grabbedPhysicsDamageableObject)) {
                                        Destroy(grabbedPhysicsDamageableObject); // Removing the PhysicsDamageableObject when the object is grabbed so it is not used as a weapon while held
                                    }
                                    grabbedRagdoll.pickedUpByPlayer = true;
                                    Debug.Log("Running Ragdoll");
                                    grabbedRagdoll.RagdollOn();
                                    if (Physics.Raycast(ray, out hit, PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.maxGrabDistance))
                                    {
                                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>(); 
                                        grabbedRB.constraints = RigidbodyConstraints.FreezeRotation;
                                        Debug.Log(grabbedRB.constraints);
                                                                       
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
                                    try {
                                        grabbedRB.TryGetComponent<Renderer>(out grabbedRenderer);
                                    }catch (System.Exception) {
                                        grabbedRenderer = null;
                                    }
                                    if (grabbedRB.collisionDetectionMode == CollisionDetectionMode.Discrete) {
                                        grabbedRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
                                    }
                                    changedRigidBodies.Add(grabbedRB);
                                    currentRBDefaultAngularFriction.Add(grabbedRB.angularDrag);
                                    currentRBDefaultLayerMask.Add(grabbedRB.gameObject.layer);
                                    grabbedRB.angularDrag = grabbedNewAngularFriction;
                                    grabbedRB.gameObject.layer = 2;
                                }

                                Pickup.PlayRandomClip(); //Rhys - Plays sound only once an object has been successfully been pickup up by the slime arm
                                FadeIn.SetActive(true); //Rhys - Enables a script that fades in a looping sound that plays while an object is held                         
                                FadeOut.SetActive(false);

                                if (grabbedRagdoll != null) {
                                    slimeBallInstance = Instantiate(scaleSlimeBall, grabbedRB.transform);
                                    slimeBallInstance.GetComponent<ScaleToObjectSize>().objectScaleTo = grabbedRagdoll.rigCentre;
                                    /* FadeIn.SetActive(true);
                                    FadeOut.SetActive(false); */
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
                        cooldownTimer = PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.grabbingCooldown;
                        StartCoroutine(Ability1Cooldown());
                    }
                }
                else {
                    if (PlayerAbilitiesController.instance.isAbilityActive) {
                        LetGoOffTheObject();
                    }
                }
            }catch (System.Exception) {
                RestartGrabbedState();
            }
        }
    }

    /// <summary>
    /// Toggles the ability to shild with the grabbed
    /// </summary>
    private void Shielding(){
        if (armAbilities[1].isActive) { // Locking the Shielding ability
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
    }

    private void GrabbedFollowing(){
        if (grabbedRB && PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.slimeEnergy >= PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.holdObjectEnergyCost * Time.deltaTime){
            if (!isShielding) {
                if (grabbedRenderer != null) {
                    Vector3 newObjectHolderPosition = (objectHolder.transform.position + (grabbedRenderer.transform.position - grabbedRenderer.bounds.center));
                    grabbedRB.velocity = (newObjectHolderPosition - grabbedRB.transform.position).normalized * Vector3.Distance(newObjectHolderPosition, grabbedRB.transform.position) * grabbedFollowForce;
                }
                else {
                    grabbedRB.velocity = (objectHolder.transform.position - grabbedRB.transform.position).normalized * Vector3.Distance(objectHolder.transform.position, grabbedRB.transform.position) * grabbedFollowForce;
                }
            }
            else{
                if (grabbedRenderer != null) {
                    Vector3 newObjectHolderShieldingPosition = (objectHolderShielding.transform.position + (grabbedRenderer.transform.position - grabbedRenderer.bounds.center));
                    grabbedRB.velocity = (newObjectHolderShieldingPosition - grabbedRB.transform.position).normalized * Vector3.Distance(newObjectHolderShieldingPosition, grabbedRB.transform.position) * grabbedFollowForce;
                }
                else {
                    grabbedRB.velocity = (objectHolderShielding.transform.position - grabbedRB.transform.position).normalized * Vector3.Distance(objectHolderShielding.transform.position, grabbedRB.transform.position) * grabbedFollowForce;
                }
            }
            PlayerStatisticsManager.instance.SlimeEnergyRecharge(-(PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.energyRechargeStatistics.slimeEnergyRechargeOverOneSecond + PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.holdObjectEnergyCost) * Time.deltaTime);
        }
        else if (PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.slimeEnergy < PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.holdObjectEnergyCost * Time.deltaTime) {
            LetGoOffTheObject();
        }
        else {
            RestartGrabbedState();
        }
    }

    /// <summary>
    /// It launches the currently grabbed item with the slime arm ability
    /// </summary>
    private void LaunchGrabbed() {
        if (grabbedRB)
        {
            if (OnPlayerInput.instance.onFire1 && PlayerStatisticsManager.instance.currentStatistics.resourcesStatistics.slimeEnergy >= PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.throwEnergyCost)
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
                if (grabbedRagdoll != null) { 
                    grabbedRagdoll.pickedUpByPlayer = false;
                    grabbedRagdoll.rig.AddComponent<PhysicsDamageableObject>(); // Adding the PhysicsDamageableObject to the rig when there is a ragdoll on the grabbed object
                    grabbedRagdoll = null;
                }else {
                    grabbedRB.gameObject.AddComponent<PhysicsDamageableObject>(); // Adding a PhysicsDamageableObject to the grabbed object when there is no ragdoll
                }
                grabbedRB.AddForce((PlayerAbilitiesController.instance.rayBitch.transform.position - grabbedRB.transform.position).normalized * PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.throwforce, ForceMode.VelocityChange);
                Destroy(slimeBallInstance);
                /* slimeBallInstance = null;
                grabbedRB = null;
                grabbedRenderer = null; */
                RestartGrabbedState();
                PlayerStatisticsManager.instance.SlimeEnergyRecharge(-PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.throwEnergyCost);
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

    private void RestartGrabbedState(){
        grabbedRB = null;
        grabbedRagdoll = null;
        grabbedRenderer = null;
        changedRigidBodies = new List<Rigidbody>();
        currentRBDefaultAngularFriction = new List<float>();
        currentRBDefaultLayerMask = new List<LayerMask>();
        isShielding = false;
        unShield = false;
        if (slimeBallInstance != null) {
            Destroy(slimeBallInstance);
        }
    }

    private void LetGoOffTheObject () {
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

    private IEnumerator Ability1Cooldown()
    {
        //after the cooldown time, set the cooldown timer to 0 allowing the ability to be cast again
        yield return new WaitForSeconds(PlayerStatisticsManager.instance.currentStatistics.combatStatistics.slimeArmStats.grabbingCooldown);
        cooldownTimer = 0;
    }

    public void EnableAbility(int index) {
        armAbilities[index].isActive = true;
    }

    public void DisableAbility(int index) {
        armAbilities[index].isActive = false;
    }

    void OnValidate() {
        for (int i = 0; i < armAbilities.Length; i++) {
            armAbilities[i].Validate();
        }
    }
}
