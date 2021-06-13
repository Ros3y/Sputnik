using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class PulsingMaterial : Pulsing
{
    public Renderer[] lightRenderers;
    public Color lightColor;

    protected override void AnimateOn()
    {
        for(int i = 0; i < this.lightRenderers.Length; i++)
        {
            this.lightRenderers[i].material.TweenColor(this.lightColor, this.pulseInteveral).OnComplete(PulseOff);
        }
    }

     protected override void AnimateOff()
    {
        for(int i = 0; i < this.lightRenderers.Length; i++)
        {
            this.lightRenderers[i].material.TweenColor(Color.black, this.pulseInteveral).OnComplete(PulseOn);
        }
    }

    protected override void StopPulsing()
    {
        base.StopPulsing();

        for(int i = 0; i < this.lightRenderers.Length; i++)
        {
            this.lightRenderers[i].material.KillTweens();
            this.lightRenderers[i].material.color = Color.black;
        }
    }
}
