using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volume;
    public Slider musicVolume;
    public Slider sfxVolume;

    
    
    private void OnEnable()
    {
        volume.value = PlayerPrefs.GetFloat("volume", 1.0f);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.80f);
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume", 0.80f);   
    }
    private void Start()
    {
        audioMixer.SetFloat("volume", Mathf.Lerp(-80.0f, 0.0f, volume.value));
        audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80.0f, -10.0f, musicVolume.value));
        audioMixer.SetFloat("sfxVolume", Mathf.Lerp(-80.0f, -5.0f, sfxVolume.value));
    }
    
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);     
        audioMixer.SetFloat("volume", Mathf.Lerp(-80.0f, 0.0f, volume));
    }
    
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);     
        audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80.0f, -10.0f, volume));
    }
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("sfxVolume", volume);     
        audioMixer.SetFloat("sfxVolume", Mathf.Lerp(-80.0f, -5.0f, volume));
    }
}
