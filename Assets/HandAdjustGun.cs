using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class HandAdjustGun : MonoBehaviour
{

    public Transform gunGuard;
    public Transform gunGrip;

    public Animator thisAnimator;
    public bool isLeftHanded;

    public Vector3 rightOffset;
    public Vector3 leftOffset;


    private void OnAnimatorIK(int layerIndex)
    {
        thisAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        thisAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

        if(isLeftHanded == true)
        {
            thisAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gunGrip.position + leftOffset);
            thisAnimator.SetIKRotation(AvatarIKGoal.LeftHand, gunGrip.rotation);

            thisAnimator.SetIKPosition(AvatarIKGoal.RightHand, gunGuard.position + rightOffset);
            thisAnimator.SetIKRotation(AvatarIKGoal.RightHand, gunGuard.rotation);
        }
        else
        {
            thisAnimator.SetIKPosition(AvatarIKGoal.RightHand, gunGrip.position + rightOffset);
            thisAnimator.SetIKRotation(AvatarIKGoal.RightHand, gunGrip.rotation);

            thisAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gunGuard.position + leftOffset);
            thisAnimator.SetIKRotation(AvatarIKGoal.LeftHand, gunGuard.rotation);
        }


    }

}
