using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pulsing : MonoBehaviour
{
    public float pulseInteveral;
    public bool on { get; private set; }

    private void Start()
    {
        PulseOn();
    }

    public void PulseOn()
    {
        if(this.on)
        {
            return;
        }

        this.on = true;
        AnimateOn();
    }

    public void PulseOff()
    {
        if(!this.on)
        {
            return;
        }

        this.on = false;
        AnimateOff();
    }

    protected virtual void StopPulsing()
    {
        this.on = false;
    }

    protected abstract void AnimateOn();
    protected abstract void AnimateOff();
}
