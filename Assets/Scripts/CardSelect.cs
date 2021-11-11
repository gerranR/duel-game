using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    public int cardSelected;
    public void SelectCard(int card)
    {
        cardSelected = card;
        print(cardSelected);
    }
}
