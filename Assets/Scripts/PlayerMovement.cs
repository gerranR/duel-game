using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius;
    public float speed, jumpForce;
    public Rigidbody2D rigidbody2d;
    private int jumpsLeft;
    public int jumpsMax;
    public GameObject groundCheckObj;

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
        float mH = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(mH * speed * Time.deltaTime, rigidbody2d.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            jumpsLeft -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapCircle(groundCheckObj.transform.position , groundCheckRadius).tag == "Ground")
        {
            jumpsLeft = jumpsMax; 
        }
    }
}
