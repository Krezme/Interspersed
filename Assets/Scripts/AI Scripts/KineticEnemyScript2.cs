using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KineticEnemyScript2 : MonoBehaviour
{

    public List<GameObject> physicsObjects = new List<GameObject>();
    private List<float> objectDistances = new List<float>();

    public GameObject playerVar;
    private bool playerInRange = false;
    private bool canThrow = false;

    public GameObject headObject;

    public GameObject object2Throw;

    public float throwCooldownTime = 2f;
    public float throwForce = 1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void findDistance()
    {
 
        //sort object list

        var PObjects = physicsObjects;
        var sortedPObjects = PObjects.OrderBy(obj => Vector3.Distance(playerVar.transform.position, obj.transform.position));

        GameObject[] sortedPObjectsArray = sortedPObjects.ToArray();

        object2Throw = sortedPObjectsArray[sortedPObjectsArray.Length - 1];


    }

    private void OnTriggerEnter(Collider other) //enter
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;

            Debug.Log("Player Entered");

            playerVar = other.gameObject;

            StartCoroutine(throwCooldownFunc());

        }
        else
        {
            if(other.attachedRigidbody.isKinematic == false)
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

            playerVar = null;


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
            headObject.transform.LookAt(other.gameObject.transform);

            if (canThrow)
            {
                throwObjectFunc();
            }
        }
    }


    private void throwObjectFunc()
    {
        StartCoroutine(throwCooldownFunc());
        findDistance();

        object2Throw.GetComponent<Rigidbody>().AddForce((playerVar.transform.position - object2Throw.transform.position) * throwForce);



    }

    IEnumerator throwCooldownFunc()
    {
        Debug.Log("Throw Charging");


        canThrow = false;
        yield return new WaitForSeconds(throwCooldownTime);
        canThrow = true;

        Debug.Log("Can Throw");


    }


}
