using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ImprovedLMALevel");
    }

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
