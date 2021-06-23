using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zigurous.Reticles;

public class Shoot : MonoBehaviour
{
    // Inputs
    private Rigidbody _rigidbody;
    public InputAction shootInput;

    // Gun
    public Transform grenadeSpawn;

    // Ammo
    public GameObject grenadePrefab;
   

    // Shooting
    public float power;
    public float initialArcPower;
    public float shootCooldown;
    private float _coolDownTime;

    

    // Death Mechanics
    public float onDeathResetDelay;
    private float _onDeathAmmoResetDelay;

    // Effects
    public Transform muzzleFlashSpawn;
    public ParticleSystem muzzleFlash;
    private ReticleBloom _bloom;

    //Audio
    public AudioClip reloadSound;
    private AudioSource _audioSource;

    private LevelController _levelController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _levelController = this.GetComponent<LevelController>();

       

        _coolDownTime = 0.0f;
      

        _onDeathAmmoResetDelay = this.onDeathResetDelay;

        this.shootInput.performed += OnShoot;
      

        _bloom = FindObjectOfType<ReticleBloom>();
    }

    private void OnEnable()
    {
        this.shootInput.Enable();
    }

    private void OnDisable()
    {
        this.shootInput.Disable();
    }

    private void Update()
    {
        if(_coolDownTime > 0.0f)
        {
            _coolDownTime -= Time.deltaTime;
        }
    }

    private bool canShoot()
    {
        bool canShoot = false;
        if((_coolDownTime <= 0.0f) && !_levelController.isPaused && GlobalControl.Instance.isDead == false)
        {
            canShoot = true; 
        }
        return canShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if(canShoot())
        {
            GameObject grenade = Instantiate(this.grenadePrefab);
            Rigidbody _rigidbodyGrenade = grenade.GetComponent<Rigidbody>();
            
            Instantiate(muzzleFlash, this.muzzleFlashSpawn.position, Camera.main.transform.rotation);
            Vector3 spawnOffset = Camera.main.transform.forward * 0.5f;
            grenade.transform.position = this.grenadeSpawn.position + spawnOffset;
            
            ForceMode mode = ForceMode.Impulse;
            _rigidbodyGrenade.AddForce( Camera.main.transform.forward * this.power, mode);
            _bloom.Apply();
            _coolDownTime = this.shootCooldown;
        }
    }
}
