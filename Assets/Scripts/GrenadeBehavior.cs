using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GrenadeBehavior : MonoBehaviour
{
    public float detonationDelay;
    public GameObject detonationEffect;
    private float _countdown;
    private float _lightTimer;
    public float detonationRadius;
    private bool hasDetonated = false;
    public float detonationForce;
    private Rigidbody _rigidbody;
    private ContactPoint _contact;
    private Light _grenadeTimerLight;
    public AudioSource grenadeImpactSound;
    private bool _hasImpacted; 
    

    private void Awake()
    {
        _countdown = this.detonationDelay;
        _lightTimer = this.detonationDelay;
        _rigidbody = GetComponent<Rigidbody>();
        _lightTimer = this.detonationDelay;
        _grenadeTimerLight = this.transform.GetChild(0).gameObject.GetComponent<Light>();
        _hasImpacted = false;
        this.grenadeImpactSound = GetComponent<AudioSource>();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        _contact = collision.contacts[0];
        if(!hasDetonated)
        {
            FixedJoint _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = collision.rigidbody;
            //_rigidbody.isKinematic = true;
            this.grenadeImpactSound.Play();
            Instantiate(_grenadeTimerLight);
            _grenadeTimerLight.transform.position = transform.position;
            _hasImpacted = true;
            if(collision.collider.tag == "Power Core")
            {
                Invoke(nameof(detonate), 0.0f);    
            }        
            else
            {
                Invoke(nameof(detonate), _countdown);
            }
            hasDetonated = true;
        }
    }

    private void detonate()
    {
        Instantiate(detonationEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, detonationRadius);

        foreach(Collider nearByObject in colliders)
        {
            Rigidbody nearByRigidbody = nearByObject.GetComponent<Rigidbody>();
            if(nearByRigidbody != null)
            {
               nearByRigidbody.AddExplosionForce(detonationForce, this.transform.position, detonationRadius); 
            }    
        }
        Destroy(this.gameObject);
    }
}
