using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class DeathWave : MonoBehaviour
{
    public float maxHeight;
    public float minHeight;
    

    private void Start()
    {
        foreach (Transform child in this.transform)
        {
            float height = Random.Range(minHeight,maxHeight); 
            float time = Random.Range(1.0f, 5.0f);
            child.transform.TweenLocalPositionY(height, time).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);
            Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 1.0f, 1f);
            //child.GetComponent<Renderer>().material.TweenColor(color, time).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);
            child.transform.TweenScale(child.transform.localScale * 0.9f, time * 2.0f).SetLoops(-1, LoopType.PingPong).SetEase(Ease.QuadInOut);
        }
         
    }
    



}
