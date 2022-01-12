using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackObj : MonoBehaviour
{
    private float hp;
    public GameObject fx, fxPrefab;
    public void GetKncokback(GameObject other, float knockbackForce, float dmg)
    {
        Vector2 direction = other.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + -direction.x * knockbackForce, GetComponent<Rigidbody2D>().velocity.y + -direction.y * knockbackForce);
        hp -= dmg;
        if(hp < 0.00001)
        {
            fx = Instantiate(fxPrefab, transform.position, transform.rotation);
            //fx.GetComponent<CrateParticles>().Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WorldBorderRight" || collision.gameObject.tag == "WorldBorderLeft" || collision.gameObject.tag == "WorldBorderDown" || collision.gameObject.tag == "WorldBorderUp")
        {
            fx = Instantiate(fxPrefab, transform.position, transform.rotation);
            //fx.GetComponent<CrateParticles>().Play();
            Destroy(gameObject);
        }
    }
}
