using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public int player1Wins, player2Wins;
    public GameObject[] levels;
    private GameObject curLvl;
    public GameObject cardScreen, player1, player2;
    public Transform lvlStorePos, playPos, spawnPos1, spawnPos2;

    private void Awake()
    {
        curLvl = levels[Random.Range(0, levels.Length)];

        Instantiate(curLvl, playPos.position, playPos.rotation);
    }

    public void ResetLevel(int playerWin)
    {
        if (playerWin == 0)
            player2Wins++;
        if (playerWin == 1)
            player1Wins++;
        curLvl.transform.position = lvlStorePos.position;
        int newLvl = Random.Range(0, levels.Length);
        Instantiate(levels[newLvl], playPos.position, playPos.rotation);
        resetPlayers();
        curLvl = levels[newLvl];
    }

    public void resetPlayers()
    {
        player1.transform.position = spawnPos1.position;
        player1.GetComponent<PlayerHealth>().health = 100;
        player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        player1.GetComponent<PlayerCombat>().CanAttack(false);
        player2.transform.position = spawnPos2.position;
        player2.GetComponent<PlayerHealth>().health = 100;
        player2.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        player2.GetComponent<PlayerCombat>().CanAttack(false);

    }
}
