using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventReceived : MonoBehaviour
{

    public GameObject[] HitTrigger;
    public GameObject[] StrongHitTrigger;
    public AudioClip _SwordSound;
    public Transform _SwordPos;


    Animator _anim;


    private void Awake()
    {
        if (_anim == null) _anim = GetComponent<Animator>();

    }


    public void HitTriggerOn()
    {
        foreach (GameObject ST in HitTrigger)
        {
            ST.SetActive(true);
        }
        AudioSource.PlayClipAtPoint(_SwordSound, _SwordPos.transform.position,0.25f);

       // _anim.ResetTrigger("StrongAttack");




    }

public void HitTriggerOff()
    {
        foreach (GameObject ST in HitTrigger)
        {
            ST.SetActive(false);
        }
    }

    public void PlayerStrongAttack()
    {

        AudioSource.PlayClipAtPoint(_SwordSound, _SwordPos.transform.position, 0.35f);
        foreach (GameObject ST in StrongHitTrigger)
        {
            ST.SetActive(true);
        }



    }

}
