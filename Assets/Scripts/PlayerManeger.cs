using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManeger : MonoBehaviour
{
    public int nextPlayerNum;
    public bool playerFull, player1Ready, player2Ready;
    public PlayerInputManager playerInputManager;

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
            player1Ready = false;
        else
            player1Ready = true;
    }
    public void Player2Ready()
    {
        if (player2Ready)
            player2Ready = false;
        else
            player2Ready = true;
    }
}
