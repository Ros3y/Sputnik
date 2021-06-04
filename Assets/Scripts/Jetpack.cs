using UnityEngine;
using UnityEngine.InputSystem;

public class Jetpack : MonoBehaviour
{
    // General Components
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    // Jetpacking
    public InputAction jetpackInput;
    public float maxJetpackFuel;
    public float jetpackConstantForce;
    public float fuelIncrementMultiplier;
    public float fuelDecrementMultiplier;
    public float availableJetpackFuel { get; private set; }
    public bool isJetpacking { get; private set; }

    // Effects
    public ParticleSystem smokeTraileffect1;
    public ParticleSystem smokeTraileffect2;
    public ParticleSystem rocketFlame1;
    public ParticleSystem rocketFlame2;

    // Audio
    public AudioSource jetpackAudioSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        this.availableJetpackFuel = this.maxJetpackFuel;
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
        if (this.isJetpacking && !CanJetpack()) {
            StopJetpacking();
        } else if (!this.isJetpacking && CanJetpack()) {
            StartJetpacking();
        }

        UpdateFuel();
    }

    private void UpdateFuel()
    {
        if (this.isJetpacking)
        {
            float decrease = this.fuelDecrementMultiplier * Time.deltaTime;
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel - decrease, 0.0f, this.maxJetpackFuel);
        }
        else if (IsPlayerGrounded())
        {
            float increase = this.fuelIncrementMultiplier * Time.deltaTime;
            this.availableJetpackFuel = Mathf.Clamp(this.availableJetpackFuel + increase, 0.0f, this.maxJetpackFuel);
        }
    }

    // private void FixedUpdate()
    // {
    //     if (this.isJetpacking) {
    //         _rigidbody.AddForce(Vector3.up * this.jetpackConstantForce);
    //     }
    // }

    private void StartJetpacking()
    {
        this.isJetpacking = true;
        this.smokeTraileffect1.Play();
        this.smokeTraileffect2.Play();
        this.rocketFlame1.Play();
        this.rocketFlame2.Play();
        this.jetpackAudioSource.Play();
    }

    private void StopJetpacking()
    {
        this.isJetpacking = false;
        this.smokeTraileffect1.Stop();
        this.rocketFlame1.Stop();
        this.smokeTraileffect2.Stop();
        this.rocketFlame2.Stop();
        this.jetpackAudioSource.Stop();
    }

    private bool CanJetpack()
    {
        return this.jetpackInput.phase == InputActionPhase.Started && this.availableJetpackFuel > 0.0f && !IsPlayerGrounded();
    }

    private bool IsPlayerGrounded()
    {
        return Physics.BoxCast(this.transform.position, _collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, _collider.height /  2);
    }

}
