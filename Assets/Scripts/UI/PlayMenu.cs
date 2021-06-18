using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Zigurous.Tweening;

public class PlayMenu : MonoBehaviour
{
    public Text time;
    public Text bestTime;
    public Slider sensitivity;
    public Button continueButton;


    private void Start()
    {
        if(GlobalControl.Instance.currentLevel == 0)
        {
            if(continueButton != null)
            {
                continueButton.interactable = false;
            }
        }
    }
    public void NewGame()
    {
        LevelController.LevelSelect(1);
    }

    public void ContinueGame()
    {
        LevelController.LevelSelect(GlobalControl.Instance.currentLevel);
    }


    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void QuitFromComplete()
    {
        GlobalControl.Instance.currentLevel++;     
        SceneManager.LoadScene("Menu Stage");
    }

    public void QuitFromPause()
    {
        SceneManager.LoadScene("Menu Stage");
    }
    public void QuitFromEnd()
    {
        GlobalControl.Instance.currentLevel = 1;
        SceneManager.LoadScene("Menu Stage");
    }


    public void Update()
    {
        if(this.time != null)
        {
            this.time.text = System.Math.Round(GlobalControl.Instance.CompletionTime, 2).ToString() + " Seconds";
        }
        if(this.bestTime != null)
        {
            this.bestTime.text = System.Math.Round(PlayerPrefs.GetFloat(GlobalControl.Instance.lastCompletedScene), 2).ToString() + " Seconds";
        }
    } 
}
