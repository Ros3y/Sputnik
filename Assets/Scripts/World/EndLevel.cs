using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    
    private CoreTransition core;
    public bool canEnd { get; private set; }
    public Text endPrompt;
    private Collider _collider; 
    private void Awake()
    {
        core = FindObjectOfType<CoreTransition>();
        core.transitioned += End;
        canEnd = false;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && core.hasTransitioned == true)
        {
            canEnd = true;
            endPrompt.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && core.hasTransitioned == true)
        {
            canEnd = false;
            endPrompt.enabled = false;
        }
    }

    private void End()
    {
        _collider.enabled = true;   
    }
}
