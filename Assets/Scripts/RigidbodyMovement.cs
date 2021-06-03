using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RigidbodyMovement : MonoBehaviour
{
    public InputAction moveInput;
    public InputAction jumpInput;
    public float speed;
    public float arealSpeed;
    public float jumpMagnitude;
    public float maxSpeed;
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Vector3 _direction;
    private Transform _transform;
    public Animator animator;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.jumpInput.performed += OnJump; 

    }

    private void OnEnable()
    {
        this.moveInput.Enable();
        this.jumpInput.Enable();
    }

    private void OnDisable()
    {
        this.moveInput.Disable();
        this.jumpInput.Disable();
    }

    private void Update()
    {
        Vector2 input = this.moveInput.ReadValue<Vector2>();

        _direction.x = input.x;
        _direction.z = input.y;
        this.animator.SetFloat("Speed", _rigidbody.velocity.sqrMagnitude);
        this.animator.SetBool("Grounded", isGrounded());
        
    }

    private void FixedUpdate()
    {
        float yaw = Camera.main.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.AngleAxis(yaw, Vector3.up);
        Vector3 forward = rotation * _direction.normalized;
        float velocityMagnitude = _rigidbody.velocity.sqrMagnitude;
        ForceMode mode = ForceMode.Force;
        float forceMultiplier = Mathf.Min(this.speed, Mathf.Abs(maxSpeed - velocityMagnitude));
        float arealForceMultiplier = Mathf.Min(this.arealSpeed, Mathf.Abs(maxSpeed - velocityMagnitude));
        this.transform.rotation = rotation;

        if(canMoveForward())
        {
            if(isGrounded())
            {
                _rigidbody.AddForce(forward * forceMultiplier, mode);    
            }

            else
            {
                _rigidbody.AddForce(forward * arealForceMultiplier, mode); 
            }
        }
   
    }

    private bool canMoveForward()
    {
        bool canMoveForward = true;
        Vector2 input = this.moveInput.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0.0f, input.y);

        float yaw = Camera.main.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0.0f, yaw, 0.0f);
        direction = rotation * direction;

        if(_rigidbody.velocity.sqrMagnitude >= maxSpeed)
        {
            // dot product of 1 means the two vectors have the same direction, -1 means exactly opposite
            if(Vector3.Dot(_rigidbody.velocity, direction) > 0.98)
            {
                canMoveForward = false;
            }
            
            else
            {
                canMoveForward = true;
            }
        }

        return canMoveForward;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(canJump())
        {
            ForceMode mode = ForceMode.Impulse;
            _rigidbody.AddForce(Vector3.up * jumpMagnitude, mode);
        }
    }

    private bool isGrounded()
    {
        return Physics.BoxCast(this.transform.position, this._collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, this._collider.height /  2);
    }

    private bool canJump()
    {
        bool canJump = false;
        if(isGrounded())
        {
            canJump = true;
        }
        return canJump;
    }
}
