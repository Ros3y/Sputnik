using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zigurous.Tweening;

public class DisplayLevelName : MonoBehaviour
{
    private Text _levelText;
    
    private void Awake()
    {
        _levelText = this.GetComponent<Text>();
    }
    
    private void Start()
    {
        _levelText.color = new Color(_levelText.color.r, _levelText.color.g, _levelText.color.b, 0.0f);
        _levelText.text = GlobalControl.Instance.currentLevelName;
        _levelText.TweenAlpha(1, 2.0f);
        _levelText.TweenAlpha(0,1.0f).SetDelay(2.0f);
    }
}
