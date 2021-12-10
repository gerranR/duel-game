using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    public float force;
    public bool right;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().TurnMovement(false);
        }

        if (right)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
        }
        else
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-force, 0), ForceMode2D.Impulse);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            StartCoroutine(turnPlayerMove(collision.gameObject));
    }


    IEnumerator turnPlayerMove(GameObject player)
    {
        yield return new WaitForSeconds(.5f);
        player.GetComponent<PlayerMovement>().TurnMovement(true);
    }
}
