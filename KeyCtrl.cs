using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCtrl : MonoBehaviour
{
    public AudioClip _DoorSoundEffect;



    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            if (GameManager1._INSTANCE != null)
                GameManager1._INSTANCE.HasKey = true;
            AudioSource.PlayClipAtPoint(_DoorSoundEffect, transform.position);
            Destroy(this.gameObject);
        }



    }





}
