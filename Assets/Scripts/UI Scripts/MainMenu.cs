using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject mainMenuCanvas;
    public GameObject controlsCanvas;
    public GameObject creditsCanvas;

    public GameObject cutScene1;
    public GameObject cutScene2;
    public GameObject cutScene3;
    public GameObject cutScene4;
    public GameObject cutScene5;

    public GameObject startButton, controlsBackButton, optionsBackButton, creditsBackButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("NewLevelConcept");
    }

    /// <summary>
    /// Loads up the splash screens when the game starts
    /// </summary>
    public void LoadCutScene1() {
        SceneManager.LoadScene("CutScene1");
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
        EventSystem.current.SetSelectedGameObject(creditsBackButton);
        SceneManager.LoadScene("Credits");
    }

    public void ReturnToMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        optionsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ToggleCS2()
    {
        cutScene1.SetActive(false);
        cutScene2.SetActive(true);
    }
    public void ToggleCS3()
    {
        cutScene2.SetActive(false);
        cutScene3.SetActive(true);
    }
    public void ToggleCS4()
    {
        cutScene3.SetActive(false);
        cutScene4.SetActive(true);
    }
    public void ToggleCS5()
    {
        cutScene4.SetActive(false);
        cutScene5.SetActive(true);
    }
}
