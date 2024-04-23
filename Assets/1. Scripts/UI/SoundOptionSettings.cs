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

        // ÃÊ±â º¼·ý, Åä±Û »óÅÂ ¼³Á¤
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
        Debug.Log("SetMasterVolume È£ÃâµÊ. º¼·ý °ª: " + volume);
        AudioListener.volume = volume;
        UpdateToggleStates();
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("SetBGMVolume È£ÃâµÊ. º¼·ý °ª: " + volume);
        audioManager.BGMVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("SetSFXVolume È£ÃâµÊ. º¼·ý °ª: " + volume);
        audioManager.SFXVolume(volume);
    }

    public void ToggleMasterVolume(bool isOn)
    {
        Debug.Log("ToggleMasterVolume È£ÃâµÊ. »óÅÂ: " + isOn);
        AudioListener.volume = isOn ? 1.0f : 0f;
        UpdateSliderValues();
    }

    public void ToggleBGM(bool isOn)
    {
        Debug.Log("ToggleBGM È£ÃâµÊ. »óÅÂ: " + isOn);
        audioManager.ToggleBGM();
    }

    public void ToggleSFX(bool isOn)
    {
        Debug.Log("ToggleSFX È£ÃâµÊ. »óÅÂ: " + isOn);
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
