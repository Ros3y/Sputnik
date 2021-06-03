using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public Animator animator;
    private Rigidbody _rigidbody;
    public InputAction dashInput;
    public float dashSpeed;
    public float groundedDashCooldown;
    private float _coolDownTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _coolDownTime = 0.0f;
        this.dashInput.performed += OnDash;
    }

    private void OnEnable()
    {
        this.dashInput.Enable();
    }

    private void OnDisable()
    {
        this.dashInput.Disable();
    }

    private void Update()
    {
        if(_coolDownTime > 0.0f)
        {
            _coolDownTime -= Time.deltaTime;
        }
    }

    private bool canDash()
    {
        bool canDash = false;
        if((_coolDownTime <= 0.0f))
        {
            canDash = true;
        }
        return canDash;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        ForceMode mode = ForceMode.Impulse;
        if(canDash())
        {
            _rigidbody.AddForce(Camera.main.transform.forward * this.dashSpeed, mode);
            this.animator.SetTrigger("Dash");
            _coolDownTime = groundedDashCooldown;
        }
    }
}
