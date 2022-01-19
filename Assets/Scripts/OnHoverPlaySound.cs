using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverPlaySound : MonoBehaviour
{
    public AudioSource cardFlipAudio;

    public void playAudio()
    {
        cardFlipAudio.Play();
    }
}
