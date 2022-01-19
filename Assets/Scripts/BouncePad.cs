using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float knockbackForce;
    public GameObject source;
    public AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            collision.gameObject.GetComponent<PlayerHealth>().Knockback(source, knockbackForce);
        }
    }
}
