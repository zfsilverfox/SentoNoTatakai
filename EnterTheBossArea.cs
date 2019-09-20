using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheBossArea : MonoBehaviour
{
    AudioSource _AS1, _BossStageSound;

    public GameObject[] _TheDoorClosed;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            foreach(GameObject DObj in _TheDoorClosed)
            {
                DObj.SetActive(true);
            }


        }
    }









}
