using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerManeger : MonoBehaviour
{
    public int nextPlayerNum;
    public bool playerFull, player1Ready, player2Ready;
    public GameObject firstButton;
    private GameObject player1, player2;
    public PlayerInputManager playerInputManager;
    public TextMeshProUGUI player1ReadyText, player2ReadyText, player1JoinText, player2JoinText;
    public InputSystemUIInputModule multiplayerPlayer1, multiplayerPlayer2;

    private void Update()
    {
        if(playerFull == true && playerInputManager.joiningEnabled == true)
        {
            DisableJoining();
        }
        else if (playerFull == false && !playerInputManager.joiningEnabled == false)
        {
            EnableJoining();
        }

        if(player1Ready && player2Ready)
        {
            FindObjectOfType<GameManeger>().startGame();
        }
    }

    public void playerleave(PlayerInput player)
    {
        nextPlayerNum--;
        if (nextPlayerNum == 0)
        {
            player1JoinText.text = "empty";
        }
        else
        {
            player2JoinText.text = "empty";
        }
        playerCountCheck();
    }
    public void playerJoin(PlayerInput player)
    {
        player.gameObject.GetComponent<PlayerHealth>().playerInt = nextPlayerNum;
        
        if (player.playerIndex == 0)
        {
            player.uiInputModule = multiplayerPlayer1;
            player1 = player.gameObject;
        }
        else
        {
            player.uiInputModule = multiplayerPlayer2;
            player2 = player.gameObject;
        }
        if (nextPlayerNum == 0)
        {
            player1JoinText.text = "player 1";
        }
        else
        {
            player2JoinText.text = "player 2";
        }
        playerCountCheck();
        nextPlayerNum++;
    }


    public void playerCountCheck()
    {
        if (playerInputManager.playerCount == playerInputManager.maxPlayerCount)
        {
            playerFull = true;
        }
        else
            playerFull = false;
    }

    public void EnableJoining()
    {
        playerInputManager.EnableJoining();
    }
    public void DisableJoining()
    {
        playerInputManager.DisableJoining();
    }

    public void Player1Ready()
    {
        if (player1Ready)
        {
            player1Ready = false;
            player1ReadyText.text = " not Ready ";
        }
        else
        {
            player1Ready = true;
            player1ReadyText.text = " Ready ";
        }
    }
    public void Player2Ready()
    {
        if (player2Ready)
        {
            player2Ready = false;
            player2ReadyText.text = " not Ready ";
        }
        else
        {
            player2Ready = true;
            player2ReadyText.text = " Ready ";
        }
    }

    public void FirstButton()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
