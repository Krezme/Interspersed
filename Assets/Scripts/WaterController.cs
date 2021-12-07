using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaterProperties
{
    public bool isCharged;
    public float damage;

}

public class WaterController : MonoBehaviour
{

    public WaterProperties waterProperties;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
       if (other.gameObject.tag == "PlayerBullet")
       {
            Debug.Log("Exit");
       } 
    }
}
