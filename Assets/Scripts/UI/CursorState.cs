using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    public bool visible;
    public CursorLockMode locked;

    private void Start()
    {
        Cursor.visible = this.visible;
        Cursor.lockState = locked;
    }
}
