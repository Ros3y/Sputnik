using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }
    private bool isGrounded()
    {
        return Physics.BoxCast(this.transform.position, this._collider.bounds.extents / 2, Vector3.down, out RaycastHit hit, this.transform.rotation, this._collider.height /  2);
    }
}
