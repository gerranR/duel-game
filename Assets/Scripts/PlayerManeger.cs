using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManeger : MonoBehaviour
{
    public int nextPlayerNum;

    public void playerleave(PlayerInput player)
    {
        nextPlayerNum--;
    }
    public void playerJoin(PlayerInput player)
    {
        player.gameObject.GetComponent<PlayerHealth>().playerInt = nextPlayerNum;
        nextPlayerNum++;
    }
}
