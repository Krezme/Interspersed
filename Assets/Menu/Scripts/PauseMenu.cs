using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject hudCanvas;
    public ThirdPersonPlayerController playerController;

    //public bool isPaused = false;
    CursorManager cursorManager;

    public GameObject eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);

        cursorManager = eventSystem.GetComponent<CursorManager>();
        
        playerController.enabled = true;

    }

    void Update()
    {
        if (OnPlayerInput.instance.isESC)
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
        hudCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        cursorManager.cursorLocked = false;
        cursorManager.isESC = true;

        playerController.enabled = false;
    }

    public void ResumeGame()
    {
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        cursorManager.cursorLocked = true;
        cursorManager.isESC = false;

        playerController.enabled = true;
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