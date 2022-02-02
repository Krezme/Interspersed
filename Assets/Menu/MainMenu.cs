using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas optionsCanvas;
    public Canvas mainMenuCanvas;

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
        optionsCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }
    public void ReturnToMainMenu()
    {
        optionsCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
}
