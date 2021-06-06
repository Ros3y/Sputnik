using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayMenu : MonoBehaviour
{
    public TMP_Text time;

   
    private void Awake()
    {
        this.time = GetComponent<TMP_Text>();              
    }

    public void NewGame()
    {
       SceneManager.LoadScene("Chapter 0 Level 1");
    }

    public void Update()
    {
        if(this.time != null)
        {
            this.time.text = System.Math.Round(GlobalControl.Instance.CompletionTIme, 2).ToString() + " Seconds";
        }
    } 
}
