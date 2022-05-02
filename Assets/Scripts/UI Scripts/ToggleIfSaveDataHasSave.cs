using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIfSaveDataHasSave : MonoBehaviour
{
    
    public Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveData.instance != null) {
            if (SaveData.instance.CheckIfSaveStateIsNotDef()) {
                thisButton.interactable = true;
            }else {
                thisButton.interactable = false;
            }
        }else {
            thisButton.interactable = false;
        }
    }
}
