using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius, bulletSpeed, raycastDist, reverseControleTime;
    public float speed, walkspeed, jumpForce, resistance, wallResistance, jetPackTime, slowzoneSpeed;
    public Rigidbody2D rigidbody2d;
    public int jumpsLeft, lorRWall;
    public int jumpsMax, maxAmmo;
    public GameObject groundCheckObj, wallCheckObjR, wallCheckObjL, arm, hair, devTools;
    public bool hasKnockback, wallJumpCheck, isMoving, isOnTreadmil, hasJetPack, reverseControles;
    public LayerMask groundLayer, wallLayer;
    public PhysicsMaterial2D playerMat;
    private bool canMove = false, jumped;
    private float inputX;
    public GameObject armPos1, armPos2, hairPos1, hairPos2;

    public AudioSource Footsteps;
    public ParticleSystem confusionPart;

    public Animator playerAnimator;

    public PlayerInput input;


    private void Awake()
    {
        devTools = FindObjectOfType<GameManeger>().DevTools;
        jumpsLeft = jumpsMax;
        playerAnimator.SetBool("Hanging", false);
    }

    private void FixedUpdate()
    {
        movement();
        GroundCheck();
    }

    public void openDevTools(InputAction.CallbackContext callback)
    {
        if (callback.performed && GameObject.FindGameObjectWithTag("Devtools") == null)
        {
            var menu = Instantiate(devTools, GameObject.Find("DevToolsScript").transform);

            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            var cardsHolder = menu.transform.Find("Cards");
            menu.transform.Find("Player1").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().ChangePlayer(1); });
            menu.transform.Find("Player2").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().ChangePlayer(2); });
            cardsHolder.transform.Find("Card3").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(0); });
            cardsHolder.transform.Find("Card4").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(1); });
            cardsHolder.transform.Find("Card5").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(2); });
            cardsHolder.transform.Find("Card6").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(3); });
            cardsHolder.transform.Find("Card7").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(4); });
            cardsHolder.transform.Find("Card8").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(5); });
            cardsHolder.transform.Find("Card9").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(6); });
            cardsHolder.transform.Find("Card10").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(7); });
            cardsHolder.transform.Find("Card11").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(8); });
            cardsHolder.transform.Find("Card12").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(9); });
            cardsHolder.transform.Find("Card13").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(10); });
            cardsHolder.transform.Find("Card14").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(11); });
            cardsHolder.transform.Find("Card15").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(12); });
            cardsHolder.transform.Find("Card16").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(13); });
            cardsHolder.transform.Find("Card17").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(14); });
            cardsHolder.transform.Find("Card18").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(15); });
            cardsHolder.transform.Find("Card19").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(16); });
            cardsHolder.transform.Find("Card20").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(17); });
            cardsHolder.transform.Find("Card21").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(18); });
            cardsHolder.transform.Find("Card22").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(19); });
            cardsHolder.transform.Find("Card23").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(20); });
            cardsHolder.transform.Find("Card24").gameObject.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<DevTools>().addCard(21); });

            FindObjectOfType<GameManeger>().pannelAnimator1 = menu.GetComponent<Animator>(); devTools.SetActive(true);
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerMovement>().TurnMovement(false);
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerMovement>().TurnMovement(false);
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerCombat>().canAttack = false;
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerCombat>().canAttack = false;
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerHealth>().canTakeDmg = false;
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerHealth>().canTakeDmg = false;
        }

        else if(callback.performed)
        {
            Destroy(GameObject.FindGameObjectWithTag("Devtools"));
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerMovement>().TurnMovement(true);
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerMovement>().TurnMovement(true);
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerCombat>().canAttack = true;
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerCombat>().canAttack = true;
            FindObjectOfType<GameManeger>().player1.GetComponent<PlayerHealth>().canTakeDmg = true;
            FindObjectOfType<GameManeger>().player2.GetComponent<PlayerHealth>().canTakeDmg = true;
        }
    }
    public void CloseDevTools()
    {

    }

    public void StartCountdownReverseControles()
    {
        StartCoroutine(ReverseControleBack());
    }

    public IEnumerator ReverseControleBack()
    {
        yield return new WaitForSeconds(reverseControleTime);
        confusionPart.Stop();
        reverseControles = false;
    }

    public void move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    void movement()
    {

        if (canMove && !hasKnockback)
        {
            if(reverseControles && inputX != 0)
            {
                if(!confusionPart.isPlaying)
                    confusionPart.Play();
                rigidbody2d.velocity = new Vector2(-inputX * (speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
                isMoving = true;
                playerAnimator.SetFloat("Speed", speed);

                if (wallJumpCheck == false)
                {
                    if (inputX > 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                        arm.transform.position = armPos1.transform.position;
                        hair.transform.position = hairPos1.transform.position;
                    }
                    else if (inputX < 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                        arm.transform.position = armPos2.transform.position;
                        hair.transform.position = hairPos2.transform.position;
                    }
                }
            }
            else if (inputX != 0)
            {
                rigidbody2d.velocity = new Vector2(inputX * (speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
                isMoving = true;
                playerAnimator.SetFloat("Speed", speed);

                if (wallJumpCheck == false)
                {
                    if (inputX > 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                        arm.transform.position = armPos1.transform.position;
                        hair.transform.position = hairPos1.transform.position;
                    }
                    else if (inputX < 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                        arm.transform.position = armPos2.transform.position;
                        hair.transform.position = hairPos2.transform.position;
                    }
                }
            }
            else
            {
                isMoving = false;
                if(!isOnTreadmil)
                {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
                
                
                playerAnimator.SetFloat("Speed", 0);
            }
        }
        

        if(wallJumpCheck)
        {
            if (rigidbody2d.velocity.y <= 0 && inputX != 0)
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y / 5);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpsLeft > 0 && canMove)
        {
            if (wallJumpCheck)
            {
                playerAnimator.SetBool("Hanging", false);
                playerAnimator.SetTrigger("Jump");
                jumpsLeft--;
                TurnMovement(false);
                if(lorRWall == 0)
                {
                    rigidbody2d.velocity = new Vector2(2.5f, jumpForce);
                }
                else if(lorRWall == 1)
                {
                    rigidbody2d.velocity = new Vector2(-2.5f, jumpForce);
                }
                StartCoroutine(WallJumpTimer());

                wallJumpCheck = false;
            }
            else 
            {
                jumpsLeft--;
                jumped = true;
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                playerAnimator.SetTrigger("Jump");
                playerAnimator.SetBool("Grounded", false);
                StartCoroutine(JumpTimer());
            }
        }
       
        if (context.canceled && hasKnockback == false)
        {
            if (rigidbody2d.velocity.y >= -1)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
            }
        }
    }

    IEnumerator WallJumpTimer()
    {
        yield return new WaitForSeconds(0.2f);
        TurnMovement(true);
    }
    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(.2f);
        if (jumpsLeft < 0)
            jumpsLeft = 0;
           
        jumped = false;
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDist, groundLayer);
        Debug.DrawRay(transform.position, -Vector2.up, Color.green);
        if (hit.transform != null)
        {
            if ((hit.transform.tag == "Ground" || hit.transform.tag == "BreakableObj") && jumped == false)
            {
                playerAnimator.SetBool("Grounded", true);
                wallJumpCheck = false;
                jumpsLeft = jumpsMax;
                
            }
            hit = new RaycastHit2D();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(wallCheckObjR.transform.position , groundCheckRadius, wallLayer))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            arm.transform.position = armPos1.transform.position;
            hair.transform.position = hairPos1.transform.position;
            playerAnimator.SetBool("Hanging", true);
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 0;
        }
        else if (Physics2D.OverlapCircle(wallCheckObjL.transform.position , groundCheckRadius, wallLayer))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            arm.transform.position = armPos2.transform.position;
            hair.transform.position = hairPos2.transform.position;
            playerAnimator.SetBool("Hanging", true);
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 1;
        }

        if(collision.gameObject.layer == 9)
        {
            playerAnimator.SetBool("Grounded", true);
            wallJumpCheck = false;
            jumpsLeft = jumpsMax;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            playerAnimator.SetBool("Hanging", false);
            wallJumpCheck = false;
        }

        if (collision.gameObject.tag == "SlowZone")
        {
            speed = walkspeed;
        }

    }

    public void TurnMovement(bool canMovement)
    {
        if(!hasKnockback)
        {
            if(!canMovement)
            {
                canMove = canMovement;
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
            else
            {
                canMove = canMovement;
            }
        }
    }

    public void leave(InputAction.CallbackContext context)
    {
        FindObjectOfType<PlayerInputManager>().EnableJoining();
        Destroy(gameObject);
    }

    public void playWalkingSounds()
    {
        Footsteps.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SlowZone")
        {
            speed = slowzoneSpeed;
        }
    }
}
