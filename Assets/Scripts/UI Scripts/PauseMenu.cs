using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas, optionsMenuCanvas, controlsMenuCanvas;
    public GameObject hudCanvas;
    public OnPlayerInput playerInput;

    //public bool isPaused = false;
    CursorManager cursorManager;

    public GameObject eventSystem;

    public AudioMixerSnapshot PausedSnapshot;

    public AudioMixerSnapshot UnpausedSnapshot;

    private float TransitionTime = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);

        cursorManager = eventSystem.GetComponent<CursorManager>();

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
        playerInput.isAllowedToMove = false;

        OnPlayerInput.instance.ResetInput();

        PausedSnapshot.TransitionTo(TransitionTime);

        hudCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        cursorManager.cursorLocked = false;
        cursorManager.isESC = true;
    }

    public void ResumeGame()
    {
        playerInput.isAllowedToMove = true;
        Debug.Log("Resume");

        UnpausedSnapshot.TransitionTo(TransitionTime);

        hudCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        cursorManager.cursorLocked = true;
        cursorManager.isESC = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Options()
    {
        optionsMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void Controls()
    {
        controlsMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        controlsMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(false);
    }

}
