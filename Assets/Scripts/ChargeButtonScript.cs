using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeButtonScript : ChargableManager
{
    public GameObject ObjectToMove;

    public GameObject MoveToLocation;

    public float MoveSpeedFloat = 1;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public override void OnCharged()
    {
        base.OnCharged();

        ObjectToMove.transform.position = Vector3.Lerp(ObjectToMove.transform.position, MoveToLocation.transform.position, MoveSpeedFloat);
    }




}
