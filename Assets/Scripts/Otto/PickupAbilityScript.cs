using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAbilityScript : MonoBehaviour
{

    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwforce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;

    Rigidbody grabbedRB;

    //Extras
    public GameObject AimPoint;
    public GameObject SphereBound;
    public Renderer GrabbedMesh;
    
    void Update()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(objectHolder.transform.position);


            SphereBound.transform.position = Vector3.MoveTowards(SphereBound.transform.position, grabbedRB.transform.position, 0.1f);
            //SphereBound.transform.position = grabbedRB.transform.position;
            
            
            if (OnPlayerInput.instance.onFire1)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwforce, ForceMode.VelocityChange);
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;

                SphereBoundFuncOff();
                GrabbedMesh = null;

            }
        }


        Ability1CastFunc();
    }

    private void Ability1CastFunc()
    {
        if (OnPlayerInput.instance.onAbility1 && cooldownTimer == 0)
        {
            print("Ability1Cast");
            if (grabbedRB) //if there is a grabbed rigidbody when we press the key, drop it.
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
                PlayerAbilitiesController.instance.isAbilityActive = false;

                SphereBoundFuncOff();
                GrabbedMesh = null;

            }
            else //if there is not a grabbed rigid body, raycast for one and 'grab it' (setting grabbedrb to it)
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                        PlayerAbilitiesController.instance.isAbilityActive = true;

                        GrabbedMesh = hit.transform.gameObject.GetComponent<Renderer>(); //get the same objects renderer
                        SphereBoundFuncOn(); //sphere bound on                   

                    }
                }
            }

            //start the cooldown 
            cooldownTimer = cooldownMaxTime;
            StartCoroutine(Ability1Cooldown());

        }

    }

    private IEnumerator Ability1Cooldown()
    {
        //after the cooldown time, set the cooldown timer to 0 allowing the ability to be cast again
        yield return new WaitForSeconds(cooldownMaxTime);
        cooldownTimer = 0;
    }


    private void SphereBoundFuncOn()
    {
        SphereBound.SetActive(true); //set the spherebound to active

        if (GrabbedMesh.GetComponent<Renderer>()) //if the grabbedmesh has a normal renderer
        {
            SphereBound.transform.localScale = GrabbedMesh.GetComponent<Renderer>().bounds.size + new Vector3(0.5f, 0.5f, 0.5f); //scale it to be the same scale + a bit bigger
        }
            
    }

    private void SphereBoundFuncOff() //turns the sphere bounds off
    {
        SphereBound.SetActive(false);

    }



}
