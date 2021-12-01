using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;
    public float bulletSpeed, dmg, knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
         rigidbody2d.AddRelativeForce(new Vector2(bulletSpeed, rigidbody2d.velocity.y));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().DoDmg(dmg - collision.GetComponent<PlayerHealth>().rangeResist);
            collision.GetComponent<PlayerHealth>().Knockback(this.gameObject, knockbackForce);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "BreakableObj")
        {
            collision.gameObject.GetComponent<KnockbackObj>().GetKncokback(this.gameObject, knockbackForce);
        }


        if (collision.transform.tag =="Sword")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
