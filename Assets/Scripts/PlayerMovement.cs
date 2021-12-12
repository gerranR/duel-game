using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius, bulletSpeed, raycastDist;
    public float speed, jumpForce, resistance, wallResistance;
    public Rigidbody2D rigidbody2d;
    private int jumpsLeft, lorRWall;
    public int jumpsMax, maxAmmo;
    public GameObject groundCheckObj, wallCheckObjR, wallCheckObjL, arm;
    public bool hasKnockback, wallJumpCheck;
    public LayerMask groundLayer, wallLayer;
    public PhysicsMaterial2D playerMat;
    private bool canMove = false, jumped;
    private float inputX;

    public AudioSource Footsteps;

    public Animator playerAnimator;

    private void Awake()
    {
        jumpsLeft = jumpsMax;
    }

    private void FixedUpdate()
    {
        movement();
        GroundCheck();
    }

    public void move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    void movement()
    {

        if (canMove && !hasKnockback)
        {
            if (inputX != 0)
            {
                rigidbody2d.velocity = new Vector2(inputX * (speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
                playerAnimator.SetFloat("Speed", speed);

                if(inputX > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if(inputX < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
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
        if (context.performed && jumpsLeft > 0 && jumped == false && canMove)
        {
            if (wallJumpCheck)
            {
                playerAnimator.SetBool("Hanging", false);
                playerAnimator.SetBool("Jump", true);
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
                jumped = true;
                jumpsLeft--;
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                playerAnimator.SetBool("Jump", true);
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
        yield return new WaitForSeconds(0.3f);
        jumped = false;
        playerAnimator.SetBool("Jump", false);
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDist, groundLayer);
        Debug.DrawRay(transform.position, -Vector2.up, Color.green);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Ground" || hit.transform.tag == "BreakableObj" && jumped == false)
            {
                playerAnimator.SetBool("Grounded", true);
                wallJumpCheck = false;
                jumpsLeft = jumpsMax;
                hit = new RaycastHit2D();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(wallCheckObjR.transform.position , groundCheckRadius, wallLayer))
        {
            playerAnimator.SetBool("Hanging", true);
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 0;
        }
        else if (Physics2D.OverlapCircle(wallCheckObjL.transform.position , groundCheckRadius, wallLayer))
        {
            playerAnimator.SetBool("Hanging", true);
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 1;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            playerAnimator.SetBool("Hanging", false);
            wallJumpCheck = false;
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
}
