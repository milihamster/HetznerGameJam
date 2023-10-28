using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider, effectSlider, musicSlider;
    [SerializeField] private AudioMixer mixer;
    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            SetMasterVolume();

        }


        if (PlayerPrefs.HasKey("effectVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("effectVolume", 1);
            SetEffectVolume();
        }


        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            SetMusicAudio();
        }


    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);

    }

    public void SetEffectVolume()
    {
        float volume = effectSlider.value;
        mixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("effectVolume", volume);
    }

    public void SetMusicAudio()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Background", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

 
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");

        SetMusicAudio();
        SetMasterVolume();
        SetEffectVolume();


    }
   
}
