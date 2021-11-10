using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Slider enemySlider;

    public void SetwarpMaxHealth(int warphealth)
    {
        enemySlider.maxValue = warphealth;
        enemySlider.value = warphealth;
    }

    public void SetwarpHealth(float warphealth)
    {
        enemySlider.value = warphealth;
    }
}
