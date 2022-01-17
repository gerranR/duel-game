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
    public GameObject[] levels, player1WinCounter, player2WinCounter;
    private GameObject curLvl, curLvlObj, menu;
    public GameObject cardScreen, player1, player2, playerSelectScreen, firstButton, winScreen, winText;
    public Transform playPos, spawnPos1, spawnPos2;
    public bool gameStarted;
    public GameObject firstSelectWinScreen;
    public TextMeshProUGUI winScreentext, roundWinText;
    public InputSystemUIInputModule baseEventSystem1, baseEventSystem2;
    public Animator pannelAnimator1, pannelAnimator2;
    public GameObject countdown;

    private void Awake()
    {
        curLvl = levels[Random.Range(0, levels.Length)];

        curLvlObj = Instantiate(curLvl, playPos.position, playPos.rotation);
    }

    public void ResetLevel(int playerWin, bool cardSelect, GameObject curCardPanel)
    {
        if (playerWin == 0)
        {
            player1WinCounter[player1Wins].SetActive(true);
            player1Wins++;
        }
        if (playerWin == 1)
        {
            player2WinCounter[player2Wins].SetActive(true);
            player2Wins++;
        }
        if (player1Wins == 5 || player2Wins == 5)
        {
            var rootMenu = GameObject.Find("WinPanel");
            if (rootMenu != null)
            {
                Destroy(curCardPanel);
                player1.GetComponent<PlayerHealth>().someoneWon = true;
                player2.GetComponent<PlayerHealth>().someoneWon = true;
                player1.GetComponent<PlayerCombat>().swordCol.GetComponent<Collider2D>().enabled = false;
                player1.GetComponent<PlayerCombat>().swordUsed = false;
                player2.GetComponent<PlayerCombat>().swordCol.GetComponent<Collider2D>().enabled = false;
                player2.GetComponent<PlayerCombat>().swordUsed = false;
                rootMenu.SetActive(true);
                menu = Instantiate(winScreen, rootMenu.transform);
                EventSystem.current = FindObjectOfType<MultiplayerEventSystem>();
                menu.transform.GetChild(0).Find("Rematch").GetComponent<Button>().onClick.AddListener(delegate { this.Rematch(); });
                menu.transform.GetChild(0).transform.Find("MainMenu").GetComponent<Button>().onClick.AddListener(delegate { this.MainMenu(); });
                if (player1Wins == 5)
                {
                    menu.transform.GetChild(0).transform.Find("WinText").GetComponent<TextMeshProUGUI>().text = "player 2 Won";
                }
                else
                    menu.transform.GetChild(0).transform.Find("WinText").GetComponent<TextMeshProUGUI>().text = "player 1 won";
            }
         }
        else
        {
            Invoke("ChangeLVL", 1f);
        }

    }

    public void ChangeLVL()
    {
        Destroy(curLvlObj);
        int newLvl = Random.Range(0, levels.Length);
        if(levels[newLvl] == curLvl)
        {
            ChangeLVL();
            return;
        }
        curLvlObj = Instantiate(levels[newLvl], playPos.position, playPos.rotation);
        resetPlayers();
        curLvl = levels[newLvl];
        spawnPos1 = curLvl.transform.Find("Spawn1");
        player1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player1.transform.position = spawnPos1.position;
        spawnPos2 = curLvl.transform.Find("Spawn2");
        if(player2 != null)
        {
            player2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            player2.transform.position = spawnPos2.position;
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
        player1.GetComponent<PlayerHealth>().spawnedCard = false;
        player2.GetComponent<PlayerHealth>().spawnedCard = false;
        for (int i = 0; i < player1WinCounter.Length; i++)
        {
            player1WinCounter[i].SetActive(false);
        }
        for (int i = 0; i < player2WinCounter.Length; i++)
        {
            player2WinCounter[i].SetActive(false);
        }

        player1.GetComponent<PlayerCombat>().fireRate = fireRate;
        player1.GetComponent<PlayerCombat>().maxAmmo = maxAmmo;
        player1.GetComponent<PlayerHealth>().maxHealth = maxHP;
        player1.GetComponent<PlayerHealth>().meleeResist = meleeResistance;
        player1.GetComponent<PlayerHealth>().rangeResist = rangeResistance;
        player1.GetComponent<PlayerCombat>().swordDmg = meleeDmg;
        player1.GetComponent<PlayerCombat>().bulletDmg = rangeDmg;
        player1.GetComponent<PlayerCombat>().numOfBulletBounce = 0f;
        player1.GetComponent<PlayerMovement>().speed = speed;
        player1.GetComponent<PlayerMovement>().jumpsMax = maxJump;
        player1.GetComponent<PlayerCombat>().burst = false;
        player1.GetComponent<PlayerCombat>().burstMax = 0;
        player1.GetComponent<PlayerCombat>().poison = false; ;
        player1.GetComponent<PlayerCombat>().poisonDmg = 0f;
        player1.GetComponent<PlayerCombat>().poisonTime = 0f;
        player1.GetComponent<PlayerCombat>().fire = false;
        player1.GetComponent<PlayerCombat>().fireDmg = 0f;
        player1.GetComponent<PlayerCombat>().fireTime = 0f;
        player1.GetComponent<PlayerCombat>().hasShotgun = false;
        player1.GetComponent<PlayerCombat>().shotgunShots = 0f;
        player1.GetComponent<PlayerHealth>().hasLifeSteal = false;
        player1.GetComponent<PlayerHealth>().lifeStealAmount = 0f;
        player1.GetComponent<PlayerCombat>().bombOnHit = false;
        player1.GetComponent<PlayerCombat>().hasReverseControles = false;
        player1.GetComponent<PlayerCombat>().reverseControleTime = 0f;
        player1.GetComponent<PlayerCombat>().slowzoneOnHit = false;
        player1.GetComponent<PlayerCombat>().trampolineOnhit = false;
        player1.GetComponent<PlayerHealth>().bulletReflect = false;
        player1.GetComponent<PlayerHealth>().bulletReturnSpeed = 0f;
        player1.GetComponent<PlayerHealth>().knockbackAdd = 0f;

        player2.GetComponent<PlayerCombat>().fireRate = fireRate;
        player2.GetComponent<PlayerCombat>().maxAmmo = maxAmmo;
        player2.GetComponent<PlayerHealth>().maxHealth = maxHP;
        player2.GetComponent<PlayerHealth>().meleeResist = meleeResistance;
        player2.GetComponent<PlayerHealth>().rangeResist = rangeResistance;
        player2.GetComponent<PlayerCombat>().swordDmg = meleeDmg;
        player2.GetComponent<PlayerCombat>().bulletDmg = rangeDmg;
        player2.GetComponent<PlayerCombat>().numOfBulletBounce = 0f;
        player2.GetComponent<PlayerMovement>().speed = speed;
        player2.GetComponent<PlayerMovement>().jumpsMax = maxJump;
        player2.GetComponent<PlayerCombat>().burst = false;
        player2.GetComponent<PlayerCombat>().burstMax = 0;
        player2.GetComponent<PlayerCombat>().poison = false; ;
        player2.GetComponent<PlayerCombat>().poisonDmg = 0f;
        player2.GetComponent<PlayerCombat>().poisonTime = 0f;
        player2.GetComponent<PlayerCombat>().fire = false;
        player2.GetComponent<PlayerCombat>().fireDmg = 0f;
        player2.GetComponent<PlayerCombat>().fireTime = 0f;
        player2.GetComponent<PlayerCombat>().hasShotgun = false;
        player2.GetComponent<PlayerCombat>().shotgunShots = 0f;
        player2.GetComponent<PlayerHealth>().hasLifeSteal = false;
        player2.GetComponent<PlayerHealth>().lifeStealAmount = 0f;
        player2.GetComponent<PlayerCombat>().bombOnHit = false;
        player2.GetComponent<PlayerCombat>().hasReverseControles = false;
        player2.GetComponent<PlayerCombat>().reverseControleTime = 0f;
        player2.GetComponent<PlayerCombat>().slowzoneOnHit = false;
        player2.GetComponent<PlayerCombat>().trampolineOnhit = false;
        player2.GetComponent<PlayerHealth>().bulletReflect = false;
        player2.GetComponent<PlayerHealth>().bulletReturnSpeed = 0f;
        player2.GetComponent<PlayerHealth>().knockbackAdd = 0f;

        Destroy(curLvlObj);
        int newLvl = Random.Range(0, levels.Length);
        curLvlObj = Instantiate(levels[newLvl], playPos.position, playPos.rotation);
        curLvl = levels[newLvl];
        spawnPos1 = curLvl.transform.Find("Spawn1");
        player1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player1.transform.position = spawnPos1.position;
        spawnPos2 = curLvl.transform.Find("Spawn2");
        player2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player2.transform.position = spawnPos2.position;
        StartCoroutine(winScreenActive());
    }

    IEnumerator winScreenActive()
    {
        player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
        player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
        player2.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
        yield return new WaitForSeconds(1f);
        Destroy(menu);
        resetPlayers();
        countdown.SetActive(true);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "3";
        yield return new WaitForSeconds(1f);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "2";
        yield return new WaitForSeconds(1f);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        yield return new WaitForSeconds(1f);
        countdown.GetComponentInChildren<TextMeshProUGUI>().text = "fight";
        yield return new WaitForSeconds(.5f);
        countdown.SetActive(false);

        player1.GetComponent<PlayerMovement>().TurnMovement(true);
        player2.GetComponent<PlayerMovement>().TurnMovement(true);
        player1.GetComponent<PlayerCombat>().canAttack = true;
        player2.GetComponent<PlayerCombat>().canAttack = true;
        player1.GetComponent<PlayerHealth>().someoneWon = false;
        player2.GetComponent<PlayerHealth>().someoneWon = false;
        player1.GetComponent<PlayerHealth>().canTakeDmg = true;
        player2.GetComponent<PlayerHealth>().canTakeDmg = true;
    }

    public void resetPlayers()
    {
        if (player2 != null)
        {
            player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
            player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player1.GetComponent<PlayerCombat>().CanAttack(false);
            player1.GetComponent<PlayerHealth>().canTakeDmg = false;
            player1.GetComponent<PlayerMovement>().TurnMovement(false);
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerCombat>().CanAttack(false);
            player1.GetComponent<PlayerHealth>().canTakeDmg = false;
            player2.GetComponent<PlayerMovement>().TurnMovement(false);

        }
    }

    public void startGame()
    {
        if (pannelAnimator2 != null)
        {
            pannelAnimator1.SetTrigger("Leave");
            pannelAnimator2.SetTrigger("Leave");
        }

        playerSelectScreen.GetComponent<Animator>().SetTrigger("Leave");

        StartCoroutine(StartGameDelay());
    }

    IEnumerator StartGameDelay()
    {
        if (!gameStarted && player2 != null)
        {
            player1.GetComponent<PlayerHealth>().health = player1.GetComponent<PlayerHealth>().maxHealth;
            player1.GetComponent<PlayerCombat>().ammo = player1.GetComponent<PlayerCombat>().maxAmmo;
            player2.GetComponent<PlayerHealth>().health = player2.GetComponent<PlayerHealth>().maxHealth;
            player2.GetComponent<PlayerCombat>().ammo = player2.GetComponent<PlayerCombat>().maxAmmo;
            yield return new WaitForSeconds(2f);    
            countdown.SetActive(true);
            countdown.GetComponentInChildren<TextMeshProUGUI>().text = "3";
            yield return new WaitForSeconds(1f);
            countdown.GetComponentInChildren<TextMeshProUGUI>().text = "2";
            yield return new WaitForSeconds(1f);
            countdown.GetComponentInChildren<TextMeshProUGUI>().text = "1";
            yield return new WaitForSeconds(1f);
            countdown.GetComponentInChildren<TextMeshProUGUI>().text = "fight";
            yield return new WaitForSeconds(.5f);
            countdown.SetActive(false);
            player1.GetComponent<PlayerCombat>().CanAttack(true);
            player1.GetComponent<PlayerCombat>().canShoot = true;
            player1.GetComponent<PlayerMovement>().TurnMovement(true);
            player1.GetComponent<PlayerHealth>().canTakeDmg = true;

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
