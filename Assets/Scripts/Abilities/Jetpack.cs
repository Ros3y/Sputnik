using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zigurous.Tweening;

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
    public float rechargeDelay;
    
    // UI
    public float _percentage { get; private set; }

    // Effects
    public ParticleSystem rocketFlame1;
    public ParticleSystem rocketFlame2;
    
    // Audio
    public AudioSource jetpackSoundSource;

    // Death Mechanics
    private bool _hasDied;
    public float onDeathResetDelay;
    private float _onDeathFuelResetDelay = 0.5f;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        this.availableJetpackFuel = maxJetpackFuel;
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

        if(this.availableJetpackFuel <= 0.0f)
        {
            rechargeDelay -= Time.deltaTime;
        }     
    }

    

    private void UpdateFuel()
    {
        if(this.isJetpacking)
        {
            float fuelDecrease = Time.deltaTime * this.fuelDecrementMultiplier;
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel - fuelDecrease, 0.0f, this.maxJetpackFuel);
        }

        else if(availableJetpackFuel <= 0.0f)
        {
            if(rechargeDelay <= 0.0f)
            {
                rechargeDelay = 0.5f;
                float fuelIncrease = Time.deltaTime * this.fuelIncrementMultiplier;     
                this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel + fuelIncrease, 0.0f, this.maxJetpackFuel);
            }
        }

        else
        {
            float fuelIncrease = Time.deltaTime * this.fuelIncrementMultiplier;     
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel + fuelIncrease, 0.0f, this.maxJetpackFuel);   
        }
    }

    private void StartJetpacking()
    {
        this.isJetpacking = true;
        this.rocketFlame1.Play();
        this.rocketFlame2.Play();
        this.jetpackSoundSource.Play();
        this.jetpackSoundSource.KillTweens();
        this.jetpackSoundSource.TweenVolume(1.0f, 0.50f);
    }

    private void StopJetpacking()
    {
        this.isJetpacking = false;
        this.rocketFlame1.Stop();
        this.rocketFlame2.Stop();
        this.jetpackSoundSource.KillTweens();
        this.jetpackSoundSource.TweenVolume(0.0f, 0.25f)
                               .OnComplete(() => this.jetpackSoundSource.Stop());
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
