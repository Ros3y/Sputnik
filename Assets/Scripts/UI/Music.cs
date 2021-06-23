using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioClip[] songs;
    private float _sceneIndex;
    private AudioSource _audioSource;

    private void Awake()
    {
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _audioSource = this.GetComponent<AudioSource>();
        SongSelection();
        _audioSource.Play();
    }

    private void SongSelection()
    {
        switch(_sceneIndex)
        {
            case 0:
            _audioSource.clip = songs[0];
            break;

            case 1:
            _audioSource.clip = songs[1];
            break;

            case 2:
            _audioSource.clip = songs[2];
            break;

            case 3:
            _audioSource.clip = songs[3];
            break;

            case 4:
            _audioSource.clip = songs[4];
            break;

            case 5:
            _audioSource.clip = songs[5];
            break;

            case 6:
            _audioSource.clip = songs[6];
            break;

            case 7:
            _audioSource.clip = songs[7];
            break;

            case 8:
            _audioSource.clip = songs[8];
            break;

            case 9:
            _audioSource.clip = songs[9];
            break;

            case 10:
            _audioSource.clip = songs[10];
            break;

            case 11:
            _audioSource.clip = songs[11];
            break;

            case 12:
            _audioSource.clip = songs[12];
            break;

            case 13:
            _audioSource.clip = songs[13];
            break;

            case 14:
            _audioSource.clip = songs[14];
            break;

             case 15:
            _audioSource.clip = songs[15];
            break;

             case 16:
            _audioSource.clip = songs[16];
            break;

             case 17:
            _audioSource.clip = songs[17];
            break;

        }
    }
}
