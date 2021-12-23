using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public float timer, damage;
    private GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerHealth>().DoDmg(damage);
            player.GetComponent<PlayerMovement>().TurnMovement(false);
            animator.SetTrigger("BearTrapTrigger");
            Invoke("Wait", timer);
        }
    }
    
    public void Wait()
    {
        player.GetComponent<PlayerMovement>().TurnMovement(true);
    }
}
