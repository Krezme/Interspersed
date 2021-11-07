using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToObjectSize : MonoBehaviour
{
    public GameObject ObjectScaleTo; //object to scale in size to
    public float ScaleOffset = 0.5f;
    public float ScaleToSpeed; //speed at which it scales to the targeted Scale
    private Vector3 OldScale; //the previouse size it was scaled to
    public Vector3 CurrentSize; //the size the object should scale to currently



    
    void Start()
    {
        CurrentSize = ObjectScaleTo.GetComponent<Renderer>().bounds.size;
    }

    
    void Update()
    {
        transform.localScale = (CurrentSize + new Vector3(ScaleOffset, ScaleOffset, ScaleOffset));
    }
}
