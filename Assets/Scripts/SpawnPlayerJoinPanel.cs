using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using TMPro;

public class SpawnPlayerJoinPanel : MonoBehaviour
{
    public GameObject playerJoinPrefab;
    public PlayerInput input;
    private GameObject menu;

    private void Awake()
    {
        var rootMenu = GameObject.Find("PlayerJoinPanel");
        if(rootMenu != null)
        {
            menu = Instantiate(playerJoinPrefab, rootMenu.transform);
            menu.transform.Find("Back").GetComponent<Button>().onClick.AddListener(delegate { rootMenu.SetActive(false); });
            StartCoroutine(changeText());
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
        }
    }

    IEnumerator changeText()
    {
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<PlayerInput>().playerIndex == 0)
        { 
            FindObjectOfType<GameManeger>().ResetLevel  (3, false);
        }
        else
        { 
            FindObjectOfType<GameManeger>().ResetLevel(3,false);
        }
        var playerText = menu.transform.Find("PlayerText");
        if (input.gameObject.GetComponent<PlayerHealth>().playerInt == 0)
        {
            print("Player1");
            menu.transform.Find("Ready").gameObject.GetComponent<Button>().onClick.AddListener(FindObjectOfType<PlayerManeger>().Player1Ready);   
            FindObjectOfType<PlayerManeger>().player1ReadyText = menu.transform.Find("ReadyText").GetComponent<TextMeshProUGUI>();   
            playerText.GetComponent<TextMeshProUGUI>().text = "Player 1";
        }
        else
        {
            print("player2");
            menu.transform.Find("Ready").gameObject.GetComponent<Button>().onClick.AddListener(FindObjectOfType<PlayerManeger>().Player2Ready);
            FindObjectOfType<PlayerManeger>().player2ReadyText = menu.transform.Find("ReadyText").GetComponent<TextMeshProUGUI>();
            playerText.GetComponent<TextMeshProUGUI>().text = "Player 2";
        }
    }
}
