using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    public float delay;
    private void Start()
    {
        Destroy(this.gameObject, delay);
    }
}
