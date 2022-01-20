using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartEventScript : MonoBehaviour
{
    public void LoadSceneEvent()
    {
        SceneManager.LoadScene(1);
    }
}
