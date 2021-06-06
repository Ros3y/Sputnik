using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayParticleEffect : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    public bool destroyOnComplete;
    private void Awake()
    {
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();   
    }

    private void Start()
    {
        if(destroyOnComplete)
        {
            Destroy(this.gameObject, particleSystem.main.duration);   
        }
    }
}
