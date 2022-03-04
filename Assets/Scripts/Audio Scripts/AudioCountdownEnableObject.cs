using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCountdownEnableObject : MonoBehaviour
{

    [Header("and whatever object is attached to the Variable afster countdown")]
    [Header("This script will ENABLE the GameObject it is attached to")]
    [SerializeField]


    public float countDownFrom = 3.0f;
    public GameObject gameObjectToEnable;

    void Update()
    {
        countDownFrom -= Time.deltaTime;
        if (countDownFrom < 0)
        {
            gameObjectToEnable.SetActive(true);
            gameObject.SetActive(false);
            // Debug.Log("Test");
        }
    }
}