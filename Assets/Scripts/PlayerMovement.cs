using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundCheckRadius, bulletSpeed, fireRate,raycastDist;
    public float speed, jumpForce, resistance, wallResistance;
    public Rigidbody2D rigidbody2d;
    public int jumpsLeft, lorRWall, ammo;
    public int jumpsMax, maxAmmo;
    public GameObject groundCheckObj, wallCheckObjR, wallCheckObjL, gun, bullet;
    public bool hasKnockback, wallJumpCheck;
    public LayerMask groundLayer;
    public PhysicsMaterial2D playerMat;
    private bool canMove = true, canShoot = true, jumped;

    private void Awake()
    {
        jumpsLeft = jumpsMax;
    }

    private void FixedUpdate()
    {
        movement();
        Combat();
        GroundCheck();
    }

    void Combat()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.forward * bulletSpeed);
            canShoot = false;
            StartCoroutine(ShootTimer());
        }
    }
    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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
        if (Input.GetButtonDown("Jump") && jumpsLeft != 0 && jumped == false)
        {
            if (wallJumpCheck)
            {
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
                jumped = false;
                jumpsLeft--;
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                print(jumpsLeft);
                StartCoroutine(JumpTimer());
            }
        }
        if (Input.GetButtonUp("Jump") && hasKnockback == false)
        {
            if (rigidbody2d.velocity.y >= -1)
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -1);
            }
        }

        if(wallJumpCheck)
        {
            if (rigidbody2d.velocity.y <= 0 && Input.GetAxisRaw("Horizontal") != 0)
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y / 5);
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
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDist, groundLayer);
        Debug.DrawRay(transform.position, -Vector2.up, Color.green);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Ground" && jumped == false)
            {
                wallJumpCheck = false;
                jumpsLeft = jumpsMax;
                hit = new RaycastHit2D();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(wallCheckObjR.transform.position , groundCheckRadius, groundLayer))
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


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
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
}
