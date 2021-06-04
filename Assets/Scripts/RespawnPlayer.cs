using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnPlayer : MonoBehaviour
{
    public InputAction respawnPlayerInput;
    private Vector3 _initialPosition;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    public GameObject _spawnEffectPosition;
    public float respawnDelay;
    private ParticleSystem _respawnEffect;
    public AudioClip respawnSound;
    public ParticleSystem deathEffect;
    public AudioClip deathSound;
    private bool _canRespawn;
    public float respawnCooldown;
    private float _respawnCooldownTime;

   private void Awake()
   {
        this.respawnPlayerInput.performed += OnRespawn;
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _initialPosition = transform.position;
        _respawnEffect = _spawnEffectPosition.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        _canRespawn = true;
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
        Instantiate(this.deathEffect, _spawnEffectPosition.transform.position, _spawnEffectPosition.transform.rotation);
        _audioSource.PlayOneShot(this.deathSound);
        _rigidbody.isKinematic = true;
        this.transform.localScale = Vector3.zero;
    }

    private void Respawn()
    {
        _rigidbody.isKinematic = false;
        this.transform.localScale = new Vector3(1,1,1);
        transform.position = _initialPosition;
        _rigidbody.velocity = Vector3.zero;
        _respawnEffect.Play();
        _audioSource.PlayOneShot(this.respawnSound);
    }
}
