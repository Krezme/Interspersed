using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsynchronousSceneLoader : MonoBehaviour
{

    public bool SceneReady = false;

    //!public Text m_Text;

    public Button uibutton;

    public string SceneName;

    

    //public static AsynchronousSceneLoader instance;


    void Start()
    {
        StartCoroutine(LoadScene());
        Button btn = uibutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneReady = true;
    }

    void Awake()
    {
        //if (instance != null)
        //{
            //Destroy(instance.gameObject);
        //}
        //instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar

        while (!asyncOperation.isDone)
        {
            //!m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%"; //Output the current progress
            if (asyncOperation.progress >= 0.9f) // Check if the load has finished
            {  
                //m_Text.text = "100%"; //Change the Text to show the Scene is ready
                
                if (SceneReady == true)
                {   
                    asyncOperation.allowSceneActivation = true; //Activate the Scene
                }
            }

            yield return null;
        }
    }


}
