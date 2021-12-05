using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Statistics", menuName = "Enemies/Enemy Statistics")]
public class EnemyStatisticsSO : ScriptableObject
{
    //
    public float health;
    public string description;
}
