using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    public GameObject playerReadyPanel;

    [SerializeField]
    private int maxPlayers = 2;

    public static PlayerConfigurationManager instace { get; private set; }

    private void Awake()
    {
        if(instace != null)
        {
            Debug.Log("SINGLETON - trying to create another instance of singleton!!");
        }
        else
        {
            instace = this;
            DontDestroyOnLoad(instace);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void setPlayerColour(int index, Material colour)
    {
        playerConfigs[index].playerMaterial = colour;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if(playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.isReady ==  true))
        {
            playerReadyPanel.SetActive(false); 
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }
    public PlayerInput input { get; set; }

    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public Material playerMaterial { get; set; }
}
