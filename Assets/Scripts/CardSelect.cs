using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using TMPro;

public class CardSelect : MonoBehaviour
{
    public GameObject player1, player2, playerLost, imagePos1, imagePos2, imagePos3, imagePos4, imagePos5;
    public TextMeshProUGUI titel1, titel2, titel3, titel4, titel5, discription1, discription2, discription3, discription4, discription5;
    public MultiplayerEventSystem eventSystemPlayer1, eventSystemPlayer2;
    public Cards[] cardRarety1, cardRarety2, cardRarety3;
    public Button cardButton1, cardButton2, cardButton3, cardButton4, cardButton5;
    public Cards[] card;
    public List<Cards> player1Cards, player2Cards;
    public GameObject cardScreen, canvasPlayer1, canvasPlayer2;

    public void ChangeCards()
    {
        if (playerLost.GetComponent<PlayerInput>().playerIndex == 0)
        {
            eventSystemPlayer1.playerRoot = cardScreen;
        }
        else
        {
            eventSystemPlayer1.playerRoot = cardScreen;
        }
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
        if (playerLost.GetComponent<PlayerInput>().playerIndex == 0)
        {
            player1Cards.Add(card[buttonPressed]);
        }
        else
        {
            player2Cards.Add(card[buttonPressed]);
        }
    }
}
