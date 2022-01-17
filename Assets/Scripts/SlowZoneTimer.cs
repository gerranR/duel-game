using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZoneTimer : MonoBehaviour
{
    private float timer;
    public float time;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= time)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), Time.deltaTime * 5);
        if (transform.localScale == new Vector3(0,0,0))
            Destroy(gameObject);
    }
}
