using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationReceivedScript : MonoBehaviour
{

    public GameObject _WeakAttackTrigger;
    public GameObject _StrongAttackTrigger;

    public AudioClip _StrongAttackSoundEffectBoss;
    public Transform _StrongAttackStrongAttackPosition;




    public void WeakAttackTriggerOn()
    {
        if (_WeakAttackTrigger != null)
            _WeakAttackTrigger.SetActive(true);
        else Debug.LogError("Please Add The Something Bf Continue The Game");
    }

    public void WeakAttackTriggerOff()
    {
        if (_WeakAttackTrigger != null)
            _WeakAttackTrigger.SetActive(false);
        else Debug.LogError("Please Add The Something Bf Continue The Game");

    }

    public void StrongAttackTriggerOn()
    {
        if (_StrongAttackTrigger != null) _StrongAttackTrigger.SetActive(true);
        else Debug.LogError("That's Something You need To Do");
    }

    public void StrongAttackTriggerOff()
    {
        if (_StrongAttackTrigger != null) _StrongAttackTrigger.SetActive(false);
        else Debug.LogError("That's Something You need To Do");

        AudioSource.PlayClipAtPoint(_StrongAttackSoundEffectBoss, _StrongAttackStrongAttackPosition.transform.position);
    }




}
