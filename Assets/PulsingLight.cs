using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class PulsingLight : MonoBehaviour
{
    
    public Light spawnLight;
    public float pulseInteveral;
    public float maxIntensity;

    
    private void Start()
    {
        IncrementIntensity();
    }

    private void IncrementIntensity()
    {
        spawnLight.TweenIntensity(maxIntensity, this.pulseInteveral).OnComplete(DecrementIntensity);    
    }

    private void DecrementIntensity()
    {
        spawnLight.TweenIntensity(0.0f, this.pulseInteveral).OnComplete(IncrementIntensity);
    }

}
