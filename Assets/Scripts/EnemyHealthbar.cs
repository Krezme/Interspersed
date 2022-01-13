using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    // The enemy HP bar slide UI object
    public Slider enemyHealthBar;

    public void SetEnemyMaxHealth(int warphealth)
    {
        enemyHealthBar.maxValue = warphealth;
        enemyHealthBar.value = warphealth;
    }

    /// <summary>
    /// Updates the enemy HP bar
    /// </summary>
    /// <param name="health">enemy's health</param>
    public void SetEnemyHealth(float health)
    {
        enemyHealthBar.value = health;
    }
}
