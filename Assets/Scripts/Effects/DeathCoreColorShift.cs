using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class DeathCoreColorShift : MonoBehaviour
{
    private void Start()
    {
        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 1.0f, 1f);
        float time = Random.Range(1.0f, 5.0f);
        this.transform.GetComponent<Renderer>().material.TweenColor(Color.black, time).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);
        //this.transform.GetComponent<Renderer>().material.TweenAlpha(0.0f, time).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);
        this.transform.TweenScale(this.transform.localScale * 0.75f, time * 2.0f).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);    
    }

    
}
