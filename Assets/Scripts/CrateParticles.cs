using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateParticles : MonoBehaviour
{
    public ParticleSystem[] particles;
    public void Play()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
            Invoke("Death", 2);
        }

    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
