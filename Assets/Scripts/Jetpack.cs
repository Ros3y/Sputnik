using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    public InputAction jetpackInput;
    public float jetpackFuel;
    public Image fuelTank;
    public Image fuelTankBar;
    private RectTransform _fuelTankRectTransform;
    private RectTransform _fuelTankBarRectTransform;
    public float jetpackConstantForce;
    private float _jetpackStartedTime;
    private float _availableJetpackFuel;
    private float _percentage;
    public float fuelDecrementMultiplier;
    public float fuelIncrementMultiplier;
    private bool _canJetpack;
    public Animator animator;
    public ParticleSystem smokeTraileffect1;
    public ParticleSystem rocketFlame1;
    public ParticleSystem smokeTraileffect2;
    public ParticleSystem rocketFlame2;
    private bool _isJetpacking;
    public AudioClip jetpackSound;
    private AudioSource _jetpackSoundSource;
    private bool _hasDied;
    public float onDeathResetDelay;
    private float _onDeathFuelResetDelay;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _availableJetpackFuel = jetpackFuel;
        _fuelTankRectTransform = fuelTank.GetComponent<RectTransform>();
        _fuelTankBarRectTransform = fuelTankBar.GetComponent<RectTransform>();
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        _jetpackSoundSource = allAudioSources[0];
        _jetpackSoundSource.clip = this.jetpackSound;
        Mathf.Clamp(this._availableJetpackFuel, 0.0f, this.jetpackFuel);
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
         if(jetpackInput.phase == InputActionPhase.Started && _availableJetpackFuel > 0 && !isGrounded())
        {
            ForceMode mode = ForceMode.Force;
            _jetpackStartedTime = Time.time;
            _rigidbody.AddForce(Vector3.up * jetpackConstantForce, mode);
            _canJetpack = true;
            float fuelDecrease = Time.deltaTime * this.fuelDecrementMultiplier;
            _isJetpacking = true;
            if(_availableJetpackFuel - fuelDecrease < 0.0f)
            {
                fuelDecrease = _availableJetpackFuel;
            }
            _availableJetpackFuel -= fuelDecrease;
        }

        if(isGrounded() && _availableJetpackFuel < this.jetpackFuel)
        {
            float fuelIncrease = Time.deltaTime * this.fuelIncrementMultiplier;    
            if((_availableJetpackFuel + fuelIncrease) > this.jetpackFuel)
            {
                fuelIncrease = this.jetpackFuel - _availableJetpackFuel;
            }
            _availableJetpackFuel += fuelIncrease;
        }
        _percentage = (_availableJetpackFuel/jetpackFuel);


        _fuelTankBarRectTransform.sizeDelta = new Vector2(((_fuelTankRectTransform.rect.width) * _percentage), _fuelTankBarRectTransform.sizeDelta.y);


        if(jetpackInput.phase != InputActionPhase.Started || isGrounded() || _availableJetpackFuel <= 0.0f)
        {
            _isJetpacking = false;
        }

        if(_isJetpacking)
        {
            this.smokeTraileffect1.Play();
            this.rocketFlame1.Play();
            this.smokeTraileffect2.Play();
            this.rocketFlame2.Play();
            if(!_jetpackSoundSource.isPlaying)
            {
                _jetpackSoundSource.Play();
            }
        }
        else
        {
            this.smokeTraileffect1.Stop();
            this.rocketFlame1.Stop();
            this.smokeTraileffect2.Stop();
            this.rocketFlame2.Stop();
            _jetpackSoundSource.Pause();
            if(_availableJetpackFuel <= 0.0f || isGrounded())
            {
                _jetpackSoundSource.Stop();               
            }
        }
        //OnDeathFuelReset();     
    }

    private void FixedUpdate()
    {
        if(_canJetpack)
        {
            ForceMode mode = ForceMode.Force;
            _rigidbody.AddForce(Vector3.up * jetpackConstantForce, mode);     
        }       
    }

    private bool isGrounded()
    {
        return Physics.BoxCast(this.transform.position, this._collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, this._collider.height /  2);
    }

    private void OnDeathFuelReset()
    {
        if(this.transform.localScale == Vector3.zero)
        {
            _hasDied = true;
        }
        if(_hasDied)
        {
            _onDeathFuelResetDelay -= Time.deltaTime;
        }
        if(_onDeathFuelResetDelay <= 0.0f)
        {
            _availableJetpackFuel = this.jetpackFuel;
            _hasDied = false;
        }
        if(!_hasDied)
        {
            _onDeathFuelResetDelay = this.onDeathResetDelay;
        }        
    }
}
