using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerScrolling : MonoBehaviour
{
    private ScrollRect _scrollRect;
    public InputAction scrollInput;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        scrollInput.Enable();
    }

    private void OnDisable()
    {
        scrollInput.Disable(); 
    }


    private void Update()
    {
        if(EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        if(EventSystem.current.currentSelectedGameObject == _scrollRect.gameObject || EventSystem.current.currentSelectedGameObject.transform.parent == _scrollRect.content)
        {
            Vector2 scroll = scrollInput.ReadValue<Vector2>() * Time.deltaTime;
            _scrollRect.normalizedPosition += scroll;       
        }
    }
}
