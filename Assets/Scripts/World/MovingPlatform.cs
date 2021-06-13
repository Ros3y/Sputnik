using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class MovingPlatform : MonoBehaviour
{
    private BoxCollider _collider;
    public float maxHeight;
    public float maxLength;
    public float maxWidth;
    public float timeScale;
    private float _yPosition;
    private float _xPosition;
    private float _zPosition;
    public float rotationSpeed;
    private Vector3 _initialPosition;
    public bool moveUpDown;
    public bool moveLeftRight;
    public bool moveBackForth;
    public bool rotate;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _initialPosition = this.transform.position;
    }

    private void Update()
    {
        if(moveLeftRight)
        {
            MoveLeftRight();
        }
        if(moveUpDown)
        {
            MoveUpDown();
        }
        if(moveBackForth)
        {
            MoveBackForth();
        }
        if(rotate)
        {
            Rotate();
        }     
    }

    private void MoveLeftRight()
    {
        _xPosition = Mathf.PingPong(Time.time * this.timeScale, this.maxLength);
        this.transform.position = new Vector3(_xPosition, _initialPosition.y, this.transform.position.z); 
    }
    private void MoveUpDown()
    {
        _yPosition = Mathf.PingPong(Time.time * this.timeScale, this.maxHeight);
        this.transform.position = new Vector3(this.transform.position.x, _initialPosition.y + _yPosition, this.transform.position.z);     
    }
    private void MoveBackForth()
    {
        _zPosition = Mathf.PingPong(Time.time * this.timeScale, this.maxWidth);
        this.transform.position = new Vector3(this.transform.position.x, _initialPosition.y, this.transform.position.z + _zPosition); 
    }

    private void Rotate()
    {
        this.transform.Rotate((Vector3.up) * (Time.deltaTime * this.rotationSpeed));        
    }  
}
