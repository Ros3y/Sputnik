using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Zigurous.Tweening;
using Zigurous.CameraSystem;

public class LevelController : MonoBehaviour
{
    private GameObject[] _levelObjects;
    private Scene _activeScene;
    public float stopWatch { get; private set; }
    public float completionTime { get; private set; }
    private RespawnPlayer _respawnInformation;
    public CoreTransition core;
    public EndLevel level;
    public InputAction EndLevelInput;
    public InputAction pauseLevelInput;
    public GameObject pauseScreen;
    public bool isPaused { get; private set; }
    private bool _endLevel;
    private CameraController _cameraController;
    
    
    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene();
        _levelObjects =  _activeScene.GetRootGameObjects();

        _cameraController = FindObjectOfType<CameraController>();
        
        _respawnInformation = GetComponent<RespawnPlayer>();
        
        this.stopWatch = 0.0f;

        _endLevel = false;
        this.isPaused = false;

        this.EndLevelInput.performed += OnEndLevel;
        this.pauseLevelInput.performed += OnPauseLevel;
 
    }


    private void OnEnable()
    {
        this.EndLevelInput.Enable();
        this.pauseLevelInput.Enable();
        // Sensitivity.value = PlayerPrefs.GetFloat("sensitivity", 1.5f);

    }

    private void OnDisable()
    {
        this.EndLevelInput.Disable();
        this.pauseLevelInput.Disable();
    }
    
    private void Update()
    {
        if(_respawnInformation.isSpawning && !_endLevel)
        {
            this.stopWatch += Time.deltaTime;
        }
        else if(_endLevel)
        {
            this.completionTime = this.stopWatch;
        }

        if(_respawnInformation.isDead)
        {
            this.stopWatch = 0.0f;
        }

    }
     public static void LevelSelect(int level)
    {
        GlobalControl.Instance.currentLevel = level;
        
        switch(level)
        {
            case 0:
            SceneManager.LoadScene("Menu Stage");
            break;
            
            case 1:
            SceneManager.LoadScene("The Gap");
            break;

            case 2:
            SceneManager.LoadScene("The Rise");
            break;

            case 3:
            SceneManager.LoadScene("Around The Bend");
            break;

            case 4:
            SceneManager.LoadScene("Hop Skip And A Jump");
            break;

            case 5:
            SceneManager.LoadScene("Reflections");
            break;

            case 6:
            SceneManager.LoadScene("Pillars");
            break;
            
            case 7:
            SceneManager.LoadScene("Precision Flying");
            break;

            case 8:
            SceneManager.LoadScene("Pitfall");
            break;

            case 9:
            SceneManager.LoadScene("Claustrophobia");
            break;

            case 10:
            SceneManager.LoadScene("Back and Forth");
            break;

            case 11:
            SceneManager.LoadScene("Going Up");
            break;

            case 12:
            SceneManager.LoadScene("Sheer Cliff");
            break;

            case 13:
            SceneManager.LoadScene("Pinball");
            break;

            case 14:
            SceneManager.LoadScene("Over and Under");
            break;

            case 15:
            SceneManager.LoadScene("Ahead of the Curve");
            break;

            case 16:
            SceneManager.LoadScene("Peak Performance");
            break;

            case 17:
            SceneManager.LoadScene("Demo End");
            break;

        }
         
    }

    private void OnEndLevel(InputAction.CallbackContext context)
    {
        if(level.canEnd)
        {
            _endLevel = true;

            GlobalControl.Instance.CompletionTime = this.stopWatch;

            if(this.stopWatch < PlayerPrefs.GetFloat(_activeScene.name, float.MaxValue))
            {
                PlayerPrefs.SetFloat(_activeScene.name, this.stopWatch);
                PlayerPrefs.Save();  
            }
            

            GlobalControl.Instance.lastCompletedScene = _activeScene.name;
            
            SceneManager.LoadScene("Level Complete Stage");
        }
    }
    private void OnPauseLevel(InputAction.CallbackContext context)
    {
        if(this.pauseScreen.activeSelf)
        {
            this.isPaused = false;
            Time.timeScale = 1.0f;
            this.pauseScreen.SetActive(false);
            _cameraController.look.enabled = true;
            AudioListener.pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else if(!this.pauseScreen.activeSelf)
        {
            this.isPaused = true;
            Time.timeScale = 0.0f;
            this.pauseScreen.SetActive(true);
            _cameraController.look.enabled = false;
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void reloadScene()
    {
        LevelSelect(GlobalControl.Instance.currentLevel);       
    }

    public void loadNextScene()
    {
        LevelSelect(GlobalControl.Instance.currentLevel + 1);    
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.CompletionTime = this.completionTime;
    }
}
