using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OutroUIFadeAndVideo : MonoBehaviour
{

    public GameObject FadeToBlack;

    public GameObject Video;

    public GameObject Credits;

    public GameObject CreditsMusic;

    private float FadeTimer = 0;

    public GameObject HUD;

    public AudioMixerSnapshot CreditsMixerSnapshot;

    public AudioMixerSnapshot UnpausedMixerSnapshot;

    private float CreditsTransitionTime = 1f;

    private float UnpausedTransitionTime = 0.01f;





    void Start()
    {
        StartCoroutine(LoadScene());
    }


    void Update()
    {
        FadeTimer += Time.deltaTime;

        if (FadeTimer > 1)
        {
            CreditsMixerSnapshot.TransitionTo(CreditsTransitionTime);
            //FadeToBlack.SetActive(false);
            Video.SetActive(true);
            Debug.Log("Video Enable");
        }
        else
        {
            FadeToBlack.SetActive(true);
            HUD.SetActive(false);
        }

        if (FadeTimer > 1.1f)
        {
            FadeToBlack.SetActive(false);
        }

        if (FadeTimer > 10) //was set to 7 then 9
        {
            Credits.SetActive(true);
        }

        if (FadeTimer > 9) //was set to 8
        {
            CreditsMusic.SetActive(true);
        }

        if (FadeTimer > 51)
        {
            Credits.SetActive(false);
            //HUD.SetActive(true);
        }

        if (FadeTimer > 52)
        {
            HUD.SetActive(true);
            CursorManager.instance.SetCursorState(false);
            //SceneManager.LoadScene(1);
            UnpausedMixerSnapshot.TransitionTo(UnpausedTransitionTime);
        }
    }


    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Menu");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                //m_Text.text = "100%";
                //Wait for intro to finish before activating the Scene
                if (FadeTimer > 52)
                {
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }



}
