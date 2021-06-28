using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayMenu : MonoBehaviour
{
    public Text time;
    public Text bestTime;
    public Text currentLevel;
    public Button continueButton;
    public Text pauseLevelName;
    public NewGameShoot newGameShoot;
    private ControllerNavigation _controllerNavigation;
    
    private void Awake()
    {
        newGameShoot = FindObjectOfType<NewGameShoot>();

        _controllerNavigation = FindObjectOfType<ControllerNavigation>();   
    }
    private void Start()
    {
        if(GlobalControl.Instance.currentLevel == 0)
        {
            if(continueButton != null)
            {
                continueButton.interactable = false;
            }
        }

        if(this.time != null)
        {
            this.time.text = System.Math.Round(GlobalControl.Instance.CompletionTime, 2).ToString() + " s";
        }
        if(this.bestTime != null)
        {
            this.bestTime.text = System.Math.Round(PlayerPrefs.GetFloat(GlobalControl.Instance.lastCompletedScene), 2).ToString() + " s";
            if(this.bestTime.text == this.time.text)
            {
                this.time.color = Color.cyan;
            }

            else
            {
                this.time.color = Color.red;
            }
        }

        if(GlobalControl.Instance.currentLevelName == "Level Complete Stage")
        {
            this.currentLevel.text = GlobalControl.Instance.lastCompletedScene;
        }

        if(this.pauseLevelName != null)
        {
            this.pauseLevelName.text = GlobalControl.Instance.currentLevelName;
        }
    }
    public void NewGame()
    {
        if(!IsInvoking("StartNewGame"))
        {
            newGameShoot.ShootGrenade();   
        }
        Invoke(nameof(StartNewGame), 5.0f);
        EventSystem.current.enabled = false;
    }

    public void StartNewGame()
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
        SceneLoader.Instance.StartTransition("Menu Stage");
    }

    public void QuitFromPause()
    {
        SceneLoader.Instance.StartTransition("Menu Stage");
    }
    public void QuitFromEnd()
    {
        GlobalControl.Instance.currentLevel = 1;
        SceneLoader.Instance.StartTransition("Menu Stage");
    }

    public void SelectOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.optionsMenuFirst);
    }

    public void SelectPlayMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.playMenuFirst);
    }

    public void SelectLevelSelectMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.levelSelectFirst);
    }

    public void SelectQuitMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.quitMenuFirst);
    }

    public void SelectExtrasMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.extrasMenuFirst);
    }

    public void SelectBackFromPlay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.mainMenuFirst);
    }

    public void SelectBackFromOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.mainMenuFirst);
    }

    public void SelectBackFromExtras()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.mainMenuFirst);
    }

    public void SelectBackFromQuit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.mainMenuFirst);
    }

    public void SelectBackFromLevelSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_controllerNavigation.playMenuFirst);
    }
}
