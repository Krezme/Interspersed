using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class OptionsMenu : MonoBehaviour
{
    /// Define variables here
    public AudioMixer mixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public static float masterVol, musicVol, sfxVol;

    public InputActionReference cameraAction;

    void Start()
    {
        ///Setting the resolutions at the start
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); ///Clearing placeholder resolutions
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i =0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// This Section is to control audio settings
    public void SetMasterVolLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        masterVol = sliderValue;
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicVol = sliderValue;
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        sfxVol = sliderValue;
    }

    ///This section controls Quality settings
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    ///This section controls Resolution settings
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    ///This section controls fulscreen settings
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //This section controls Camera settings
    public void InvertY(bool isInverted)
    {
        if (isInverted)
        {
            Debug.Log("Not Inverted");
            cameraAction.action.ChangeBinding(0);
            cameraAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=true,invertY)" });
        }
        else
        {
            Debug.Log("Inverted");
            cameraAction.action.ApplyBindingOverride(new InputBinding { overrideProcessors = "invertVector2(invertX=false,invertY)" });
        }
        
    }
}
