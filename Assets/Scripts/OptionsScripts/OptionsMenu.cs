using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    /// Define variables here
    public AudioMixer mixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public static float masterVol =1.0f, musicVol = 1.0f, sfxVol = 1.0f;
    public Slider masterSlider, musicSlider, sfxSlider;

    public Toggle invertXToggle, invertYToggle;
    public Slider mouseSensitivitySlider, mouseAimSensitivitySlider;
    public Text mouseSensitivityText, mouseAimSensitivityText;

    public Toggle sprintToggle, walkToggle;

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

        UpdateSettings();
    }

    public void UpdateSettings()
    {
        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        invertXToggle.isOn = OnPlayerInput.invertXBool;
        invertYToggle.isOn = OnPlayerInput.invertYBool;

        sprintToggle.isOn = OnPlayerInput.isSprintToggleable;
        walkToggle.isOn = OnPlayerInput.isWalkingToggleable;

        mouseSensitivitySlider.value = OnPlayerInput.mouseSensitivity;
        mouseAimSensitivitySlider.value = OnPlayerInput.mouseSensitivityAim;

        mouseSensitivityText.text = OnPlayerInput.mouseSensitivity.ToString();
        mouseAimSensitivityText.text = OnPlayerInput.mouseSensitivityAim.ToString();

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
        
        OnPlayerInput.invertYBool = isInverted;

    }
    public void InvertX(bool isInverted)
    {

        OnPlayerInput.invertXBool = isInverted;

    }
    public void MouseSensitivity(float sliderValue)
    {
        OnPlayerInput.mouseSensitivity = sliderValue;
        mouseSensitivityText.text = OnPlayerInput.mouseSensitivity.ToString();
    }
    public void MouseAimSensitivity(float sliderValue)
    {
        OnPlayerInput.mouseSensitivityAim = sliderValue;
        mouseAimSensitivityText.text = OnPlayerInput.mouseSensitivityAim.ToString();
    }
    public void OnSprintToggle(bool toggle)
    {
        OnPlayerInput.isSprintToggleable = toggle;
    }
    public void OnWalkToggle(bool toggle)
    {
        OnPlayerInput.isWalkingToggleable = toggle;
    }
}
