using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private GameObject[] _levelObjects;
    private Scene _activeScene;
    public float stopWatch { get; private set; }
    public float completionTime { get; private set; }
    private RespawnPlayer _respawnInformation;
    public CoreTransition core;
    private bool _levelComplete;
    
    
    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene();
        _levelObjects =  _activeScene.GetRootGameObjects();
    
        //GlobalControl.Instance.previousSceneIndex = _activeScene.buildIndex;
        
        _respawnInformation = GetComponent<RespawnPlayer>();
        
        this.stopWatch = 0.0f;

        _levelComplete = false;

 
    }
    
    private void Update()
    {
        if(_respawnInformation.isSpawning && !this.core.hasTransitioned)
        {
            this.stopWatch += Time.deltaTime;
        }
        else if(this.core.hasTransitioned)
        {
            this.completionTime = this.stopWatch;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _levelComplete = true;
        }

        if(_respawnInformation.isDead)
        {
            this.stopWatch = 0.0f;
        }

        if(_levelComplete)
        {
            GlobalControl.Instance.CompletionTIme = this.completionTime;
            SceneManager.LoadScene("Level Completion Screen");
        }
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(GlobalControl.Instance.previousSceneIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(GlobalControl.Instance.previousSceneIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.CompletionTIme = this.completionTime;
    }
}
