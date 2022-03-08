using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticEnemyScript2 : MonoBehaviour
{

    public List<GameObject> physicsObjects = new List<GameObject>();
    private List<float> objectDistances = new List<float>();

    public GameObject playerVar;
   



    // Start is called before the first frame update
    void Start()
    {
        findDistance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void findDistance()
    {
        foreach(GameObject physicsObject in physicsObjects)
        {
            float dist = Vector3.Distance(gameObject.transform.position, playerVar.transform.position);

            objectDistances.Add(dist);
        }


        objectDistances.Sort();

        foreach(float f in objectDistances)
        {
            Debug.Log(f);
        }

        Debug.Log("greatestDistance: " + objectDistances[physicsObjects.Count - 1]);



    }



}
