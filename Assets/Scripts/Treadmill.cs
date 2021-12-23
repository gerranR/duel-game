using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    public float speed, resistance, force;
    public bool right;

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().isOnTreadmil = true;
            if (collision.gameObject.GetComponent<PlayerMovement>().isMoving)
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().flipX == false)
                {
                    if (right)
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().speed = speed;
                    }
                    else
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().resistance = resistance;
                    }
                }
                else
                {
                    if (!right)
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().speed = speed;
                    }
                    else
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().resistance = resistance;
                    }
                }
            }
            else
            {
                if(right)
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(force, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                else
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-force, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<PlayerMovement>().speed = collision.gameObject.GetComponent<PlayerMovement>().walkspeed;
            collision.gameObject.GetComponent<PlayerMovement>().resistance = 1;
            collision.gameObject.GetComponent<PlayerMovement>().isOnTreadmil = false;
        }
    }
}
