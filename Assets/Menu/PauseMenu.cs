using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject hudCanvas;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (OnPlayerInput.instance.isESC)
        {
            isPaused = true;
        }

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        hudCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
