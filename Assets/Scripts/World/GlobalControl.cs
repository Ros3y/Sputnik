using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public int previousSceneIndex;
    public float CompletionTIme;

    private void Awake()
    {
        if(Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
