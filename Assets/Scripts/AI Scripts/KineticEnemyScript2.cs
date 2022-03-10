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

    //graphic variables
    public LineRenderer throwLineRenderer;

    public Material lineMaterial;




    // Start is called before the first frame update
    void Start()
    {

        //initializing the line renderer
        throwLineRenderer.startWidth = 0.3f;
        throwLineRenderer.endWidth = 0.3f;

        throwLineRenderer.startColor = Color.red;
        throwLineRenderer.endColor = Color.blue;

        throwLineRenderer.material = lineMaterial;

        throwLineRenderer.SetPosition(0, headObject.transform.position);
        throwLineRenderer.SetPosition(1, headObject.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if(object2Throw == null)
        {
            return;
        }
        else
        {
            throwLineRenderer.SetPosition(0, headObject.transform.position);
            throwLineRenderer.SetPosition(1, object2Throw.transform.position);
        }


    }



    void findDistance()
    {
 
        //sort object list

        var PObjects = physicsObjects;
        var sortedPObjects = PObjects.OrderBy(obj => Vector3.Distance(playerVar.transform.position, obj.transform.position));

        GameObject[] sortedPObjectsArray = sortedPObjects.ToArray();

        object2Throw = sortedPObjectsArray[sortedPObjectsArray.Length - 1];

        //start drawing line renderer between selected object and head





    }

    private void OnTriggerEnter(Collider other) //enter
    {
        if (other.gameObject.tag == "Player") //if the player enters mark them
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
        if (other.gameObject.tag == "Player") //if it's the player mark them as non-present
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

                if(object2Throw = other.gameObject)
                {
                    object2Throw = null;
                    throwLineRenderer.SetPosition(0, headObject.transform.position);
                    throwLineRenderer.SetPosition(1, headObject.transform.position);
                }

            }
           
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") //if the player is around look and throw
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
