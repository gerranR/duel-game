using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public float speed = 5, knockbackForce, moveSpeed;
    public bool moving;
    public GameObject[] checkpoints;
    private int targetCheckpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, checkpoints[targetCheckpoint].transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == checkpoints[targetCheckpoint].transform.position)
            {
                if (targetCheckpoint < checkpoints.Length-1)
                {
                    targetCheckpoint += 1;
                }
                else
                {
                    targetCheckpoint = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().DoDmg(50);
            collision.gameObject.GetComponent<PlayerHealth>().Knockback(this.gameObject, knockbackForce);
        }
    }
}
