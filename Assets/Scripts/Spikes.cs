using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float knockbackForce, damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().DoDmg(damage);
            collision.gameObject.GetComponent<PlayerHealth>().Knockback(this.gameObject, knockbackForce);
        }
    }
}
