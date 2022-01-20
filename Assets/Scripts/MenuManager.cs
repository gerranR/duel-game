using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainSettingsPanel, soundSettings, videoSettings, mainMenuPanel;
    public void OpenSettings()
    {
        mainSettingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        soundSettings.SetActive(false);
        videoSettings.SetActive(true);
    }
    public void CloseSettings()
    {
        soundSettings.SetActive(false);
        videoSettings.SetActive(false);
        mainSettingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}