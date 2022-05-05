using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGOOnDestroyAndOrDisable : MonoBehaviour
{

    public bool onThisDisable;
    public bool onThisDestroy;
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void OnDestroy() {
        if (onThisDestroy) {
            foreach (GameObject gO in gameObjects) {
                try {
                    gO.SetActive(true);
                }
                catch (System.Exception) {}
            }
        }
    }

    void OnDisable() {
        if (onThisDisable) {
            foreach (GameObject gO in gameObjects) {
                gO.SetActive(true);
            }
        }
    }
}
