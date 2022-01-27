using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToObjectSize : MonoBehaviour
{
    public GameObject objectScaleTo; //object to scale in size to
    public float scaleOffset = 1;
    public float scaleToSpeed; //speed at which it scales to the targeted Scale
    private Vector3 oldScale; //the previouse size it was scaled to
    public Vector3 currentSize; //the size the object should scale to currently



    
    void Start()
    {
        objectScaleTo = transform.parent.gameObject;
        currentSize = objectScaleTo.GetComponent<Renderer>().bounds.size;
    }

    
    void Update()
    {
        transform.localScale = (currentSize + new Vector3(scaleOffset, scaleOffset, scaleOffset));
    }
}
