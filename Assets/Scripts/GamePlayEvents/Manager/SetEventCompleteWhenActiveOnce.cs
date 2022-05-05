using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEventCompleteWhenActiveOnce : MonoBehaviour
{
    public int eventIndex;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (SaveData.instance != null) {
            SaveData.instance.SetEventToComplete(eventIndex);
            SaveData.instance.RecordState();
        }
    }
}
