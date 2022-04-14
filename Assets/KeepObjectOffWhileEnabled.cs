using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOffWhileEnabled : MonoBehaviour
{
    public GameObject KeepOff;
    
    void Update()
    {
        KeepOff.SetActive(false);
    }
}
