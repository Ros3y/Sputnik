using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class CubeEffects : MonoBehaviour
{
    private void Start()
    {
        float time = Random.Range(1.0f, 5.0f);
        this.transform.TweenScale(this.transform.localScale * 0.875f, time * 2.0f).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);     
    }

}
