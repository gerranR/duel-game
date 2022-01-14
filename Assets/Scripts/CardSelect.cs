using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using TMPro;

public class CardSelect : MonoBehaviour
{
    public GameObject player1, player2, playerLost, imagePos1, imagePos2, imagePos3, imagePos4, imagePos5, curentPanel;
    public TextMeshProUGUI titel1, titel2, titel3, titel4, titel5, discription1, discription2, discription3, discription4, discription5;
    public Cards[] cardRarety1, cardRarety2, cardRarety3;
    public Cards[] card;
    public List<Cards> player1Cards, player2Cards;
    private bool canPress = true;

    public void ChangeCards(int playerWon)
    {
        for (int i = 0; i < card.Length; i++)
        {
            float r = Random.Range(0, 10);

            if (r == 0)
            {
                card[i] = cardRarety3[Random.Range(0, cardRarety3.Length)];
            }
            else if (r <=3)
            {
                card[i] = cardRarety2[Random.Range(0, cardRarety2.Length)];
            }
            else
            {
                card[i] = cardRarety1[Random.Range(0, cardRarety1.Length)];
            }
        }

        Instantiate(card[0].cardImage, imagePos1.transform.position, imagePos1.transform.rotation, imagePos1.transform);
        Instantiate(card[1].cardImage, imagePos2.transform.position, imagePos2.transform.rotation, imagePos2.transform);
        Instantiate(card[2].cardImage, imagePos3.transform.position, imagePos3.transform.rotation, imagePos3.transform);
        Instantiate(card[3].cardImage, imagePos4.transform.position, imagePos4.transform.rotation, imagePos4.transform);
        Instantiate(card[4].cardImage, imagePos5.transform.position, imagePos5.transform.rotation, imagePos5.transform);

        titel1.text = card[0].titel;
        titel2.text = card[1].titel;
        titel3.text = card[2].titel;
        titel4.text = card[3].titel;
        titel5.text = card[4].titel;
        discription1.text = card[0].discription;
        discription2.text = card[1].discription;
        discription3.text = card[2].discription;
        discription4.text = card[3].discription;
        discription5.text = card[4].discription;
        if(titel1.text != card[0].titel)
        {
            Destroy(GameObject.Find("CardSelectMenu"));
            playerLost.GetComponent<PlayerHealth>().spawnedCard = false;
        }

        if (playerWon == 1)
        {
            GameObject.Find("PlayerChooseCard").GetComponent<TextMeshProUGUI>().text = "Player 1";
        }
        else
        {
            GameObject.Find("PlayerChooseCard").GetComponent<TextMeshProUGUI>().text = "Player 2";
        }
    }

    public void changeStats(int buttonPressed)
    {
        if (canPress)
        {
            playerLost.GetComponent<PlayerCombat>().fireRate += card[buttonPressed].fireRate;
            if(playerLost.GetComponent<PlayerCombat>().fireRate <= 0)
            {
                playerLost.GetComponent<PlayerCombat>().fireRate = 0.1f;
            }
            playerLost.GetComponent<PlayerCombat>().maxAmmo += card[buttonPressed].maxAmmo;
            if (playerLost.GetComponent<PlayerCombat>().maxAmmo <= 0)
            {
                playerLost.GetComponent<PlayerCombat>().maxAmmo = 1;
            }
            playerLost.GetComponent<PlayerHealth>().maxHealth += card[buttonPressed].maxHP;
            if (playerLost.GetComponent<PlayerHealth>().maxHealth <= 0)
            {
                playerLost.GetComponent<PlayerHealth>().maxHealth = 0.1f;
            }
            playerLost.GetComponent<PlayerHealth>().meleeResist += card[buttonPressed].meleeResistance;
            if (playerLost.GetComponent<PlayerHealth>().meleeResist <= 0)
            {
                playerLost.GetComponent<PlayerHealth>().meleeResist = 0.1f;
            }
            playerLost.GetComponent<PlayerHealth>().rangeResist += card[buttonPressed].rangeResistance;
            if (playerLost.GetComponent<PlayerHealth>().rangeResist <= 0)
            {
                playerLost.GetComponent<PlayerHealth>().rangeResist = 0.1f;
            }
            playerLost.GetComponent<PlayerCombat>().swordDmg += card[buttonPressed].meleeDmg;
            if (playerLost.GetComponent<PlayerCombat>().swordDmg <= 0)
            {
                playerLost.GetComponent<PlayerCombat>().swordDmg = 0.1f;
            }
            playerLost.GetComponent<PlayerCombat>().bulletDmg += card[buttonPressed].rangeDmg;
            if (playerLost.GetComponent<PlayerCombat>().bulletDmg <= 0)
            {
                playerLost.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
            }
            playerLost.GetComponent<PlayerCombat>().numOfBulletBounce += card[buttonPressed].bulletBounces;
            if (playerLost.GetComponent<PlayerCombat>().numOfBulletBounce <= 0)
            {
                playerLost.GetComponent<PlayerCombat>().numOfBulletBounce = 0f;
            }
            playerLost.GetComponent<PlayerMovement>().speed += card[buttonPressed].speed;
            if (playerLost.GetComponent<PlayerMovement>().speed <= 0)
            {
                playerLost.GetComponent<PlayerMovement>().speed = 0.1f;
            }
            playerLost.GetComponent<PlayerMovement>().jumpsMax += card[buttonPressed].maxJump;
            if (playerLost.GetComponent<PlayerMovement>().jumpsMax <= 0)
            {
                playerLost.GetComponent<PlayerMovement>().jumpsMax = 1;
            }
            GetComponent<GameManeger>().gameStarted = false;
            GetComponent<GameManeger>().startGame();
            if(card[buttonPressed].burst)
            {
                playerLost.GetComponent<PlayerCombat>().burst = true;
                playerLost.GetComponent<PlayerCombat>().burstMax += card[buttonPressed].burstNum;
                if (playerLost.GetComponent<PlayerCombat>().burstMax <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().burstMax = 0;
                }
            }
            if (card[buttonPressed].poison)
            {
                playerLost.GetComponent<PlayerCombat>().poison = true;
                playerLost.GetComponent<PlayerCombat>().poisonDmg += card[buttonPressed].poisonDmg;
                if (playerLost.GetComponent<PlayerCombat>().poisonDmg <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().poisonDmg = 0f;
                }
                playerLost.GetComponent<PlayerCombat>().poisonTime += card[buttonPressed].poisonTime;
                if (playerLost.GetComponent<PlayerCombat>().poisonTime <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().poisonTime = 0f;
                }
            }
            if (card[buttonPressed].fire)
            {
                playerLost.GetComponent<PlayerCombat>().fire = true;
                playerLost.GetComponent<PlayerCombat>().fireDmg += card[buttonPressed].fireDmg;
                if (playerLost.GetComponent<PlayerCombat>().fireDmg <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().fireDmg = 0f;
                }
                playerLost.GetComponent<PlayerCombat>().fireTime += card[buttonPressed].fireTime;
                if (playerLost.GetComponent<PlayerCombat>().fireTime <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().fireTime = 0f;
                }
            }
            if (card[buttonPressed].shotgun)
            {
                playerLost.GetComponent<PlayerCombat>().hasShotgun = true;
                playerLost.GetComponent<PlayerCombat>().shotgunShots += card[buttonPressed].shotgunShots;
                if (playerLost.GetComponent<PlayerCombat>().shotgunShots <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().shotgunShots = 0f;
                }
            }
            if (card[buttonPressed].halfHPDubbelDmg)
            {
                playerLost.GetComponent<PlayerHealth>().maxHealth = playerLost.GetComponent<PlayerHealth>().maxHealth / 2;
                if (playerLost.GetComponent<PlayerHealth>().maxHealth <= 0)
                {
                    playerLost.GetComponent<PlayerHealth>().maxHealth = 1f;
                }
                playerLost.GetComponent<PlayerCombat>().swordDmg += playerLost.GetComponent<PlayerCombat>().swordDmg;
                if (playerLost.GetComponent<PlayerCombat>().swordDmg <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().swordDmg = 0.1f;
                }
                playerLost.GetComponent<PlayerCombat>().bulletDmg += playerLost.GetComponent<PlayerCombat>().bulletDmg;
                if (playerLost.GetComponent<PlayerCombat>().bulletDmg <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
                }
            }
            if (card[buttonPressed].lifeSteal)
            {
                playerLost.GetComponent<PlayerHealth>().hasLifeSteal = true;
                playerLost.GetComponent<PlayerHealth>().lifeStealAmount += card[buttonPressed].lifeStealAmount;
                if (playerLost.GetComponent<PlayerHealth>().lifeStealAmount <= 0)
                {
                    playerLost.GetComponent<PlayerHealth>().lifeStealAmount = 0f;
                }
            }
            if (card[buttonPressed].bomb)
            {
                playerLost.GetComponent<PlayerCombat>().bombOnHit = true;
            }
            if (card[buttonPressed].reverseControle)
            {
                playerLost.GetComponent<PlayerCombat>().hasReverseControles = true;
                playerLost.GetComponent<PlayerCombat>().reverseControleTime += card[buttonPressed].reverseControleTime;
                if (playerLost.GetComponent<PlayerCombat>().reverseControleTime <= 0)
                {
                    playerLost.GetComponent<PlayerCombat>().reverseControleTime = 0f;
                }
            }
            if (card[buttonPressed].slowzone)
            {
                playerLost.GetComponent<PlayerCombat>().slowzoneOnHit = true;
            }
            if (card[buttonPressed].trampoline)
            {
                playerLost.GetComponent<PlayerCombat>().trampolineOnhit = true;
            }
            if (card[buttonPressed].reflect)
            {
                playerLost.GetComponent<PlayerHealth>().bulletReflect = true;
                playerLost.GetComponent<PlayerHealth>().bulletReturnSpeed += card[buttonPressed].bulletReflectSpeed;
                if (playerLost.GetComponent<PlayerHealth>().bulletReturnSpeed <= 0)
                {
                    playerLost.GetComponent<PlayerHealth>().bulletReturnSpeed = 0.1f;
                }
            }
            if (playerLost.GetComponent<PlayerInput>().playerIndex == 0)
            {
                player1Cards.Add(card[buttonPressed]);
                player2.GetComponent<PlayerHealth>().knockbackAdd += card[buttonPressed].knaockback;
                if (playerLost.GetComponent<PlayerHealth>().knockbackAdd <= 0)
                {
                    playerLost.GetComponent<PlayerHealth>().knockbackAdd = 1;
                }
                player1.GetComponent<PlayerHealth>().canTakeDmg = true;
            }
            else
            {
                player2Cards.Add(card[buttonPressed]);
                player1.GetComponent<PlayerHealth>().knockbackAdd += card[buttonPressed].knaockback;
                if (playerLost.GetComponent<PlayerHealth>().knockbackAdd <= 0)
                {
                    playerLost.GetComponent<PlayerHealth>().knockbackAdd = 1f;
                }
                player2.GetComponent<PlayerHealth>().canTakeDmg = true;
            }
            playerLost.GetComponent<PlayerHealth>().spawnedCard = false;

            canPress = false;
            curentPanel.GetComponent<Animator>().SetTrigger("leaveCard");
            Invoke("buttonDelay", 2f);
        }
    }

    private void buttonDelay()
    {
        print(curentPanel);
        Destroy(curentPanel);
        player1.GetComponent<PlayerCombat>().CanAttack(true);
        player2.GetComponent<PlayerCombat>().CanAttack(true);
        canPress = true;
    }
}
