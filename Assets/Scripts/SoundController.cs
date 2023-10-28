using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic, collitionSound;
  
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

    public void PlayCollitionSound()
    {
        collitionSound.Play();
    }




  
  

}
