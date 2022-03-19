using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDontDestroyOnLoadWithFadeOutScript : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    public GameObject FadeOutAudio;

    public GameObject FadeInAudio;


    public AudioClip Valiente;

    public AudioClip DistantValleys;

    public AudioClip TheLiberator;

    public AudioSource MenuMusicSource;

    public int MenuSelector;

    public GameObject ValienteScene;

    public GameObject DistantValleysScene;

    public GameObject TheLiberatorScene;


    public static AudioDontDestroyOnLoadWithFadeOutScript instance;

    void start()
    {
        MenuMusicSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        MenuSelector = Random.Range(0, 3);

        if (MenuSelector == 0)
        {
            MenuMusicSource.PlayOneShot(Valiente);
            ValienteScene.SetActive(true);
            DistantValleysScene.SetActive(false);
            TheLiberatorScene.SetActive(false);

        }

        if (MenuSelector == 1)
        {
            MenuMusicSource.PlayOneShot(DistantValleys);
            ValienteScene.SetActive(false);
            DistantValleysScene.SetActive(true);
            TheLiberatorScene.SetActive(false);
        }

        if (MenuSelector == 2)
        {
            MenuMusicSource.PlayOneShot(TheLiberator);
            ValienteScene.SetActive(false);
            DistantValleysScene.SetActive(false);
            TheLiberatorScene.SetActive(true);
        }



        if (instance != null) 
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
