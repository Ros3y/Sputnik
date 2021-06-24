using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminal : PulsingMaterial
{
    public ParticleSystem disabledEffect;
    public CoreTransition core;
    public bool disabled {get; private set; }

    private void Awake()
    {
        FindObjectOfType<CoreTransition>().transitioned += DisableForeverDelay;
    }
    private void EnableTerminal()
    {
        this.disabled = false;
        this.lightColor = Color.red;
        this.disabledEffect.gameObject.SetActive(false);
    }
    private void DisableTerminal()
    {
        if(!this.disabled)
        {
            this.disabled = true;
            this.lightColor = Color.cyan;
            this.disabledEffect.gameObject.SetActive(true);

            StopPulsing();
            PulseOn();
            Invoke(nameof(EnableTerminal), this.pulseInteveral * 4);
        }
    }
    private void DisableForeverDelay()
    {
        Invoke(nameof(DisableForever), this.pulseInteveral);
    }

    private void DisableForever()
    {
        this.disabled = true;
        this.lightColor = Color.cyan;
        this.disabledEffect.gameObject.SetActive(true);

        StopPulsing();
        PulseOn();
        CancelInvoke();   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Grenade")
        {
            Invoke(nameof(DisableTerminal), 1.0f);
        }
    }

}
