using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminalManager : MonoBehaviour
{
    private PowerTerminal[] _terminals;
    //public bool isDisabled;
    public ParticleSystem disabledEffect;
    
    private void Awake()
    {
        _terminals = this.GetComponentsInChildren<PowerTerminal>();
    }

   
    private void Update()
    {
        for(int i = 0; i < _terminals.Length; i++)
        {
            if(_terminals[i].hadCollision)
            {
                _terminals[i].lightColor = Color.cyan;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Grenade")
        {
            Invoke(nameof(DisableTerminal), 1.0f);

            for(int i = 0; i < _terminals.Length; i++)
            {
                if(_terminals[i].gameObject == this.gameObject)
                {
                    _terminals[i].lightColor = Color.cyan;
                }
            }
        }
    }

    private void DisableTerminal()
    {
        Instantiate(disabledEffect, this.transform.position, this.transform.rotation);
    }
}
