using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip hurt;
    public AudioClip dead;
    public AudioClip commonCoin;
    public AudioClip specialCoin;
    public AudioClip passLevel;
    public AudioClip GetStar;

    [HideInInspector]
    public float bgmVolume = 0.6f;
    [HideInInspector]
    public float effectVolume = 1;

    private AudioSource bgmSource;

    public void Play(AudioClip clip, AudioSource source,bool isBGM = false)
    {
        source.clip = clip;

        if(isBGM)
        {
            source.volume = bgmVolume;
            if (bgmSource == null)
                bgmSource = source;            
        }
        else
            source.volume = effectVolume;

        source.Play();
    }

    private void Update()
    {
        bgmSource.volume = bgmVolume;
    }
}
