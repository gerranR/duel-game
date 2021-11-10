using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius;
    public float speed, jumpForce, resistance, wallResistance;
    public Rigidbody2D rigidbody2d;
    private int jumpsLeft, lorRWall;
    public int jumpsMax;
    public GameObject groundCheckObj, wallCheckObjR, wallCheckObjL;
    public bool hasKnockback, wallJumpCheck;
    public LayerMask groundLayer;
    public PhysicsMaterial2D playerMat;
    private bool canMove = true;

    private void Awake()
    {
        jumpsLeft = jumpsMax;
    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                rigidbody2d.velocity = new Vector2((speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
            }
            else if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rigidbody2d.velocity = new Vector2((-speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
            }
            else if(!hasKnockback)
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            if (wallJumpCheck)
            {
                TurnMovement(false);
                if(lorRWall == 0)
                {
                    rigidbody2d.velocity = new Vector2(5, jumpForce);
                }
                else if(lorRWall == 1)
                {
                    rigidbody2d.velocity = new Vector2(-5, jumpForce);
                }
                StartCoroutine(WallJumpTimer());
                jumpsLeft -= 1;
                wallJumpCheck = false;
            }
            else
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                jumpsLeft -= 1;
            }
        }
        if (Input.GetButtonUp("Jump") && hasKnockback == false)
        {
            if(rigidbody2d.velocity.y >= -1)
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
        }
    }

    IEnumerator WallJumpTimer()
    {
        yield return new WaitForSeconds(0.2f);
        TurnMovement(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(groundCheckObj.transform.position , groundCheckRadius, groundLayer))
        {
            wallJumpCheck = false;
            jumpsLeft = jumpsMax;
        }
        else if (Physics2D.OverlapCircle(wallCheckObjR.transform.position , groundCheckRadius, groundLayer))
        {
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 0;
        }
        else if (Physics2D.OverlapCircle(wallCheckObjL.transform.position , groundCheckRadius, groundLayer))
        {
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            lorRWall = 1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            if(rigidbody2d.velocity.y <= 0 && Input.GetAxisRaw("Horizontal") != 0)
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y / 5);
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
}
