using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2CharacterAnimatorReceived : MonoBehaviour
{
    public GameObject _Arrow;
    public Transform _ShootingArea;
    public GameObject _ArrowFromAI;

    public AudioClip _ShootSoundEffect;
    





    public void Shoot()
    {
        GameObject InstanArrow = Instantiate(_Arrow, _ShootingArea.position,transform.rotation);
        _ArrowFromAI.SetActive(false);
        AudioSource.PlayClipAtPoint(_ShootSoundEffect, _ShootingArea.transform.position, 0.15f);
    }










}
