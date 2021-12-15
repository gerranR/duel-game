using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] bgMusic;
    public AudioSource musicSource;

    private void Start()
    {
        StartRandomMusic();
    }

    void Update()
    {
        if(!musicSource.isPlaying)
        {
            StartRandomMusic();
        }
    }

    public void StartRandomMusic()
    {
        var curClip = Random.Range(0, bgMusic.Length);
        musicSource.clip = bgMusic[curClip];
        musicSource.Play();
    }
}
