using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandle : MonoBehaviour
{


    Animator _anim;

    
    public Transform _RightElbow;
    
    public Transform _RightHandPosition;


   
    public float _RightWeight = 1.0f;



    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }






    private void OnAnimatorIK(int layerIndex)
    {

       

        _anim.SetIKHintPosition(AvatarIKHint.RightElbow, _RightElbow.position);
        _anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow,_RightWeight);
        


       

        _anim.SetIKPosition(AvatarIKGoal.RightHand, _RightHandPosition.position);
        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, _RightWeight);

        _anim.SetIKRotation(AvatarIKGoal.RightHand, _RightHandPosition.rotation);
        _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, _RightWeight);

      
    }
}
