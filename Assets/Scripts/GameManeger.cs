using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManeger : MonoBehaviour
{
    public float fireRate, maxHP, meleeDmg, rangeDmg, speed, meleeRange, meleeResistance, rangeResistance;
    public int maxAmmo, maxJump;
    public int player1Wins, player2Wins;
    public GameObject[] levels;
    private GameObject curLvl, curLvlObj;
    public GameObject cardScreen, player1, player2, playerSelectScreen, playerSelectScreen1, playerSelectScreen2, firstButton, winScreen;
    public Transform lvlStorePos, playPos, spawnPos1, spawnPos2;
    public bool gameStarted;
    public GameObject firstSelectWinScreen;

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

        if (player1Wins == 5 || player2Wins == 5)
        {
            winScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelectWinScreen);
        }
        else
        {
            Destroy(curLvlObj);
            int newLvl = Random.Range(0, levels.Length);
            curLvlObj = Instantiate(levels[newLvl], playPos.position, playPos.rotation);
            resetPlayers();
            curLvl = levels[newLvl];
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Rematch()
    {
        player1Wins = 0;
        player2Wins = 0;
        player1.GetComponent<PlayerCombat>().fireRate = fireRate;
        player1.GetComponent<PlayerCombat>().maxAmmo = maxAmmo;
        player1.GetComponent<PlayerHealth>().maxHealth = maxHP;
        player1.GetComponent<PlayerHealth>().meleeResist = 0f;
        player1.GetComponent<PlayerHealth>().rangeResist = 0f;
        player1.GetComponent<PlayerCombat>().swordDmg = meleeDmg;
        player1.GetComponent<PlayerCombat>().bulletDmg = rangeDmg;
        player1.GetComponent<PlayerMovement>().speed = speed;
        player1.GetComponent<PlayerMovement>().jumpsMax = maxJump;
        player2.GetComponent<PlayerCombat>().fireRate = fireRate;
        player2.GetComponent<PlayerCombat>().maxAmmo = maxAmmo;
        player2.GetComponent<PlayerHealth>().maxHealth = maxHP;
        player2.GetComponent<PlayerHealth>().meleeResist = meleeResistance;
        player2.GetComponent<PlayerHealth>().rangeResist = rangeResistance;
        player2.GetComponent<PlayerCombat>().swordDmg = meleeDmg;
        player2.GetComponent<PlayerCombat>().bulletDmg = rangeDmg;
        player2.GetComponent<PlayerMovement>().speed = speed;
        player2.GetComponent<PlayerMovement>().jumpsMax = maxJump;
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
        if (!gameStarted && player2 != null)
        {
            player1.transform.position = spawnPos1.position;
            player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
            player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player1.GetComponent<PlayerCombat>().CanAttack(true);
            player1.GetComponent<PlayerCombat>().canShoot = true;
            player1.GetComponent<PlayerMovement>().TurnMovement(true);
            player2.transform.position = spawnPos2.position;
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player2.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerCombat>().CanAttack(true);
            player2.GetComponent<PlayerCombat>().canShoot = true;
            player2.GetComponent<PlayerMovement>().TurnMovement(true);
            playerSelectScreen.SetActive(false);
            playerSelectScreen1.SetActive(false);
            playerSelectScreen2.SetActive(false);
            gameStarted = true;
        }
    }
}
