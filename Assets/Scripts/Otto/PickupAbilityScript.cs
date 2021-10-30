using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAbilityScript : MonoBehaviour
{
    //PlayerInput
    public OnPlayerInput onPlayerInput;

    //Cooldown variables
    public float cooldownMaxTime = 1;
    public float cooldownTimer = 0;

    //Functionality Variables
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwforce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;

    Rigidbody grabbedRB;

    void Start()
    {
        onPlayerInput = GetComponent<OnPlayerInput>();
    }

    
    void Update()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(objectHolder.transform.position);

            if (onPlayerInput.onFire1)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwforce, ForceMode.VelocityChange);
                grabbedRB = null;
            }
        }


        ability1CastFunc();
    }

    private void ability1CastFunc()
    {
        if (onPlayerInput.onAbility1 && cooldownTimer == 0)
        {
            print("Ability1Cast");
            if (grabbedRB) //if there is a grabbed rigidbody when we press the key, drop it.
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
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
                    }
                }
            }

            //start the cooldown 
            cooldownTimer = cooldownMaxTime;
            StartCoroutine(ability1Cooldown());

        }

    }

    private IEnumerator ability1Cooldown()
    {
        //after the cooldown time, set the cooldown timer to 0 allowing the ability to be cast again
        yield return new WaitForSeconds(cooldownMaxTime);
        cooldownTimer = 0;
    }

}
