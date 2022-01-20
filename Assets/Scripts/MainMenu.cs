using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SwapPanel, music;
    public void StartGame(GameObject button)
    {
        DontDestroyOnLoad(music);
        SwapPanel.GetComponent<Animator>().SetTrigger("StartGame");

    }
    public void Quit()
    {
        Application.Quit();
    }
}
