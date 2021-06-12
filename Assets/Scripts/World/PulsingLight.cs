using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class PulsingLight : MonoBehaviour
{
    
    public Light pulseLight;
    public Material lightMaterial;
    public float pulseInteveral;
    public float maxIntensity;
    [HideInInspector]
    public Color lightColor;
    
    private void Start()
    {
        IncrementIntensity();
    }
    private void Update()
    {
       
    }

    public void IncrementIntensity()
    {
        if(this.pulseLight != null)
        {
            this.pulseLight.TweenIntensity(maxIntensity, this.pulseInteveral).OnComplete(DecrementIntensity);
        }
        if(this.lightMaterial != null)
        {
            this.lightMaterial.TweenColor(this.lightColor, pulseInteveral).OnComplete(DecrementIntensity);    
        }
    }

    public void DecrementIntensity()
    {
        if(this.pulseLight != null)
        {
            this.pulseLight.TweenIntensity(0.0f, this.pulseInteveral).OnComplete(IncrementIntensity);
        }
        if(this.lightMaterial != null)
        {
            this.lightMaterial.TweenColor(Color.black, pulseInteveral).OnComplete(IncrementIntensity);
        }
    }

}
