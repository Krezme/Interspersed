using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    #region Singleton

    public static Energybar instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            Debug.LogError("There is a second instance of energy bar. Please remove.");
        }
        instance = this;
    }

    #endregion
    public Slider slider;

}
