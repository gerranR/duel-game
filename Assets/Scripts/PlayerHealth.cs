using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHealth : MonoBehaviour
{
    public int playerInt;
    public float health, maxHealth, rangeResist, meleeResist;
    public Slider hpSlider;
    private GameObject cardScreen, firstButton;

    private void Awake()
    {
        cardScreen = FindObjectOfType<GameManeger>().cardScreen;
        firstButton = FindObjectOfType<GameManeger>().firstButton;
        if (playerInt == 0)
        {
            FindObjectOfType<GameManeger>().player1 = gameObject;
            FindObjectOfType<CardSelect>().player1 = gameObject;
        }
        else
        {
            FindObjectOfType<GameManeger>().player2 = gameObject;
            FindObjectOfType<CardSelect>().player2 = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = health;
    }

    public void DoDmg(float dmg)
    {
        health -= dmg;
        if(health <= 0.00001)
        {
            FindObjectOfType<CardSelect>().playerLost = this.gameObject;
            FindObjectOfType<CardSelect>().ChangeCards();
            cardScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstButton);
            if (playerInt == 0)
                FindObjectOfType<GameManeger>().ResetLevel(1);
            else
                FindObjectOfType<GameManeger>().ResetLevel(0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Sword")
        {
            DoDmg(collision.transform.GetComponentInParent<PlayerCombat>().swordDmg - meleeResist);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "WorldBorder")
        {
            DoDmg(100);
        }
    }
}
