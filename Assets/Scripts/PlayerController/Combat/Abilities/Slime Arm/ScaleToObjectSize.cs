using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToObjectSize : MonoBehaviour
{
    public GameObject objectScaleTo; //object to scale in size to
    private GameObject parent;
    public float scaleOffset = 1;
    public float scaleToSpeed; //speed at which it scales to the targeted Scale
    private Vector3 oldScale; //the previouse size it was scaled to
    public Vector3 parentSize; //the size the object should scale to currently



    
    void Start()
    {
        parent = transform.parent.gameObject;
        if (transform.parent.TryGetComponent<Renderer>(out Renderer renderer)) {
            parentSize = renderer.bounds.size;
        }
    }

    
    void Update()
    {
        if (transform.parent.TryGetComponent<Renderer>(out Renderer renderer)) {
            transform.localScale = (parentSize + new Vector3(scaleOffset, scaleOffset, scaleOffset));
        }
        else {
            Vector3 newScale = new Vector3(scaleOffset, scaleOffset, scaleOffset);
            transform.parent = objectScaleTo.transform;
            transform.localScale = newScale;
            transform.parent = parent.transform;
        }
    }
}
