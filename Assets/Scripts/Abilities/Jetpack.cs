using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    // General Components
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    
    // Jetpacking
    public InputAction jetpackInput;
    public float maxJetpackFuel;
    public float availableJetpackFuel { get; private set; }
    public bool isJetpacking { get; private set; }
    public float jetpackConstantForce;
    public float fuelDecrementMultiplier;
    public float fuelIncrementMultiplier;
    
    // UI
    public float _percentage { get; private set; }

    // Effects
    public ParticleSystem smokeTraileffect1;
    public ParticleSystem rocketFlame1;
    public ParticleSystem smokeTraileffect2;
    public ParticleSystem rocketFlame2;
    
    // Audio
    public AudioSource jetpackSoundSource;

    // Death Mechanics
    private bool _hasDied;
    public float onDeathResetDelay;
    private float _onDeathFuelResetDelay;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        this.availableJetpackFuel = maxJetpackFuel;

        //this.jetpackSoundSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        this.jetpackInput.Enable();
    }

    private void OnDisable()
    {
        this.jetpackInput.Disable();
    }
    private void Update()
    {
        if(this.isJetpacking && !CanJetpack())
        {
            StopJetpacking();
        }
        else if(!this.isJetpacking && CanJetpack())
        {
            StartJetpacking();
        }
         
        UpdateFuel();
        _percentage = (this.availableJetpackFuel/maxJetpackFuel);
        
        //OnDeathFuelReset();     
    }

    // private void FixedUpdate()
    // {
    //     if(_canJetpack)
    //     {
    //         ForceMode mode = ForceMode.Force;
    //         _rigidbody.AddForce(Vector3.up * jetpackConstantForce, mode);     
    //     }       
    // }
    
    // private void OnDeathFuelReset()
    // {
    //     if(this.transform.localScale == Vector3.zero)
    //     {
    //         _hasDied = true;
    //     }
    //     if(_hasDied)
    //     {
    //         _onDeathFuelResetDelay -= Time.deltaTime;
    //     }
    //     if(_onDeathFuelResetDelay <= 0.0f)
    //     {
    //         this.availableJetpackFuel = this.maxJetpackFuel;
    //         _hasDied = false;
    //     }
    //     if(!_hasDied)
    //     {
    //         _onDeathFuelResetDelay = this.onDeathResetDelay;
    //     }        
    // }

    private void UpdateFuel()
    {
        if(this.isJetpacking)
        {
            float fuelDecrease = Time.deltaTime * this.fuelDecrementMultiplier;
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel - fuelDecrease, 0.0f, this.maxJetpackFuel);
        }

        else if(isPlayerGrounded())
        {
            float fuelIncrease = Time.deltaTime * this.fuelIncrementMultiplier;     
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel + fuelIncrease, 0.0f, this.maxJetpackFuel);
        }
    }

    private void StartJetpacking()
    {
        this.isJetpacking = true;
        this.smokeTraileffect1.Play();
        this.rocketFlame1.Play();
        this.smokeTraileffect2.Play();
        this.rocketFlame2.Play();
        this.jetpackSoundSource.Play();
    }

    private void StopJetpacking()
    {
        this.isJetpacking = false;
        this.smokeTraileffect1.Stop();
        this.rocketFlame1.Stop();
        this.smokeTraileffect2.Stop();
        this.rocketFlame2.Stop();
        this.jetpackSoundSource.Stop();
    }

    private bool CanJetpack()
    {
        return this.jetpackInput.phase == InputActionPhase.Started && this.availableJetpackFuel > 0.0f && !isPlayerGrounded();
    }

    private bool isPlayerGrounded()
    {
        return Physics.BoxCast(this.transform.position, this._collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, this._collider.height /  2);
    }  
}
