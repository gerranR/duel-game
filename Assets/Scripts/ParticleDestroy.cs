using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", 3); 
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
