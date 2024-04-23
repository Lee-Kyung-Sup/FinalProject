using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle masterVolumeToggle;
    [SerializeField] private Toggle bgmVolumeToggle;
    [SerializeField] private Toggle sfxVolumeToggle;

    private AudioManager audioManager;

    void Start()
    {

        audioManager = AudioManager.Instance;

        // 초기 볼륨, 토글 상태 설정
        UpdateUIWithSoundSettings();
    }

    public void UpdateUIWithSoundSettings()
    {
        masterVolumeSlider.value = AudioListener.volume;
        bgmVolumeSlider.value = audioManager.bgmSource.volume;
        sfxVolumeSlider.value = audioManager.sfxSource.volume;

        masterVolumeToggle.isOn = AudioListener.volume > 0;
        bgmVolumeToggle.isOn = !audioManager.bgmSource.mute;
        sfxVolumeToggle.isOn = !audioManager.sfxSource.mute;
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        UpdateToggleStates();
    }

    public void SetBGMVolume(float volume)
    {
        audioManager.BGMVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioManager.SFXVolume(volume);
    }

    public void ToggleMasterVolume(bool isOn)
    {
        AudioListener.volume = isOn ? 1.0f : 0f;
        UpdateSliderValues();
    }

    public void ToggleBGM(bool isOn)
    {
        audioManager.ToggleBGM();
    }

    public void ToggleSFX(bool isOn)
    {
        audioManager.ToggleSFX();
    }

    private void UpdateSliderValues()
    {
        masterVolumeSlider.value = AudioListener.volume;
        bgmVolumeSlider.value = audioManager.bgmSource.volume;
        sfxVolumeSlider.value = audioManager.sfxSource.volume;
    }

    private void UpdateToggleStates()
    {
        masterVolumeToggle.isOn = AudioListener.volume > 0;
        bgmVolumeToggle.isOn = !audioManager.bgmSource.mute;
        sfxVolumeToggle.isOn = !audioManager.sfxSource.mute;
    }
}
