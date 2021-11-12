using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    public int cardSelected;
    public GameObject player1, player2;
    public void SelectCard(int card)
    {
        cardSelected = card;
        print(cardSelected);
        player1.GetComponent<PlayerCombat>().CanAttack(true);
        player2.GetComponent<PlayerCombat>().CanAttack(true);
    }
}
