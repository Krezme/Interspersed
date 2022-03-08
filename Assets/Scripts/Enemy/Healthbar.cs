using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    #region Singleton
    public static Healthbar instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            Debug.LogError("There is a second instance of health bar. Please remove.");
        }
        instance = this;
    }

    #endregion

    public Slider slider;
}
