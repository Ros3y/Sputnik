using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Zigurous.Tweening;
using Zigurous.CameraSystem;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour
{
    public float stopWatch { get; private set; }
    public float completionTime { get; private set; }
    private RespawnPlayer _respawnInformation;
    public EndLevel level;
    public InputAction EndLevelInput;
    public InputAction pauseLevelInput;
    public GameObject pauseScreen;
    public bool isPaused { get; private set; }
    private bool _endLevel;
    private CameraController _cameraController;
    private ControllerNavigation _controllerNavigation;
    
    
    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
        
        _respawnInformation = GetComponent<RespawnPlayer>();
        
        this.stopWatch = 0.0f;

        _endLevel = false;
        this.isPaused = false;

        this.EndLevelInput.performed += OnEndLevel;
        this.pauseLevelInput.performed += OnPauseLevel;

        _controllerNavigation = FindObjectOfType<ControllerNavigation>();
 
    }


    private void OnEnable()
    {
        this.EndLevelInput.Enable();
        this.pauseLevelInput.Enable();
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
            SceneLoader.Instance.StartTransition("Menu Stage");
            break;
            
            case 1:
            SceneLoader.Instance.StartTransition("The Gap");
            break;

            case 2:
            SceneLoader.Instance.StartTransition("The Rise");
            break;

            case 3:
            SceneLoader.Instance.StartTransition("Around The Bend");
            break;

            case 4:
            SceneLoader.Instance.StartTransition("Hop Skip And A Jump");
            break;

            case 5:
            SceneLoader.Instance.StartTransition("Reflections");
            break;

            case 6:
            SceneLoader.Instance.StartTransition("Pillars");
            break;
            
            case 7:
            SceneLoader.Instance.StartTransition("Precision Flying");
            break;

            case 8:
            SceneLoader.Instance.StartTransition("Pitfall");
            break;

            case 9:
            SceneLoader.Instance.StartTransition("Claustrophobia");
            break;

            case 10:
            SceneLoader.Instance.StartTransition("Back and Forth");
            break;

            case 11:
            SceneLoader.Instance.StartTransition("Going Up");
            break;

            case 12:
            SceneLoader.Instance.StartTransition("Sheer Cliff");
            break;

            case 13:
            SceneLoader.Instance.StartTransition("Pinball");
            break;

            case 14:
            SceneLoader.Instance.StartTransition("Over and Under");
            break;

            case 15:
            SceneLoader.Instance.StartTransition("Ahead of the Curve");
            break;

            case 16:
            SceneLoader.Instance.StartTransition("Peak Performance");
            break;

            case 17:
            SceneLoader.Instance.StartTransition("Demo End");
            break;

        }
         
    }

    private void OnEndLevel(InputAction.CallbackContext context)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if(level.canEnd)
        {
            EndLevelInput.Disable();
            _endLevel = true;

            GlobalControl.Instance.CompletionTime = this.stopWatch;

            if(this.stopWatch < PlayerPrefs.GetFloat(activeScene.name, float.MaxValue))
            {
                PlayerPrefs.SetFloat(activeScene.name, this.stopWatch);
                PlayerPrefs.Save();  
            }
            

            GlobalControl.Instance.lastCompletedScene = activeScene.name;
            
            SceneLoader.Instance.StartTransition("Level Complete Stage");
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

            if(EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(_controllerNavigation.pauseMenuFirst);
            }
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
