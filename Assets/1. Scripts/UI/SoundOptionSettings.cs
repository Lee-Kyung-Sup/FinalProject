using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionSettings : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    public Toggle bgmVolumeToggle;
    public Toggle sfxVolumeToggle;

    void Start()
    {
        bgmVolumeSlider.value = bgmAudioSource.volume;
        sfxVolumeSlider.value = sfxAudioSource.volume;
        bgmVolumeToggle.isOn = bgmAudioSource.volume > 0;
        sfxVolumeToggle.isOn = sfxAudioSource.volume > 0;

        bgmVolumeSlider.onValueChanged.AddListener(HandleBgmVolumeSlider);
        sfxVolumeSlider.onValueChanged.AddListener(HandleSfxVolumeSlider);
        bgmVolumeToggle.onValueChanged.AddListener(HandleBgmToggle);
        sfxVolumeToggle.onValueChanged.AddListener(HandleSfxToggle);
    }

    void HandleBgmVolumeSlider(float volume)
    {
        bgmAudioSource.volume = volume;
        bgmVolumeToggle.isOn = volume > 0;
    }

    void HandleSfxVolumeSlider(float volume)
    {
        sfxAudioSource.volume = volume;
        sfxVolumeToggle.isOn = volume > 0;
    }

    void HandleBgmToggle(bool isOn)
    {
        bgmAudioSource.volume = isOn ? bgmVolumeSlider.value : 0;
    }

    void HandleSfxToggle(bool isOn)
    {
        sfxAudioSource.volume = isOn ? sfxVolumeSlider.value : 0;
    }
}

