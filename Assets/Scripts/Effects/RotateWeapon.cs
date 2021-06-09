using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    private Animator _animator;

    public Transform weapon;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 targetPostion = cameraTransform.position + (cameraTransform.forward * 10.0f);
        Vector3 targetDirection = targetPostion - this.weapon.position;

        this.weapon.rotation = Quaternion.LookRotation(targetDirection);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, this.leftHand.position);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, this.rightHand.position);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    }

}
