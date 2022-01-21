using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThroughDoor : MonoBehaviour
{
    [HideInInspector] public float speed, delay;
    [HideInInspector] public bool triggered;
    public float targetRot;
    private float startRot;
    private bool moving;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        startRot = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        print(triggered);
        if (moving)
        {
            TriggerFloor();
        }
        else if (triggered)
        {
            Invoke("TriggerFloor", delay);
        }
    }

    public void TriggerFloor()
    {
        audioSource.Play();
        if (targetRot > 180f)
        {
            if (transform.rotation.eulerAngles.z > targetRot)
            {
                transform.Rotate(0, 0, -speed * Time.deltaTime);
            }
            else
            {
                moving = false;
            }
        }
        else if (transform.rotation.eulerAngles.z < targetRot)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            moving = false;
        }
    }
}
