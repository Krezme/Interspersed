using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject mainMenuCanvas;
    public GameObject controlsCanvas;
    public GameObject wannaQuitCanvas;
    public GameObject confirmStartNewGame;

    public GameObject cutScene1;
    public GameObject cutScene2;
    public GameObject cutScene3;
    public GameObject cutScene4;
    public GameObject cutScene5;

    public GameObject startButton, controlsBackButton, optionsBackButton, yesButton;

    public GameObject next1, next2, next3, next4, next5;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        if (mainMenuCanvas != null) {
            mainMenuCanvas.SetActive(true);
        }
        if (optionsCanvas != null){
        optionsCanvas.SetActive(false);
        }
        if (controlsCanvas != null){
        controlsCanvas.SetActive(false);
        }
        if (confirmStartNewGame != null) {
        confirmStartNewGame.SetActive(false);
        }
        if (wannaQuitCanvas != null)
        {
            wannaQuitCanvas.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NewLevelConcept");
    }

    public void StartNewGame () {
        if (SaveData.instance != null) {
            if (SaveData.saveAvailable) {
                mainMenuCanvas.SetActive(false);
                confirmStartNewGame.SetActive(true);
            }
            else {
                LoadCutScene1();
            }
        }else {
            LoadCutScene1();
        }
    }
    
    public void SetTextToLoading(Text text) {
        text.text = "Loading...";
    }

    /// <summary>
    /// Loads up the splash screens when the game starts
    /// </summary>
    public void LoadCutScene1() {
        SceneManager.LoadScene("CutScene1");
        EventSystem.current.SetSelectedGameObject(next1);
    }
    
    public void LoadCutScene6() {
        SceneManager.LoadScene("CutScene6");
    }
    public void LoadDemo() {
        SceneManager.LoadScene("Dev_Island");
    }

    public void LoadOldLevel() {
        SceneManager.LoadScene("ImprovedLMALevel");
    }

    public void WannaQuitGame()
    {
        EventSystem.current.SetSelectedGameObject(yesButton);
        wannaQuitCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        EventSystem.current.SetSelectedGameObject(optionsBackButton);
        optionsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void Controls()
    {
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
        controlsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        optionsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        wannaQuitCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ToggleCS2()
    {
        EventSystem.current.SetSelectedGameObject(next2);
        cutScene1.SetActive(false);
        cutScene2.SetActive(true);
    }
    public void ToggleCS3()
    {
        EventSystem.current.SetSelectedGameObject(next3);
        cutScene2.SetActive(false);
        cutScene3.SetActive(true);
    }
    public void ToggleCS4()
    {
        EventSystem.current.SetSelectedGameObject(next4);
        cutScene3.SetActive(false);
        cutScene4.SetActive(true);
    }
    public void ToggleCS5()
    {
        EventSystem.current.SetSelectedGameObject(next5);
        cutScene4.SetActive(false);
        cutScene5.SetActive(true);
    }
}
