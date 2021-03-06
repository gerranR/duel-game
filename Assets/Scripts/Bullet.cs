using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rigidbody2d;
    public float bulletSpeed, dmg, knockbackForce, poisonDmg, poisonTime, fireDmg, fireTime, numOfBounces, bounceForce, reverseControleTime;
    public GameObject player, bomb, trampoline, slowZone;
    public bool poison, fire, shotgun, hasReverseControles;

    public ParticleSystem reflectPart;
    public AudioSource bulletBounceSound;

    // Start is called before the first frame update
    void Start()
    {
            rigidbody2d.AddRelativeForce(new Vector2(bulletSpeed, rigidbody2d.velocity.y));
    }
    private void Update()
    {
        if(FindObjectOfType<CardSelect>().cardsOnScreen)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "WorldBorderRight" || collision.gameObject.tag == "WorldBorderLeft" || collision.gameObject.tag == "WorldBorderDown" || collision.gameObject.tag == "WorldBorderUp")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            numOfBounces--;
            if (numOfBounces <= 0)
            {
                Destroy(gameObject);
            }
            bulletBounceSound.Play();
            if (player.GetComponent<PlayerCombat>().bombOnHit)
            {
                Instantiate(bomb, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerCombat>().trampolineOnhit && collision.gameObject.layer == 3)
            { 
                Vector3 diffrence = collision.contacts[0].normal;
                diffrence.Normalize();
                float rotationZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
                Instantiate(trampoline, transform.position, rotation);
            }
            if (player.GetComponent<PlayerCombat>().slowzoneOnHit)
            {
                Instantiate(slowZone, transform.position, transform.rotation);
            }

            else
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                GetComponent<Rigidbody2D>().AddForce(Vector2.Reflect(dir, collision.contacts[0].normal) * bounceForce);
            }
        }
        if (collision.transform.tag == "Player")
        {
            if (collision.gameObject != player)
            {
                if (player.GetComponent<PlayerCombat>().bombOnHit)
                {
                    Instantiate(bomb, transform.position, transform.rotation);
                }
                if (poison)
                {
                    collision.gameObject.GetComponent<PlayerHealth>().poisoned = poison;
                    collision.gameObject.GetComponent<PlayerHealth>().PoisonDmg = poisonDmg;
                    collision.gameObject.GetComponent<PlayerHealth>().poisonTime = poisonTime;
                }
                if (fire)
                {
                    print("fire");
                    collision.gameObject.GetComponent<PlayerHealth>().fireDmg = fireDmg;
                    collision.gameObject.GetComponent<PlayerHealth>().fireTime = fireTime;
                    collision.gameObject.GetComponent<PlayerHealth>().fire = fire;

                }
                if(hasReverseControles)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().reverseControles = hasReverseControles;
                    collision.gameObject.GetComponent<PlayerMovement>().reverseControleTime = reverseControleTime;
                    collision.gameObject.GetComponent<PlayerMovement>().StartCountdownReverseControles();
                }
                collision.gameObject.GetComponent<PlayerHealth>().DoDmg(dmg - collision.gameObject.GetComponent<PlayerHealth>().rangeResist);
                collision.gameObject.GetComponent<PlayerHealth>().Knockback(this.gameObject, knockbackForce);


                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "BreakableObj")
        {
            if (player.GetComponent<PlayerCombat>().bombOnHit)
            {
                Instantiate(bomb, transform.position, transform.rotation);
            }
            collision.gameObject.GetComponent<KnockbackObj>().GetKncokback(this.gameObject, knockbackForce, dmg);
        }


        if (collision.transform.tag == "Sword")
        {
            if (collision.gameObject.GetComponentInParent<PlayerHealth>().hasLifeSteal)
            {
                collision.gameObject.GetComponentInParent<PlayerHealth>().healPart.Play();
                collision.gameObject.GetComponentInParent<PlayerHealth>().health += collision.gameObject.GetComponentInParent<PlayerHealth>().lifeStealAmount;
            }
            if (collision.gameObject.GetComponentInParent<PlayerHealth>().bulletReflect)
            {
                reflectPart.Play();
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                GetComponent<Rigidbody2D>().AddForce(dir * collision.gameObject.GetComponentInParent<PlayerHealth>().bulletReturnSpeed);
            }
            else
            {
                Destroy(gameObject);
            }

        }


    }
}
