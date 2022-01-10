using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public float targetAngle ,speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * targetAngle; //tweak this to change frequency

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
