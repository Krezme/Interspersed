using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVignette : MonoBehaviour
{
    #region Singleton
    public static HealthVignette instance;
    void Awake () {
        if (instance != null) {
            Debug.LogError("THERE ARE TWO OR MORE HealthVignette INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }
        else {
            instance = this;
        }
    }
    #endregion

    public Image image;
}
