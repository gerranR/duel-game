using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerInt;
    public float health;
    public Slider hpSlider;
    public GameObject cardScreen;

    private void Awake()
    {
        cardScreen = FindObjectOfType<GameManeger>().cardScreen;
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
            cardScreen.SetActive(true);
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
            DoDmg(collision.transform.GetComponentInParent<PlayerCombat>().swordDmg);
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
