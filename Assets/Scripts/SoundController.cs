using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic, effectSource;

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
     
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void PlaySoundAmbient(AudioClip clip)
    {

        backgroundMusic.PlayOneShot(clip);
    }

    public void PlaySoundEffect(AudioClip clip)
    {

        effectSource.PlayOneShot(clip);
    }



}
