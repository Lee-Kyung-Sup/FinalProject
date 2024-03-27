using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundController : MonoBehaviour
{


    public Slider _bgmSlider, _sfxSlider;


    public void ToggleBGM()
    {
        AudioManager.Instance.ToggleBGM();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void BGMVolume()
    {
        AudioManager.Instance.BGMVolume(_bgmSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
