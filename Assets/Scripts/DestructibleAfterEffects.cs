using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleAfterEffects : MonoBehaviour
{
    public float destroyDelay;
    private float _countdown;
    private bool destroyOn = false;
    public GameObject dissovleEffect;
    
    private void Awake()
    {
        this.destroyOn = true;
        _countdown = this.destroyDelay;
    }
    private void Update()
    {
        if(this.destroyOn)
        {
            _countdown -= Time.deltaTime;
        }
        if(_countdown <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
