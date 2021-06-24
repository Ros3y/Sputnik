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
        Sensitivity.value = PlayerPrefs.GetFloat("sensitivity", 0.5f);
    }

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        if(_cameraController != null)
        {
            float value = Mathf.Lerp(0.1f, 2.0f, PlayerPrefs.GetFloat("sensitivity", 0.5f));
            _cameraController.look.sensitivity = new Vector2(value, value);
        }
    }
  
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        if(_cameraController != null)
        {
            float value = Mathf.Lerp(0.1f, 2.0f, sensitivity);
            _cameraController.look.sensitivity = new Vector2(value, value);
        }
    }
}
