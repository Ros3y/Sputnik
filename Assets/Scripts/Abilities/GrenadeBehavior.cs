using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zigurous.Reticles;

public class GrenadeBehavior : Destructible
{
    private Rigidbody _rigidbody;
    private SphereCollider _collider;

    // Detonation
    private ContactPoint _contact;
    private bool _hasImpacted;
    public float detonationDelay;
    private float _countdown;
    private float _failsafeDetonationTime = 30.0f;

    // Effects
    public Light _grenadeTimerLight;
    private float _lightTimer;
    private Hitmarker _hitmarker;

    // Audio 
    private AudioSource _audioSource;
    public AudioClip grenadeImpactSound;
    public AudioClip grenadeTimerSound;
    private float _grenadeTimerDelay = 0.5f;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();

        _detonationPosition = this.gameObject;

        _countdown = this.detonationDelay;
        _lightTimer = this.detonationDelay;
        _lightTimer = this.detonationDelay;

        _hasImpacted = false;

        _audioSource = GetComponent<AudioSource>();
        _hitmarker = FindObjectOfType<Hitmarker>();
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
        }
        _grenadeTimerDelay -= Time.deltaTime;
        _failsafeDetonationTime -= Time.deltaTime;
        if(_failsafeDetonationTime <= 0.0f)
        {
            Detonate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_hasImpacted)
        {
            if(collision.transform.tag != "No Stick")
            {
                this.transform.position = collision.contacts[0].point + collision.contacts[0].normal * _collider.radius/6.0f;
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                _hasImpacted = true;
                _audioSource.PlayOneShot(this.grenadeImpactSound);
                _hitmarker.enabled = true;
                Invoke(nameof(playGrenadeTimer), 0.3f);


                Detonate(this.detonationDelay);
            }
            // if(collision.transform.tag == "Death Field")
            // {
            //     Detonate();
            // }
            if(collision.transform.tag == "Power Core")
            {
                Detonate();
            }
        }
    }

    private void playGrenadeTimer()
    {
        _audioSource.PlayOneShot(this.grenadeTimerSound);    
    }
}
