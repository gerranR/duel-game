using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public int player1Wins, player2Wins;
    public GameObject[] levels;
    private GameObject curLvl, curLvlObj;
    public GameObject cardScreen, player1, player2, playerSelectScreen, firstButton;
    public Transform lvlStorePos, playPos, spawnPos1, spawnPos2;
    public bool gameStarted;

    private void Awake()
    {
        curLvl = levels[Random.Range(0, levels.Length)];

        curLvlObj = Instantiate(curLvl, playPos.position, playPos.rotation);
    }

    public void ResetLevel(int playerWin)
    {
        if (playerWin == 0)
            player2Wins++;
        if (playerWin == 1)
            player1Wins++;
        Destroy(curLvlObj);
        int newLvl = Random.Range(0, levels.Length);
        curLvlObj = Instantiate(levels[newLvl], playPos.position, playPos.rotation);
        resetPlayers();
        curLvl = levels[newLvl];
    }

    public void resetPlayers()
    {
        player1.transform.position = spawnPos1.position;
        player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
        player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        player1.GetComponent<PlayerCombat>().CanAttack(false);
        player1.GetComponent<PlayerMovement>().TurnMovement(false);
        player2.transform.position = spawnPos2.position;
        player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
        player2.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        player2.GetComponent<PlayerCombat>().CanAttack(false);
        player2.GetComponent<PlayerMovement>().TurnMovement(false);


    }

    public void startGame()
    {
        if (!gameStarted)
        {
            player1.transform.position = spawnPos1.position;
            player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
            player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player1.GetComponent<PlayerCombat>().CanAttack(true);
            player1.GetComponent<PlayerMovement>().TurnMovement(true);
            player2.transform.position = spawnPos2.position;
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player2.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerCombat>().CanAttack(true);
            player2.GetComponent<PlayerMovement>().TurnMovement(true);
            playerSelectScreen.SetActive(false);
            gameStarted = true;
        }
    }
}
