using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class Wire : MonoBehaviour
{
    private BoxCollider _collider;

    public Transform startPosition;
    public Transform endPosition;
    public Pulse pulsePrefab;
    public float pulseTravelTime;
    public bool pulseEmitter;
    public float pulseEmitInterval;
    public bool transferable;
    public bool replicatable;

    public bool deadEnd;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.startPosition.position, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.endPosition.position, 0.5f);
    }


    private void Start()
    {
        if(this.pulseEmitter)
        {
            InvokeRepeating(nameof(EmmitPulse), this.pulseEmitInterval, this.pulseEmitInterval);
        }
    }

    private Pulse EmmitPulse()
    {
        Pulse pulse = Instantiate(this.pulsePrefab, this.startPosition.position, Quaternion.identity);
        pulse.currentWire = this;
        TweenPulse(pulse);
        return pulse;
    }

    private void TweenPulse(Pulse pulse)
    {
        TweenCallback onComplete = () => {
            if (this.deadEnd) {
                Destroy(pulse.gameObject);
            }    
        };
        float timeScale = this.pulseTravelTime * pulse.currentWire.gameObject.transform.localScale.z;
        pulse.transform.position = this.startPosition.position;
        pulse.transform.TweenPosition(this.endPosition.position, timeScale)
                       .SetEase(Ease.Linear)
                       .OnComplete(onComplete);                    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.transferable)
        {
            Pulse pulse = other.gameObject.GetComponent<Pulse>();
            if(pulse != null && pulse.currentWire != this && pulse.sourceWire != this)
            {
                if(this.replicatable)
                {
                    Pulse newPulse = EmmitPulse();
                    newPulse.sourceWire = pulse.currentWire;
                }
                else
                {
                    pulse.sourceWire = pulse.currentWire;
                    pulse.currentWire = this;
                    pulse.CancelInvoke();
                    TweenPulse(pulse);
                }
            }
        }
    }
}
