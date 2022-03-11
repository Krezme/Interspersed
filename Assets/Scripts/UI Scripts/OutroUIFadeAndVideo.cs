using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroUIFadeAndVideo : MonoBehaviour
{

    public GameObject FadeToBlack;

    public GameObject Video;

    public GameObject Credits;

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

        if (FadeTimer > 7)
        {
            Credits.SetActive(true);
        }
    }
}
