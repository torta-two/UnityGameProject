using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;

    public AudioClip BGM;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip hurt;
    public AudioClip dead;
    public AudioClip commonCoin;
    public AudioClip specialCoin;
    public AudioClip passLevel;
    public AudioClip GetStar;

    private float volume = 1;
    private float firstVolume;

    private void Start()
    {
        volume = GetComponent<Ctrl>().volume;
        firstVolume = BGMSource.volume;
    }

    public void Play(AudioClip clip, AudioSource source)
    {
        source.clip = clip;

        if (source != BGMSource)
        {            
            source.volume = volume;
        }

        source.Play();
    }

    private void Update()
    {
        volume = GetComponent<Ctrl>().volume;

        BGMSource.volume = volume * firstVolume;
    }
    
}
