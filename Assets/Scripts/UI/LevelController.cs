using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject currentLevel { get; private set; }
    public GameObject spawnLocation { get; private set; }
    private GameObject[] _levelObjects;
    private Scene _activeScene;
    public float stopWatch { get; private set; }
    public float completionTime { get; private set; }
    private RespawnPlayer _respawnInformation;
    public GameObject powerCore;
    private PowerCoreBehavior _powerCoreBehavior;
    private bool _levelComplete;
    private GameObject _replayLevel;
    
    
    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene();
        _levelObjects =  _activeScene.GetRootGameObjects();
        this.currentLevel = _levelObjects[0];
        _replayLevel = this.currentLevel;
        GlobalControl.Instance.previousSceneIndex = _activeScene.buildIndex;
        

        this.spawnLocation = this.currentLevel.gameObject.transform.GetChild(0).gameObject;
        this.transform.position = this.spawnLocation.transform.position;
        _respawnInformation = GetComponent<RespawnPlayer>();
        
        this.stopWatch = 0.0f;

        _powerCoreBehavior = powerCore.gameObject.GetComponent<PowerCoreBehavior>();

        _levelComplete = false;

 
    }
    
    private void Update()
    {
        if(_respawnInformation.isSpawning && !_powerCoreBehavior.hasDetonated)
        {
            this.stopWatch += Time.deltaTime;
        }
        else if(_powerCoreBehavior.hasDetonated)
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
