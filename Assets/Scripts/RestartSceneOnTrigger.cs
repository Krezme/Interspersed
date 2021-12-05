using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnTrigger : MonoBehaviour
{
    /// <summary>
    /// When the right object enters the collider it will restart the scene
    /// </summary>
    /// <param name="col">The other collider</param>
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
