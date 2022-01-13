using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGamePlaceHolder : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (OnPlayerInput.instance.isESC) {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
    }
}
