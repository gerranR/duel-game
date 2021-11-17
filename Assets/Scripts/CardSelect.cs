using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    public GameObject player1, player2, playerLost;

    public Cards[] cardRarety1, cardRarety2, cardRarety3;
    public Button cardButton1, cardButton2, cardButton3, cardButton4, cardButton5;
    public Cards[] card;

    public void ChangeCards()
    {
        for (int i = 0; i < card.Length; i++)
        {
            float r = Random.Range(0, 10);

            if (r == 0)
            {
                card[i] = cardRarety3[Random.Range(0, cardRarety3.Length)];
            }
            else if (r <= 3)
            {
                card[i] = cardRarety2[Random.Range(0, cardRarety2.Length)];
            }
            else
            {
                card[i] = cardRarety1[Random.Range(0, cardRarety1.Length)];
            }
        }
        //cardButton1.image.sprite = card[0].cardImage;
        //cardButton2.image.sprite = card[1].cardImage;
        //cardButton3.image.sprite = card[2].cardImage;
        //cardButton4.image.sprite = card[3].cardImage;
        //cardButton5.image.sprite = card[4].cardImage;
    }

    public void changeStats(int buttonPressed)
    {
        player1.GetComponent<PlayerCombat>().CanAttack(true);
        player2.GetComponent<PlayerCombat>().CanAttack(true);
        playerLost.GetComponent<PlayerCombat>().fireRate += card[buttonPressed].fireRate;
        playerLost.GetComponent<PlayerCombat>().maxAmmo += card[buttonPressed].maxAmmo; 
        playerLost.GetComponent<PlayerHealth>().maxHealth += card[buttonPressed].maxHP; 
        playerLost.GetComponent<PlayerHealth>().meleeResist += card[buttonPressed].meleeResistance; 
        playerLost.GetComponent<PlayerHealth>().rangeResist += card[buttonPressed].rangeResistance; 
        playerLost.GetComponent<PlayerCombat>().swordDmg += card[buttonPressed].meleeDmg; 
        playerLost.GetComponent<PlayerCombat>().bulletDmg += card[buttonPressed].rangeDmg; 
        playerLost.GetComponent<PlayerMovement>().speed += card[buttonPressed].speed;
        playerLost.GetComponent<PlayerMovement>().jumpsMax += card[buttonPressed].maxJump;
        GetComponent<GameManeger>().gameStarted = false;
        GetComponent<GameManeger>().startGame();
        if(card[buttonPressed].halfHPDubbelDmg)
        {
            playerLost.GetComponent<PlayerHealth>().maxHealth = playerLost.GetComponent<PlayerHealth>().maxHealth / 2;
            playerLost.GetComponent<PlayerCombat>().swordDmg += playerLost.GetComponent<PlayerCombat>().swordDmg;
            playerLost.GetComponent<PlayerCombat>().bulletDmg += playerLost.GetComponent<PlayerCombat>().bulletDmg;
        }
    }
}
