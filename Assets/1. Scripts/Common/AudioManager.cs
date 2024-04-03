using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] bgmSounds, sfxSounds;
    public AudioSource bgmSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } //½Ì±ÛÅæ

    private void Start()
    {
        PlayBGM("Intro");
    }


    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgmSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            bgmSource.clip = s.clip;
            bgmSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void BGMVolume(float volume)
    {
       bgmSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void StopBGM()
    {
        bgmSource.Stop(); 
    }
}
