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
    private Vector3 _direction;
    private Jetpack _jetpack;
    public float speed;
    public float arealSpeed;
    public float jumpMagnitude;
    public float maxSpeed;
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    public Animator animator;
    public float quadraticDragCoefficient;
    public float maxDrag;
    private bool _isJumping;
    private Vector2 _velocity;
    private Vector2 _animationDirection;

    public LayerMask layerMask;
    private LevelController _levelController;

    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _jetpack = GetComponent<Jetpack>();
        _levelController = GetComponent<LevelController>();

        this.jumpInput.performed += OnJump; 
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;     
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

        _animationDirection = Vector2.SmoothDamp(_animationDirection, input, ref _velocity, 0.2f);



        _direction.x = input.x;
        _direction.z = input.y;


        this.animator.SetFloat("Speed", _rigidbody.velocity.sqrMagnitude);
        this.animator.SetBool("Grounded", isGrounded());
        this.animator.SetFloat("xDirection", _animationDirection.x);
        this.animator.SetFloat("yDirection", _animationDirection.y);

        if( this.quadraticDragCoefficient * (_rigidbody.velocity.sqrMagnitude/2) < this.maxDrag)
        {
            _rigidbody.drag = this.quadraticDragCoefficient * (_rigidbody.velocity.sqrMagnitude/2);
        }
        else if(this.quadraticDragCoefficient * (_rigidbody.velocity.sqrMagnitude/2) > this.maxDrag)
        {
            _rigidbody.drag = this.maxDrag;
        }

        if(isGrounded())
        {
            _rigidbody.drag = 4.0f;
            _collider.height = 2.0f;              
        }
        else
        {
            _collider.material.dynamicFriction = 0.4f;
            _collider.height = 1.5f;        
        }
    }

    private void FixedUpdate()
    {
        if(!_levelController.isPaused)
        {
            Move();
        }    
    }

    private void Move()
    {
         float yaw = Camera.main.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.AngleAxis(yaw, Vector3.up);
        Vector3 forward = rotation * _direction.normalized;

        float velocityMagnitude = _rigidbody.velocity.sqrMagnitude;

        this.transform.rotation = rotation;

        Vector3 force = Vector3.zero;
        ForceMode mode = ForceMode.Impulse;


        if(isGrounded() && canMoveForward())
        {
            float forceMultiplier = Mathf.Min(this.speed, Mathf.Abs(maxSpeed - velocityMagnitude));
            force = forward * forceMultiplier;    
        }
        else
        {
            float aerialForceMultiplier = Mathf.Min(this.arealSpeed, Mathf.Abs(maxSpeed - velocityMagnitude));
            force = forward * aerialForceMultiplier; 
        }

        if(_jetpack.isJetpacking)
        {
            force += (Vector3.up * _jetpack.jetpackConstantForce) + (Camera.main.transform.forward * (_jetpack.jetpackConstantForce/1.5f));        
        }

        if(_isJumping)
        {
            force += (Vector3.up * jumpMagnitude);
            _isJumping = false;
        }

        if(force.sqrMagnitude > 0.0f)
        {
            _rigidbody.AddForce(force, mode);
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
        _isJumping = false;
        if(canJump() && !context.canceled)
        {
            _isJumping = true;    
        }
        else
        {
            _isJumping = false;
        }
    }

    private bool isGrounded()
    {
        return Physics.BoxCast(this.transform.position, this._collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, this._collider.height /  2, layerMask);
    }

    private bool canJump()
    {
        bool canJump = false;
        if(isGrounded() && !_isJumping)
        {
            canJump = true;
        }
        return canJump;
    }
}
