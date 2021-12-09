using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerManeger : MonoBehaviour
{
    public int nextPlayerNum;
    public bool playerFull, player1Ready, player2Ready;
    public GameObject firstButton;
    public Slider ammoPlayer1, ammoPlayer2;
    private GameObject player1, player2;
    public PlayerInputManager playerInputManager;
    public TextMeshProUGUI player1ReadyText, player2ReadyText;

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
        playerCountCheck();
    }
    public void playerJoin(PlayerInput player)
    {
        player.gameObject.GetComponent<PlayerHealth>().playerInt = nextPlayerNum;
        
        if (player.playerIndex == 0)
        {
            player1 = player.gameObject;
            player1.GetComponent<PlayerCombat>().ammoSlider = ammoPlayer1;
        }
        else
        {
            player2 = player.gameObject;
            player2.GetComponent<PlayerCombat>().ammoSlider = ammoPlayer2;
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
