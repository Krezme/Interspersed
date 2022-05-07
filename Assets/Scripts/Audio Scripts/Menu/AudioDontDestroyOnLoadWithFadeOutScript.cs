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

    public AudioClip MushroomSong;

    public AudioSource MenuMusicSource;

    public int MenuSelector;

    public GameObject ValienteScene;

    public GameObject DistantValleysScene;

    public GameObject TheLiberatorScene;

    public GameObject KingSlugScene;

    public GameObject Bus;



    public AudioSource MenuClickSource;

    public AudioSource MenuHoverSource;


    public static AudioDontDestroyOnLoadWithFadeOutScript instance;


    void start()
    {
        MenuMusicSource = GetComponent<AudioSource>(); //Telling the script to play the music through a single sourse which is assigned through the inspector window
    }


    void Awake()
    {
        if (instance != null) //Places the music with a DontDestroyOnLoad thing which allows it to continute playing until we start a level
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (!MenuMusicSource.isPlaying) //If a song stops playing, it gets noticed - The code then triggers the SongSelector to choose a new song and accompanying 3D background randomly
        {
            Invoke("SongSelector", 0f);
        }
    }


    void SongSelector()
    {

        MenuSelector = Random.Range(0, 4); //Generating a random digit which is assigned to a song and it's accompanying 3D background

        if (MenuSelector == 0)
        {
            MenuMusicSource.PlayOneShot(Valiente);
            ValienteScene.SetActive(true);
            DistantValleysScene.SetActive(false);
            TheLiberatorScene.SetActive(false);
            KingSlugScene.SetActive(false);
            Bus.SetActive(true);
        }

        if (MenuSelector == 1)
        {
            MenuMusicSource.PlayOneShot(DistantValleys);
            ValienteScene.SetActive(false);
            DistantValleysScene.SetActive(true);
            TheLiberatorScene.SetActive(false);
            KingSlugScene.SetActive(false);
            Bus.SetActive(false);
        }

        if (MenuSelector == 2)
        {
            MenuMusicSource.PlayOneShot(TheLiberator);
            ValienteScene.SetActive(false);
            DistantValleysScene.SetActive(false);
            TheLiberatorScene.SetActive(true);
            KingSlugScene.SetActive(false);
            Bus.SetActive(false);
        }

        if (MenuSelector == 3)
        {
            MenuMusicSource.PlayOneShot(MushroomSong);
            ValienteScene.SetActive(false);
            DistantValleysScene.SetActive(false);
            TheLiberatorScene.SetActive(false);
            KingSlugScene.SetActive(true);
            Bus.SetActive(false);
        }
    }
}