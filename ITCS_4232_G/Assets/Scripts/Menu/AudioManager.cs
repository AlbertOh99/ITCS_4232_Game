using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }
    private AudioSource source;


    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void play(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

}
