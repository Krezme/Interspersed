﻿using System.Collections;
using UnityEngine;

public class AudioCountdownDisableObject : MonoBehaviour
{
    [Header(" ")]
    [Header("                                     ---===== From Rhys' Collection =====---")]
    [Header("and whatever object is attached to the Variable after countdown")]
    [Header("This script will DISABLE the GameObject it is attached to")]
    [SerializeField]
   

    public float countDownFrom = 3.0f;
    public GameObject gameObjectToDisable;

    private float chosenTime = 3.0f;

    void Start()
    {
        chosenTime = countDownFrom;
    }


    void OnAwake()
    {
        countDownFrom = chosenTime;
    }


    void Update()
    {
        countDownFrom -= Time.deltaTime;
        if (countDownFrom < 0)
        {
            gameObjectToDisable.SetActive(false);
            gameObject.SetActive(false);
            countDownFrom = chosenTime;
        }
    }
}