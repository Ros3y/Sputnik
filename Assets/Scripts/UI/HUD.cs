using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Jetpack _jetpack;
    private RectTransform _fuelTankRectTransform;
    private RectTransform _fuelTankBarRectTransform;
    public Image fuelTank;
    public Image fuelTankBar;
    // public Text ammo;
    private Shoot _shoot;
    public Text stopWatch;
    private LevelController _levelInformation;

    private void Awake()
    {
        _jetpack = GetComponent<Jetpack>();
        _shoot = GetComponent<Shoot>();
        _levelInformation = GetComponent<LevelController>(); 
        _fuelTankRectTransform = fuelTank.GetComponent<RectTransform>();
        _fuelTankBarRectTransform = fuelTankBar.GetComponent<RectTransform>();
    }

    private void Update()
    {
        _fuelTankBarRectTransform.sizeDelta = new Vector2(((_fuelTankRectTransform.rect.width) * _jetpack._percentage), _fuelTankBarRectTransform.sizeDelta.y);
    }
}
