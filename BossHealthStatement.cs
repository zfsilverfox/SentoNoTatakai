using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthStatement : MonoBehaviour
{

    public int MaxHealth = 300;
    public int CrtHealth;


    BossCtrl _bossCtrl;

    private void Awake()
    {
        if (_bossCtrl == null) _bossCtrl = GetComponent<BossCtrl>();
    }



    private void Start()
    {
        CrtHealth = MaxHealth;
    }

    private void Update()
    {
        if(CrtHealth > 0)
        {
            _bossCtrl.isDead = false;
        }
        else if(CrtHealth <= 0)
        {
            _bossCtrl.isDead = true;
        }
    }
}
