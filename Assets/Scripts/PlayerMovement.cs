using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius;
    public float speed, jumpForce, resistance, wallResistance;
    public Rigidbody2D rigidbody2d;
    private int jumpsLeft;
    public int jumpsMax;
    public GameObject groundCheckObj, wallCheckObjR, wallCheckObjL;
    public bool hasKnockback, wallJumpCheck;
    public LayerMask groundLayer;
    public PhysicsMaterial2D playerMat;

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
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rigidbody2d.velocity = new Vector2((speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
        }
        else if(Input.GetAxisRaw("Horizontal") != 0)
        {
            rigidbody2d.velocity = new Vector2((-speed / resistance) * Time.deltaTime, rigidbody2d.velocity.y);
        }
        else
        {
            turnMovement(false);
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            if (wallJumpCheck)
            {
                rigidbody2d.velocity = new Vector2((rigidbody2d.velocity.x + jumpForce) + -rigidbody2d.velocity.x, jumpForce);
            }
            else
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                jumpsLeft -= 1;
            }
        }
        if (Input.GetButtonUp("Jump") && hasKnockback == false)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(groundCheckObj.transform.position , groundCheckRadius, groundLayer))
        {
            jumpsLeft = jumpsMax; 
        }
        else if (Physics2D.OverlapCircle(wallCheckObjR.transform.position , groundCheckRadius, groundLayer))
        {
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
     
            playerMat.friction = wallResistance;
        }
        else if (Physics2D.OverlapCircle(wallCheckObjL.transform.position , groundCheckRadius, groundLayer))
        {
            jumpsLeft = jumpsMax;
            wallJumpCheck = true;
            playerMat.friction = wallResistance;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            playerMat.friction = 0;
            wallJumpCheck = false;
        }
    }

    public void turnMovement(bool canMovement)
    {
        if(!hasKnockback)
        {
            if(canMovement)
            {

            }
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }
}
