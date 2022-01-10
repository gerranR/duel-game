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
            playerLost.GetComponent<PlayerCombat>().maxAmmo += card[buttonPressed].maxAmmo;
            playerLost.GetComponent<PlayerHealth>().maxHealth += card[buttonPressed].maxHP;
            playerLost.GetComponent<PlayerHealth>().meleeResist += card[buttonPressed].meleeResistance;
            playerLost.GetComponent<PlayerHealth>().rangeResist += card[buttonPressed].rangeResistance;
            playerLost.GetComponent<PlayerCombat>().swordDmg += card[buttonPressed].meleeDmg;
            playerLost.GetComponent<PlayerCombat>().bulletDmg += card[buttonPressed].rangeDmg;
            playerLost.GetComponent<PlayerCombat>().numOfBulletBounce += card[buttonPressed].bulletBounces;
            playerLost.GetComponent<PlayerMovement>().speed += card[buttonPressed].speed;
            playerLost.GetComponent<PlayerMovement>().jumpsMax += card[buttonPressed].maxJump;
            GetComponent<GameManeger>().gameStarted = false;
            GetComponent<GameManeger>().startGame();
            if(card[buttonPressed].burst)
            {
                playerLost.GetComponent<PlayerCombat>().burst = true;
                playerLost.GetComponent<PlayerCombat>().burstMax += card[buttonPressed].burstNum;
            }
            if (card[buttonPressed].poison)
            {
                playerLost.GetComponent<PlayerCombat>().poison = true;
                playerLost.GetComponent<PlayerCombat>().poisonDmg += card[buttonPressed].poisonDmg;
                playerLost.GetComponent<PlayerCombat>().poisonTime += card[buttonPressed].poisonTime;
            }
            if (card[buttonPressed].fire)
            {
                playerLost.GetComponent<PlayerCombat>().fire = true;
                playerLost.GetComponent<PlayerCombat>().fireDmg += card[buttonPressed].fireDmg;
                playerLost.GetComponent<PlayerCombat>().fireTime += card[buttonPressed].fireTime;
            }
            if (card[buttonPressed].shotgun)
            {
                playerLost.GetComponent<PlayerCombat>().hasShotgun = true;
                playerLost.GetComponent<PlayerCombat>().shotgunShots += card[buttonPressed].shotgunShots;
            }
            if (card[buttonPressed].halfHPDubbelDmg)
            {
                playerLost.GetComponent<PlayerHealth>().maxHealth = playerLost.GetComponent<PlayerHealth>().maxHealth / 2;
                playerLost.GetComponent<PlayerCombat>().swordDmg += playerLost.GetComponent<PlayerCombat>().swordDmg;
                playerLost.GetComponent<PlayerCombat>().bulletDmg += playerLost.GetComponent<PlayerCombat>().bulletDmg;
            }
            if (card[buttonPressed].lifeSteal)
            {
                playerLost.GetComponent<PlayerHealth>().hasLifeSteal = true;
                playerLost.GetComponent<PlayerHealth>().lifeStealAmount += card[buttonPressed].lifeStealAmount;
            }
            if (card[buttonPressed].bomb)
            {
                playerLost.GetComponent<PlayerCombat>().bombOnHit = true;
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
            }
            if (playerLost.GetComponent<PlayerInput>().playerIndex == 0)
            {
                player1Cards.Add(card[buttonPressed]);
                player2.GetComponent<PlayerHealth>().knockbackAdd += card[buttonPressed].knaockback;
                player1.GetComponent<PlayerHealth>().canTakeDmg = true;
            }
            else
            {
                player2Cards.Add(card[buttonPressed]);
                player1.GetComponent<PlayerHealth>().knockbackAdd += card[buttonPressed].knaockback;
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
