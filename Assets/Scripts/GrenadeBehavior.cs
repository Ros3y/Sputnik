using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GrenadeBehavior : Destructible
{
    private Rigidbody _rigidbody;

    // Detonation
    private ContactPoint _contact;
    private bool _hasImpacted;
    public float detonationDelay;
    private float _countdown;
    private bool hasDetonated = false;

    // Effects
    public Light _grenadeTimerLight;
    private float _lightTimer;

    // Audio 
    private AudioSource _audioSource;
    public AudioClip grenadeImpactSound;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _detonationPosition = this.gameObject;

        _countdown = this.detonationDelay;
        _lightTimer = this.detonationDelay;
        _lightTimer = this.detonationDelay;

        _hasImpacted = false;

        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(_hasImpacted)
        {
            if(_lightTimer - Time.deltaTime >= 0.0f)
            {
                _lightTimer -= Time.deltaTime;
            }
            else
            {
                _lightTimer = 0.0f;
            }
            _grenadeTimerLight.intensity = Mathf.Lerp(8.0f, 0.0f, _lightTimer);
            Debug.Log(_grenadeTimerLight);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _contact = collision.contacts[0];
        if(!hasDetonated)
        {
            FixedJoint _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = collision.rigidbody;

            _hasImpacted = true;
            _audioSource.PlayOneShot(this.grenadeImpactSound);

            Detonate(this.detonationDelay);
        
            hasDetonated = true;
        }
    }
}
