using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughFloor : MonoBehaviour
{
    public float speed, delay;
    public GameObject[] sides;
    public LayerMask playermask;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            foreach (GameObject side in sides)
            {
                side.GetComponent<FallThroughDoor>().triggered = true;
            }
        }
    }
}
