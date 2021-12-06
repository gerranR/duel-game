using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManeger : MonoBehaviour
{
    public float fireRate, maxHP, meleeDmg, rangeDmg, speed, meleeRange, meleeResistance, rangeResistance;
    public int maxAmmo, maxJump;
    public int player1Wins, player2Wins;
    public GameObject[] levels;
    private GameObject curLvl, curLvlObj, menu;
    public GameObject cardScreen, player1, player2, playerSelectScreen, firstButton, winScreen, winText;
    public Transform playPos, spawnPos1, spawnPos2;
    public bool gameStarted;
    public GameObject firstSelectWinScreen;
    public TextMeshProUGUI winScreentext, roundWinText;
    public InputSystemUIInputModule baseEventSystem1, baseEventSystem2;

    private void Awake()
    {
        curLvl = levels[Random.Range(0, levels.Length)];

        curLvlObj = Instantiate(curLvl, playPos.position, playPos.rotation);
    }

    public void ResetLevel(int playerWin)
    {
        if (playerWin == 0)
        {
            player1Wins++;
        }
        if (playerWin == 1)
        {
            player2Wins++;
        }
        if (player1Wins == 5 || player2Wins == 5)
        {
            var rootMenu = GameObject.Find("WinPanel");
            if (rootMenu != null)
            {
                player1.GetComponent<PlayerHealth>().someoneWon = true;
                player2.GetComponent<PlayerHealth>().someoneWon = true;
                rootMenu.SetActive(true);
                menu = Instantiate(winScreen, rootMenu.transform);
                player1.GetComponent<PlayerInput>().uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
                EventSystem.current = FindObjectOfType<MultiplayerEventSystem>();
                print(menu.transform.Find("Rematch").name   );
                menu.transform.Find("Rematch").GetComponent<Button>().onClick.AddListener(delegate { this.Rematch(); });
                menu.transform.Find("MainMenu").GetComponent<Button>().onClick.AddListener(delegate { this.MainMenu(); });
            }
            //if (player1Wins == 5)
            //{
            //    winScreentext.text = "player 2 Won";
            //}
            //else
            //    winScreentext.text = "player 1 won";
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
        SceneManager.LoadScene(1);
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
        player1.GetComponent<PlayerMovement>().TurnMovement(true);
        player2.GetComponent<PlayerMovement>().TurnMovement(true);
        player1.GetComponent<PlayerCombat>().canAttack = true;
        player2.GetComponent<PlayerCombat>().canAttack = true;
        player1.GetComponent<PlayerHealth>().someoneWon = false;
        player2.GetComponent<PlayerHealth>().someoneWon = false;
        player1.GetComponent<PlayerHealth>().canTakeDmg = true;
        player2.GetComponent<PlayerHealth>().canTakeDmg = true;
        Invoke("winScreenActive", 0.1f);
    }

    private void winScreenActive()
    {
        Destroy(menu);
    }

    public void resetPlayers()
    {
        if (player2 != null)
        {
            player1.transform.position = spawnPos1.position;
            player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
            player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player1.GetComponent<PlayerCombat>().CanAttack(false);
            player1.GetComponent<PlayerHealth>().canTakeDmg = false;
            player1.GetComponent<PlayerMovement>().TurnMovement(false);
            player2.transform.position = spawnPos2.position;
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerCombat>().CanAttack(false);
            player1.GetComponent<PlayerHealth>().canTakeDmg = false;
            player2.GetComponent<PlayerMovement>().TurnMovement(false);

        }
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
            player1.GetComponent<PlayerHealth>().canTakeDmg = true;
            player2.transform.position = spawnPos2.position;
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player2.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerCombat>().CanAttack(true);
            player2.GetComponent<PlayerCombat>().canShoot = true;
            player2.GetComponent<PlayerMovement>().TurnMovement(true);
            player2.GetComponent<PlayerHealth>().canTakeDmg = true;
            GameObject[] playerJoinArray = GameObject.FindGameObjectsWithTag("PlayerJoin");
            for (int i = 0; i < playerJoinArray.Length; i++)
            {
                Destroy(playerJoinArray[i]);
            }
            
            playerSelectScreen.SetActive(false);
            gameStarted = true;
        }
    }
}
