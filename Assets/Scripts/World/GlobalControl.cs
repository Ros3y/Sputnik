using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zigurous.DataStructures;

public class GlobalControl : SingletonBehavior<GlobalControl>
{
    public int currentLevel;
    public string currentLevelName;
    public float CompletionTime;
    public string lastCompletedScene;
    public bool playerIsGrounded;
    public bool isDead;
}
