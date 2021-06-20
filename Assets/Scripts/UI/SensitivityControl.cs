using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.CameraSystem;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{
  
    private CameraController _cameraController;
    public Slider Sensitivity;

    private void OnEnable()
    {
        Sensitivity.value = PlayerPrefs.GetFloat("sensitivity", 0.75f);
    }

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        if(_cameraController != null)
        {
            _cameraController.look.sensitivity = new Vector2(PlayerPrefs.GetFloat("sensitivity", 0.75f), PlayerPrefs.GetFloat("sensitivity", 0.75f));
        }
    }
  
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        if(_cameraController != null)
        {
            _cameraController.look.sensitivity = new Vector2(sensitivity, sensitivity);
        }
    }
}
