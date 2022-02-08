using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject mainMenuCanvas;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ImprovedLMALevel");
    }

    /// <summary>
    /// Loads up the splash screens when the game starts
    /// </summary>
    public void LoadCutScene1() {
        SceneManager.LoadScene("CutScene1");
    }
    public void LoadCutScene2() {
        SceneManager.LoadScene("CutScene2");
    }
    public void LoadCutScene3() {
        SceneManager.LoadScene("CutScene3");
    }
    public void LoadCutScene4() {
        SceneManager.LoadScene("CutScene4");
    }
    public void LoadCutScene5() {
        SceneManager.LoadScene("CutScene5");
    }
    public void LoadCutScene6() {
        SceneManager.LoadScene("CutScene6");
    }
    public void LoadDemo() {
        SceneManager.LoadScene("Dev_Island");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
