using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyyChracter1AnimatorReceived : MonoBehaviour
{

    public GameObject _HitTrigger;
    public AudioClip _SwordSoundEffect;
    public Transform _SwordPosition;


 
    public void HitTriggerOn()
    {
        _HitTrigger.SetActive(true);
        AudioSource.PlayClipAtPoint(_SwordSoundEffect, _SwordPosition.transform.position, 0.15f);
    }

    public void HitTriggerOff()
    {
        _HitTrigger.SetActive(false);
    }




}
