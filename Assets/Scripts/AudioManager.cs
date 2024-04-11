using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource Sfx;

    public static AudioManager instance{  get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;

        Music.clip = clip;
        Music.loop = loop;
        Music.Play();
    }

    public void PlaySfx(AudioClip clip, bool loop = false)
    {
        if (clip == null) return;

        Sfx.clip = clip;
        Sfx.loop = loop;
        Sfx.Play();
    }
}
