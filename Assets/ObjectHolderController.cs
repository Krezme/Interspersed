using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolderController : MonoBehaviour
{

    public GameObject objectHolderDestination;

    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, objectHolderDestination.transform.position, Time.deltaTime * followSpeed);
    }
}
