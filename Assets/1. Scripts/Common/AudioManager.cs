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
    } //�̱���

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
        Debug.Log("BGMVolume �޼ҵ� �����. ���� ����: " + volume);
        bgmSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        Debug.Log("SFXVolume �޼ҵ� �����. ���� ����: " + volume);
        sfxSource.volume = volume;
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
        Debug.Log("BGM ��Ʈ ����: " + bgmSource.mute);
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        Debug.Log("SFX ��Ʈ ����: " + sfxSource.mute);
    }

    public void StopBGM()
    {
        bgmSource.Stop(); 
    }
}
