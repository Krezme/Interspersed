using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalEnergybar : MonoBehaviour
{
    #region Singleton

    public static CrystalEnergybar instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            Debug.LogError("There is a second instance of CrystalEnergybar. Please remove.");
        }
        instance = this;
    }

    #endregion
    public Slider slider;

}
