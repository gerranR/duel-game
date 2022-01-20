using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SwapPanel;
    public void StartGame(GameObject button)
    {
        SwapPanel.GetComponent<Animator>().SetTrigger("StartGame");

    }
    public void Quit()
    {
        Application.Quit();
    }
}
