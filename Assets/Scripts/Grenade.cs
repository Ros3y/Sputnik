using UnityEngine;

public class Grenade : Destructible
{
    private Light _grenadeTimerLight;
    private AudioSource _audioSource;
    private float _lightTimer;
    private bool _hasImpacted;
    private bool _hasDetonated;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _grenadeTimerLight = GetComponentInChildren<Light>();
        _lightTimer = this.detonationDelay;
    }

    private void Update()
    {
        if (_hasImpacted)
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
        if (!_hasDetonated)
        {
            FixedJoint _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = collision.rigidbody;
            _grenadeTimerLight.enabled = true;
            _audioSource.Play();
            _hasImpacted = true;

            if(collision.collider.tag == "Power Core")
            {
                collision.gameObject.GetComponent<Destructible>().Detonate();
                Destroy(this.gameObject);
            }
            else
            {
                Detonate();
            }
        }
    }

    protected override void OnDetonate()
    {
        base.OnDetonate();

        _hasDetonated = true;
    }

}
