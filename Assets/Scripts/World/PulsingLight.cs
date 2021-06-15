using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class PulsingLight : Pulsing
{
    public Light pulseLight;
    public float maxIntensity;
    public bool transitionWithCore;

    private void Awake()
    {
        FindObjectOfType<CoreTransition>().transitioned += Transition;
    }
    
    protected override void AnimateOn()
    {
        this.pulseLight.TweenIntensity(this.maxIntensity, this.pulseInteveral).OnComplete(PulseOff);
    }

    protected override void AnimateOff()
    {
        this.pulseLight.TweenIntensity(0.0f, this.pulseInteveral).OnComplete(PulseOn);
    }

    protected override void StopPulsing()
    {
        base.StopPulsing();

        this.pulseLight.KillTweens();
        this.pulseLight.intensity = 0.0f;
    }

    private void Transition()
    {
        if(transitionWithCore)
        {
            pulseLight.color = Color.cyan;   
        }
    }

}
