using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject soundSettings, videoSettings, controlsMenu, MainSettingsPanel;
    public Resolutions[] resolutions;
    public bool isFullscreen;
    private int currentRes;

    [System.Serializable]
    public class Resolutions
    {
        public int width;
        public int height;
    }

    // Start is called before the first frame update44
    void Start()
    {
        isFullscreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }
    public void UpdateMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", value);

    }
    public void UpdateSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", value);
    }
    public void OpenVideoSettings()
    {
        videoSettings.SetActive(true);
        soundSettings.SetActive(false);
    }
    public void OpenSoundSettings()
    {
        videoSettings.SetActive(false);
        soundSettings.SetActive(true);
    }
    
    public void ChangeRes(int res)
    {
        currentRes = res;
        Screen.SetResolution(resolutions[currentRes].width, resolutions[currentRes].height, isFullscreen);
    }

    public void ToggleFullscreen(bool check)
    {
        isFullscreen = check;
        Screen.SetResolution(resolutions[currentRes].width, resolutions[currentRes].height, isFullscreen);
    }
}
