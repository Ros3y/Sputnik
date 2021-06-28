using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zigurous.Tweening;

public class Music : MonoBehaviour
{
    public AudioClip[] songs;
    private float _sceneIndex;
    public AudioSource audioSource { get; private set; }

    private void Awake()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayRandomSong();
    }

    private IEnumerator PlaySong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
        yield return new WaitForSeconds(song.length);
        PlayRandomSong();
    }
    
    private void PlayRandomSong()
    {
        int randomSong = Random.Range(0,6);
        audioSource.volume = 0.0f;
        audioSource.TweenVolume(1.0f, 1.0f);
        StartCoroutine(PlaySong(songs[randomSong]));
    }
}
