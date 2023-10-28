using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic, collitionSound;
    [SerializeField] Slider volumeSlider, effectSlider, musicSlider;
    [SerializeField] private AudioMixer mixer;
    public static SoundController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  

        } else
        {
            Destroy(gameObject);
        }
        this.PlayBackgroundMusic();
        this.initVolume();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void PlayCollitionSound()
    {
        collitionSound.Play();
    }

    void initVolume()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            Load();

        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("effectVolume"))
        {
            PlayerPrefs.SetFloat("effectVolume", 1);
            Load();

        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();

        }
        else
        {
            Load();
        }


    }

    public void ChangeVolume()
    {

        mixer.SetFloat("Master", ConvertValue(volumeSlider.value));
        mixer.SetFloat("Effects", ConvertValue(effectSlider.value));
        mixer.SetFloat("Background", ConvertValue(musicSlider.value));
        Save();

    }


    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        effectSlider.value = PlayerPrefs.GetFloat("effectVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        ChangeVolume();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("effectVolume", effectSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);

    }

    private float ConvertValue(float value)
    {
        return Mathf.Log10(value) * 20;

    }


}
