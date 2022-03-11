using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroUIFadeAndVideo : MonoBehaviour
{

    public GameObject FadeToBlack;

    public GameObject Video;

    public GameObject Credits;

    public GameObject CreditsMusic;

    private float FadeTimer = 0;


    void Update()
    {
        FadeTimer += Time.deltaTime;

        if (FadeTimer > 1)
        {
            //FadeToBlack.SetActive(false);
            Video.SetActive(true);
            Debug.Log("Video Enable");
        }
        else
        {
            FadeToBlack.SetActive(true);
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
        }

        if (FadeTimer > 52)
        {
            SceneManager.LoadScene(0);
        }
    }
}
