using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectByAnimationEvent : MonoBehaviour
{
    public GameObject gameObjectToEnable;

    public void EnableGameObjectAnimationEvent() {
        gameObjectToEnable.SetActive(true);
    }
}
