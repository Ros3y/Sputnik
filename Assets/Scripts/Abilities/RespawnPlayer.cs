using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zigurous.Tweening;
using Zigurous.CameraSystem;

public class RespawnPlayer : MonoBehaviour
{
    public InputAction respawnPlayerInput;
    private Rigidbody _rigidbody;
    public GameObject deathEffectPosition;
    public float respawnDelay;
    public ParticleSystem respawnEffect;
    public AudioClip respawnSound;
    public ParticleSystem deathEffect;
    public AudioClip deathSound;
    private bool _canRespawn;
    public float respawnCooldown;
    private float _respawnCooldownTime;
    private AudioSource _audioSource;
    public bool isSpawning { get; private set; }
    public bool isDead { get; private set; }
    private Vector2 _orbit;
    private CameraController _controller;
    
    public Transform spawnLocation;

   private void Awake()
   {
        this.respawnPlayerInput.performed += OnRespawn;
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _canRespawn = true;


        this.isSpawning = true;
        this.transform.position = this.spawnLocation.position;
        
        _controller = Camera.main.GetComponent<CameraController>();
        _orbit = new Vector2(this.spawnLocation.eulerAngles.y, this.spawnLocation.eulerAngles.x);
        _controller.look.orbit = _orbit;

        Instantiate(this.respawnEffect, this.spawnLocation.transform.position, this.spawnLocation.transform.rotation);
        _audioSource.PlayOneShot(this.respawnSound);
        _audioSource.KillTweens();
        // this.jetpackSoundSource.TweenVolume(0.0f, 0.25f)
        //                        .OnComplete(() => this.jetpackSoundSource.Stop());
   }

   private void OnEnable()
   {
        this.respawnPlayerInput.Enable();
   }

   private void OnDisable()
   {
        this.respawnPlayerInput.Disable();
   }

   private void Update()
   {
       if(!_canRespawn)
       {
           _respawnCooldownTime -= Time.deltaTime;
       }

       if(_respawnCooldownTime <= 0.0f)
        {
            _canRespawn = true;
            _respawnCooldownTime = this.respawnCooldown;
        }
   }

   private void OnCollisionEnter(Collision collision)
   {
       if(collision.collider.transform.tag == "Death Field")
       {
            Invoke(nameof(Kill), 0.0f);
            Invoke(nameof(Respawn), this.respawnDelay);           
       }
   }


   private void OnRespawn(InputAction.CallbackContext context)
    {
        if(_canRespawn)
        {
            Invoke(nameof(Kill), 0.0f);
            Invoke(nameof(Respawn), this.respawnDelay);
        }
        _canRespawn = false;
    }

    
    private void Kill()
    {
        Instantiate(this.deathEffect, this.deathEffectPosition.transform.position, this.deathEffectPosition.transform.rotation);
        _audioSource.PlayOneShot(this.deathSound);
        _rigidbody.isKinematic = true;
        this.transform.localScale = Vector3.zero;
        _canRespawn = false;
        this.isDead = true;
    }

    private void Respawn()
    {
        _controller.look.orbit = _orbit;
        _rigidbody.isKinematic = false;
        this.transform.localScale = new Vector3(1,1,1);        
        transform.position = this.spawnLocation.transform.position;
        _rigidbody.velocity = Vector3.zero;
        Instantiate(this.respawnEffect, this.spawnLocation.transform.position, this.spawnLocation.transform.rotation);
        _audioSource.PlayOneShot(this.respawnSound);
        this.isDead = false;
        this.isSpawning = true;
    }
}
