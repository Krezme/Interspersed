using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerGameObject : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - this.gameObject.transform.position).normalized);       
    }
}
