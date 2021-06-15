using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class CoreBehavior : MonoBehaviour
{
    private BoxCollider _collider;

    public float maxHeight;
    public float timeScale;
    private float _height;
    public float rotationSpeed;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _initialPosition = this.transform.position;
    }

    private void Update()
    {
        _height = Mathf.PingPong(Time.time * this.timeScale, this.maxHeight);
        this.transform.position = new Vector3(this.transform.position.x, _initialPosition.y + _height, this.transform.position.z);
        this.transform.Rotate((Vector3.up) * (Time.deltaTime * this.rotationSpeed));
    }
}
