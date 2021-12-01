using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackObj : MonoBehaviour
{
    public void GetKncokback(GameObject other, float knockbackForce)
    {
        Vector2 direction = other.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + -direction.x * knockbackForce, GetComponent<Rigidbody2D>().velocity.y + -direction.y * knockbackForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WorldBorderRight" || collision.gameObject.tag == "WorldBorderLeft" || collision.gameObject.tag == "WorldBorderDown" || collision.gameObject.tag == "WorldBorderUp")
        {
            Destroy(gameObject);
        }
    }
}
