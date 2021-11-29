using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class PlayerHealth : MonoBehaviour
{
    public int playerInt;
    public float health, maxHealth, rangeResist, meleeResist;
    public Slider hpSlider;
    private GameObject cardScreen, firstButton;
    public bool canTakeDmg = true;
    public bool spawnedCard, someoneWon;
    public GameObject CardPanelPrefab;
    public float knockbackForce, knockbackCount, knockBackLenght;

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
        hpSlider.maxValue = maxHealth;
    }

    public void DoDmg(float dmg)
    {
        if (canTakeDmg)
        {
            health -= dmg;
            if (health <= 0.00001)
            {
                if(GetComponent<PlayerInput>().playerIndex == 0)
                {
                    FindObjectOfType<GameManeger>().ResetLevel  (0);
                }
                else
                { 
                    FindObjectOfType<GameManeger>().ResetLevel(1);
                }

                if (someoneWon == false)
                {
                    var rootMenu = GameObject.Find("CardPanel");
                    if (rootMenu != null && spawnedCard == false)
                    {
                        rootMenu.SetActive(true);
                        var menu = Instantiate(CardPanelPrefab, rootMenu.transform);
                        this.GetComponent<PlayerInput>().uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
                        EventSystem.current = FindObjectOfType<MultiplayerEventSystem>();
                        FindObjectOfType<CardSelect>().playerLost = this.gameObject;
                        FindObjectOfType<CardSelect>().ChangeCards();
                        spawnedCard = true;
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Sword")
        {
            DoDmg(collision.transform.GetComponentInParent<PlayerCombat>().swordDmg - meleeResist);
        }
    }

    public void Knockback(GameObject other, float knockbackForce)
    {
        //Vector2 direction = other.transform.up - transform.position;
        //print(other.transform.up);
        //print(direction);
        //GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        //print(other.GetComponent<Rigidbody2D>().velocity * knockbackForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "WorldBorder")
        {
            DoDmg(maxHealth);
            //Knockback(collision.gameObject, 30000);
        }
    }
}
