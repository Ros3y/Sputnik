using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class CubeProjection : MonoBehaviour
{
    public Vector3 normal;
    private Renderer _renderer;
    private float distance = 1.0f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        Vector3 startPosition = this.transform.localPosition;
        Vector3 endProsition = this.transform.localPosition + (this.normal * this.distance);

        Sequence sequence = Tweening.Sequence();
        sequence.Append(this.transform.TweenLocalPosition(endProsition, 0.5f));
        sequence.Append(this.transform.TweenLocalPosition(startPosition, 0.5f).SetDelay(2.0f));
        sequence.Play();
        //_renderer.material.TweenColor(Color.cyan, 1.0f).SetDelay(2.5f);
  
        
    }
}
