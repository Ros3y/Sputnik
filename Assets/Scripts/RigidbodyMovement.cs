using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Vector3 _direction;
    private Jetpack _jetpack;
    public Animator animator;
    public InputAction moveInput;
    public InputAction jumpInput;
    public float speed;
    public float arealSpeed;
    public float jumpMagnitude;
    public float maxSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _jetpack = GetComponent<Jetpack>();

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
        this.transform.rotation = rotation;

        Vector3 force = Vector3.zero;

        if (canMoveForward())
        {
            if(isGrounded())
            {
                float forceMultiplier = Mathf.Min(this.speed, Mathf.Abs(maxSpeed - velocityMagnitude));
                force = forward * forceMultiplier;
            }
            else
            {
                float aerialForceMultiplier = Mathf.Min(this.arealSpeed, Mathf.Abs(maxSpeed - velocityMagnitude));
                force = forward * aerialForceMultiplier;
            }
        }

        if (_jetpack.isJetpacking) {
            force += Vector3.up * _jetpack.jetpackConstantForce;
        }

        if (force.sqrMagnitude > 0.0f) {
            _rigidbody.AddForce(force);
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
        return isGrounded();
    }

}
