using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButtonAssign : MonoBehaviour
{
    public GameObject imagePos1, imagePos2, imagePos3, imagePos4, imagePos5, pannel;
    public TextMeshProUGUI titel1, titel2, titel3, titel4, titel5, discription1, discription2, discription3, discription4, discription5;
    public Button button1, button2, button3, button4, button5;
    private CardSelect cardSelect;

    private void Awake()
    {
        cardSelect = FindObjectOfType<CardSelect>();

        cardSelect.imagePos[0] = imagePos1;
        cardSelect.imagePos[1] = imagePos2;
        cardSelect.imagePos[2] = imagePos3;
        cardSelect.imagePos[3] = imagePos4;
        cardSelect.imagePos[4] = imagePos5;

        cardSelect.titel[0] = titel1;
        cardSelect.discription[0] = discription1;
        cardSelect.titel[1] = titel2;
        cardSelect.discription[1] = discription2;
        cardSelect.titel[2] = titel3;
        cardSelect.discription[2] = discription3;
        cardSelect.titel[3] = titel4;
        cardSelect.discription[3] = discription4;
        cardSelect.titel[4] = titel5;
        cardSelect.discription[4] = discription5;


        this.transform.Find("card1").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.ChangeStats(0); });
        this.transform.Find("card2").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.ChangeStats(1); });
        this.transform.Find("card3").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.ChangeStats(2); });
        this.transform.Find("card4").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.ChangeStats(3); });
        this.transform.Find("card5").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.ChangeStats(4); });

        cardSelect.curentPanel = pannel;

    }
}
