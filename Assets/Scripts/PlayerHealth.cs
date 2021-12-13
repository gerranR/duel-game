using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int playerInt;
    public float health, maxHealth, rangeResist, meleeResist, borderKnockbackForce;
    public Slider hpSlider;
    private GameObject cardScreen, firstButton;
    public bool canTakeDmg = true;
    public bool spawnedCard, someoneWon;
    public GameObject CardPanelPrefab, deathPart, arm, hair;
    public float knockbackForce, knockbackCount, knockBackLenght;
    ContactPoint2D[] contactPoints;

    public AudioSource playerDeathAudioSource, hitAudio;

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
            hitAudio.Play();
            if (health <= 0.00001)
            {
                if (someoneWon == false)
                {
                    GetComponent<Animator>().SetFloat("Speed", 0);
                    GetComponent<Animator>().SetBool("Grounded", true);
                    GetComponent<Animator>().SetBool("Jump", false);
                    playerDeathAudioSource.Play();
                    FindObjectOfType<GameManeger>().player2.GetComponent<PlayerHealth>().canTakeDmg = false;
                    FindObjectOfType<GameManeger>().player1.GetComponent<PlayerHealth>().canTakeDmg = false;
                    arm.SetActive(false);
                    hpSlider.gameObject.SetActive(false);
                    hair.SetActive(false);
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    GetComponent<SpriteRenderer>().enabled = false;
                    GameObject deathPartical = Instantiate(deathPart, this.gameObject.transform);
                    GetComponent<PlayerMovement>().TurnMovement(false);
                    FindObjectOfType<GameManeger>().winText.SetActive(true);
                    if(playerInt == 1)
                        FindObjectOfType<GameManeger>().winText.GetComponent<TextMeshProUGUI>().text = "player 1 has won";
                    else
                        FindObjectOfType<GameManeger>().winText.GetComponent<TextMeshProUGUI>().text = "player 2 has won";
                    StartCoroutine(deathTime());
                }
            }
        }
    }

    IEnumerator deathTime()
    {
        yield return new WaitForSeconds(3f);

            var rootMenu = GameObject.Find("CardPanel");
            if (rootMenu != null && spawnedCard == false)
            {
                FindObjectOfType<GameManeger>().winText.SetActive(false);
                rootMenu.SetActive(true);
                var menu = Instantiate(CardPanelPrefab, rootMenu.transform);
                this.GetComponent<PlayerInput>().uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
                EventSystem.current = FindObjectOfType<MultiplayerEventSystem>();
                GetComponent<SpriteRenderer>().enabled = true;
                arm.SetActive(true);
                hpSlider.gameObject.SetActive(true);
                hair.SetActive(true);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                FindObjectOfType<CardSelect>().playerLost = this.gameObject;
                FindObjectOfType<CardSelect>().ChangeCards(playerInt + 1);
                spawnedCard = true;
                FindObjectOfType<GameManeger>().ResetLevel(playerInt, true, menu);
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
        this.GetComponent<PlayerMovement>().hasKnockback = true;
        Vector2 direction = other.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + - direction.x * knockbackForce, GetComponent<Rigidbody2D>().velocity.y + -direction.y * knockbackForce);
        StartCoroutine(NoKnockback());
    }

    public void BorderKnockback(float knockbackForce, int borderNum)
    {
        if(borderNum == 1)
        {
            this.GetComponent<PlayerMovement>().hasKnockback = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + knockbackForce, GetComponent<Rigidbody2D>().velocity.y);
            StartCoroutine(NoKnockback());
        }        if(borderNum == 0)
        {
            this.GetComponent<PlayerMovement>().hasKnockback = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + -knockbackForce, GetComponent<Rigidbody2D>().velocity.y);
            StartCoroutine(NoKnockback());
        }        if(borderNum == 3)
        {
            this.GetComponent<PlayerMovement>().hasKnockback = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y + knockbackForce);
            StartCoroutine(NoKnockback());
        }        if(borderNum == 2)
        {
            this.GetComponent<PlayerMovement>().hasKnockback = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y+ -knockbackForce);
            StartCoroutine(NoKnockback());
        }
    }


    IEnumerator NoKnockback()
    {
        yield return new WaitForSeconds(.3f);
        this.GetComponent<PlayerMovement>().hasKnockback = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WorldBorderRight")
        {
            DoDmg(50);
            BorderKnockback(borderKnockbackForce, 0);
        }        if (collision.gameObject.tag == "WorldBorderLeft")
        {
            DoDmg(50);
            BorderKnockback(borderKnockbackForce, 1);
        }        if (collision.gameObject.tag == "WorldBorderUp")
        {
            DoDmg(50);
            BorderKnockback(borderKnockbackForce, 2);
        }        if (collision.gameObject.tag == "WorldBorderDown")
        {
            DoDmg(50);
            BorderKnockback(borderKnockbackForce, 3);
        }

    }
}
