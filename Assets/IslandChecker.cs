using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandChecker : MonoBehaviour
{
    public GameObject spinner;
    public GameObject theSpot;

    // Start is called before the first frame update
    

    private void OnTriggerEnter(Collider other)
    {
        Transform _spinner = Instantiate(spinner.transform, theSpot.transform.position, Quaternion.identity);
        _spinner.transform.rotation = theSpot.transform.rotation;
        Destroy(this.gameObject);
    }

    
}
