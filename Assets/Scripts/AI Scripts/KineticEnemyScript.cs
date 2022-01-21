using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticEnemyScript : MonoBehaviour
{
    public GameObject headSphere;

    public List<GameObject> physicsObjects = new List<GameObject>();

    public bool playerInRange;
    public bool ready2Throw = false;
    

    public GameObject PlayerVar;

    public float ChargeTime = 3;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //enter
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;

            Debug.Log("Player Entered");

            PlayerVar = other.gameObject;
            

        }
        else
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                physicsObjects.Add(other.gameObject);


            }
        }
    }

    private void OnTriggerExit(Collider other) //exit
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;

            Debug.Log("Player Left");

            PlayerVar = null;


        }
        else
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                physicsObjects.Remove(other.gameObject);


            }
        }


    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            headSphere.transform.LookAt(other.gameObject.transform);


            if (ready2Throw == true)
            {
                KineticThrow();

            }
            else
            {

                StartCoroutine(ChargeKineticThrow());


            }



        }

        


    }

    IEnumerator ChargeKineticThrow()
    {
        Debug.Log("Enemy Charging Throw");

        yield return new WaitForSeconds(ChargeTime);

        ready2Throw = true;
        
    }
    public void KineticThrow()
    {
        ready2Throw = false;

        Debug.Log("Enemy Threw");

    }


}
