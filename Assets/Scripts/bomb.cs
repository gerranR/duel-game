using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public Collider2D collider;
    public float dmg, knockbackForce, countdown;
    public GameObject explosion;
    public AudioSource explosionSound;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(countdown);
        collider.enabled = true;
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(explosion, transform);
        explosionSound.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().DoDmg(dmg - collision.GetComponent<PlayerHealth>().rangeResist);
            collision.GetComponent<PlayerHealth>().Knockback(this.gameObject, knockbackForce);
        }

        if (collision.gameObject.tag == "BreakableObj")
        {
            collision.gameObject.GetComponent<KnockbackObj>().GetKncokback(this.gameObject, knockbackForce, dmg);
        }
    }
}
