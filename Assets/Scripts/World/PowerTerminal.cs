using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminal : PulsingLight
{
    public GrenadeBehavior grenade;
    public ParticleSystem disabledEffect;
    public CoreTransition core;
    public bool hadCollision;

    public void Awake()
    {
        this.lightColor = Color.red;
    }
    public void Start()
    {
        IncrementIntensity();
    }

    public void Update()
    {
        if(core.hasTransitioned)
        {
            this.lightColor = Color.cyan;
        }
        else
        {
            this.lightColor = Color.red;
        }

    }

    private void DisableTerminal()
    {
        Instantiate(disabledEffect, this.transform.position, this.transform.rotation);
    }
}
