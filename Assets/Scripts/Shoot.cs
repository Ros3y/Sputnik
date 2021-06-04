using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    // Inputs
    private Rigidbody _rigidbody;
    public InputAction shootInput;
    public InputAction reloadInput;

    // Gun
    public GameObject grenadeLauncher;
    private GameObject _grenadeSpawn;

    // Ammo
    public GameObject grenadePrefab;
    public int magazineSize;
    public int ammoRemaining { get; private set; }

    // Shooting
    public float power;
    public float initialArcPower;
    public float shootCooldown;
    private float _coolDownTime;

    // Reload
    public float reloadTimer;
    public float reloadDelay;
    private bool _reloadDelayFinished;
    private float _reloadDelayTime;
    private float _dynamicReloadTime;

    // Death Mechanics
    public float onDeathResetDelay;
    private float _onDeathAmmoResetDelay;
    private bool _hasDied;

    // Effects
    private GameObject _muzzleFlashSpawn;
    public ParticleSystem muzzleFlash;

    //Audio
    public AudioClip reloadSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _reloadDelayFinished = true;

        _coolDownTime = 0.0f;
        _reloadDelayTime = 0.0f;

        this.ammoRemaining = this.magazineSize;
        _grenadeSpawn = this.grenadeLauncher.transform.GetChild(0).gameObject;
        _muzzleFlashSpawn = this.grenadeLauncher.transform.GetChild(1).gameObject;

        _onDeathAmmoResetDelay = this.onDeathResetDelay;
        _hasDied = false;

        this.shootInput.performed += OnShoot;
        this.reloadInput.performed += OnReload;
    }

    private void OnEnable()
    {
        this.shootInput.Enable();
        this.reloadInput.Enable();
    }

    private void OnDisable()
    {
        this.shootInput.Disable();
        this.reloadInput.Disable();
    }

    private void Update()
    {
        if(_coolDownTime > 0.0f)
        {
            _coolDownTime -= Time.deltaTime;
        }
        if(this.ammoRemaining <= 0 && !IsInvoking("Reload"))
        {
            _dynamicReloadTime = Mathf.Lerp(0.0f, this.reloadTimer, 1.0f - ((float)(this.ammoRemaining)/(float)(this.magazineSize))) / ((float)(this.magazineSize) - (float)(this.ammoRemaining));
            InvokeRepeating(nameof(Reload), _dynamicReloadTime, _dynamicReloadTime);
        }

        OnDeathAmmoReset();
    }

    private bool canShoot()
    {
        bool canShoot = false;
        if((_coolDownTime <= 0.0f) && this.ammoRemaining > 0 && !IsInvoking("Reload"))
        {
            if(_reloadDelayFinished)
            {
                canShoot = true;
            }
        }
        return canShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if(canShoot())
        {
            GameObject grenade = Instantiate(this.grenadePrefab);
            Rigidbody _rigidbodyGrenade = grenade.GetComponent<Rigidbody>();
            
            Instantiate(muzzleFlash, _muzzleFlashSpawn.transform.position, Camera.main.transform.rotation);
            Vector3 spawnOffset = Camera.main.transform.forward * 0.5f;
            grenade.transform.position = _grenadeSpawn.transform.position + spawnOffset;
            
            ForceMode mode = ForceMode.Impulse;
            //_rigidbodyGrenade.AddForce(Vector3.up * this.initialArcPower, mode);
            _rigidbodyGrenade.AddForce( Camera.main.transform.forward * this.power, mode);
            this.ammoRemaining--;
            _coolDownTime = this.shootCooldown;
            _reloadDelayTime = this.reloadDelay;
        }
    }

    private void OnReload(InputAction.CallbackContext context)
    {
            if(this.ammoRemaining < this.magazineSize && !IsInvoking("Reload"))
            {
                _dynamicReloadTime = Mathf.Lerp(0.0f, this.reloadTimer, 1.0f - ((float)(this.ammoRemaining)/(float)(this.magazineSize))) / ((float)(this.magazineSize) - (float)(this.ammoRemaining));
                InvokeRepeating(nameof(Reload), _dynamicReloadTime, _dynamicReloadTime);
            }

    }

    private void Reload()
    {
        if(this.ammoRemaining < this.magazineSize)
        {
            _audioSource.PlayOneShot(this.reloadSound);
            this.ammoRemaining++;
        }
        else
        {
            FinishReloading();
        }
    }

    private void FinishReloading()
    {
        CancelInvoke();
        _reloadDelayFinished = false;
        Invoke(nameof(ReloadCooldown), this.reloadDelay);     
    }

    private void ReloadCooldown()
    {
        _reloadDelayFinished = true;
    }

    private void OnDeathAmmoReset()
    {
        if(this.transform.localScale == Vector3.zero)
        {
            _hasDied = true;
        }
        if(_hasDied)
        {
            _onDeathAmmoResetDelay -= Time.deltaTime;
        }
        if(_onDeathAmmoResetDelay <= 0.0f)
        {
            this.ammoRemaining = this.magazineSize;
            _hasDied = false;
        }
        if(!_hasDied)
        {
            _onDeathAmmoResetDelay = this.onDeathResetDelay;
        }        
    }
}
