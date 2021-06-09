using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class LightsBehavior : MonoBehaviour
{
    public GameObject wireLight;
    public Transform endPosition;
    public Transform startPosition;
    private Tween _OriginTween;
    private Tween _tween;
    public float travelTime;
    public bool isOriginLight;
    public float originPulseDelay;



    private void Start()
{
        if(this.isOriginLight)
        {
            _OriginTween = this.wireLight.transform.TweenPosition(this.endPosition.position, this.travelTime)
                            .SetEase(Ease.Linear)
                            .SetAutoKill(false)
                            .OnComplete(RestartOriginLoop);
        }
}

    private void RestartOriginLoop()
    {
        this.wireLight.transform.position = this.startPosition.position;
        _OriginTween.SetDelay(this.originPulseDelay);
        _OriginTween.Restart();      
    
    }

    private void sendSignal()
    {
        this.wireLight.SetActive(true);
        this.wireLight.transform.TweenPosition(this.endPosition.position, this.travelTime)
                                .SetEase(Ease.Linear)
                                .SetAutoKill(false)
                                .OnComplete(resetLightPosition);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Smart Light" && (collider.transform.parent != this.transform))
        {
            Debug.Log("zap");
            sendSignal();  
        }
    }

    private void resetLightPosition()
    {
        this.wireLight.transform.position = this.startPosition.position;
        this.wireLight.SetActive(false);                
    }
}
