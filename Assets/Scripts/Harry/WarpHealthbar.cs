using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpHealthbar : MonoBehaviour
{
    public Slider warpslider;
    public int warpmaxHealth = 80;
    void Start()
    {
        warpslider.value = warpmaxHealth;
    }

    public void SetwarpMaxHealth(int warphealth)
    {
        warpslider.maxValue = warphealth;
        warpslider.value = warphealth;
    }

    public void SetwarpHealth(int warphealth)
    {
        warpslider.value = warphealth;
    }
}
