using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainSettingsPanel, soundSettings, videoSettings;
    public void OpenSettings()
    {
        MainSettingsPanel.SetActive(true);
        soundSettings.SetActive(false);
        videoSettings.SetActive(false);
    }
    public void CloseSettings()
    {
        soundSettings.SetActive(false);
        videoSettings.SetActive(false);
        MainSettingsPanel.SetActive(false);
    }
}