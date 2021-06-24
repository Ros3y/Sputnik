using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLevel : MonoBehaviour
{
    public bool visible;
    public CursorLockMode locked;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = this.visible;
        Cursor.lockState = locked;
        AudioListener.pause = false;
        GlobalControl.Instance.isDead = false;
    }
}
