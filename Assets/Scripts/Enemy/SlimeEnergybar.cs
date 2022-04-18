using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeEnergybar : MonoBehaviour
{
    #region Singleton

    public static SlimeEnergybar instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            Debug.LogError("There is a second instance of SlimeEnergybar. Please remove.");
        }
        instance = this;
    }

    #endregion
    public Slider slider;

}
