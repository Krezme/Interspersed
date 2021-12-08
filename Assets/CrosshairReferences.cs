using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairReferences : MonoBehaviour
{

#region Singleton
    public static CrosshairReferences instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("THERE ARE 2 CrosshairReferences SCRIPTS IN EXISTANCE");
            return;
        }
        instance = this;
    }
#endregion

    public Slider[] chargesUI;
}
