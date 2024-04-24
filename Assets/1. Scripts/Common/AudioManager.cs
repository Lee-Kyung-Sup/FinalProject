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

    public void BGMVolume(float volume)
    {
        Debug.Log("BGMVolume ¸Þ¼Òµå ½ÇÇàµÊ. º¼·ý ¼³Á¤: " + volume);
        bgmSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        Debug.Log("SFXVolume ¸Þ¼Òµå ½ÇÇàµÊ. º¼·ý ¼³Á¤: " + volume);
        sfxSource.volume = volume;
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
        Debug.Log("BGM ¹ÂÆ® »óÅÂ: " + bgmSource.mute);
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        Debug.Log("SFX ¹ÂÆ® »óÅÂ: " + sfxSource.mute);
    }

    public void StopBGM()
    {
        bgmSource.Stop(); 
    }
}
