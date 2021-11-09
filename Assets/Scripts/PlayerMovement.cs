using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius;
    public float speed, jumpForce, resistance;
    public Rigidbody2D rigidbody2d;
    public int jumpsLeft;
    public int jumpsMax;
    public GameObject groundCheckObj;
    public bool hasKnockback;

    private void Awake()
    {
        jumpsLeft = jumpsMax;
    }

    private void Update()
    {
        movement();
    }

    void movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rigidbody2d.velocity = new Vector2(speed * Time.deltaTime, rigidbody2d.velocity.y);
        }
        else if(Input.GetAxisRaw("Horizontal") != 0)
        {
            rigidbody2d.velocity = new Vector2(-speed * Time.deltaTime, rigidbody2d.velocity.y);
        }
        else
        {
            turnMovement(false);
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            jumpsLeft -= 1;
        }
        if(Input.GetButtonUp("Jump") && hasKnockback == false)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapCircle(groundCheckObj.transform.position , groundCheckRadius).tag == "Ground")
        {
            jumpsLeft = jumpsMax; 
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
