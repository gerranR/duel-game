using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButtonAssign : MonoBehaviour
{
    public GameObject imagePos1, imagePos2, imagePos3, imagePos4, imagePos5;
    public TextMeshProUGUI titel1, titel2, titel3, titel4, titel5, discription1, discription2, discription3, discription4, discription5;
    public Button button1, button2, button3, button4, button5;
    private CardSelect cardSelect;

    private void Awake()
    {
        cardSelect = FindObjectOfType<CardSelect>();

        cardSelect.imagePos1 = imagePos1;
        cardSelect.imagePos2 = imagePos2;
        cardSelect.imagePos3 = imagePos3;
        cardSelect.imagePos4 = imagePos4;
        cardSelect.imagePos5 = imagePos5;

        cardSelect.titel1 = titel1;
        cardSelect.discription1 = discription1;
        cardSelect.titel2 = titel2;
        cardSelect.discription2 = discription2;
        cardSelect.titel3 = titel3;
        cardSelect.discription3 = discription3;
        cardSelect.titel4 = titel4;
        cardSelect.discription4 = discription4;
        cardSelect.titel5 = titel5;
        cardSelect.discription5 = discription5;


        this.transform.Find("card1").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.changeStats(0); });
        this.transform.Find("card2").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.changeStats(1); });
        this.transform.Find("card3").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.changeStats(2); });
        this.transform.Find("card4").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.changeStats(3); });
        this.transform.Find("card5").GetComponent<Button>().onClick.AddListener(delegate { cardSelect.changeStats(4); });

    }
}
