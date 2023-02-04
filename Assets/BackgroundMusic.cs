using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = menuMusic;
        source.Play();
    }

    public void ChageToGameMusic()
    {
        source.clip = gameMusic;
        source.Play();
    }
}
