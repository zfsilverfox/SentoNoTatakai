using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitTrigger : MonoBehaviour
{
    private static string FrontHitTriggerTags = "EnemyFrontHit";


    private static string PlayerWeaponStringTags = "PlayerWeapon";
    private static string PlayerWeaponStrongTags = "PlayerWeaponStrong";


    private static string GetHitAnimatorTrigger = "GetHit";
    private static string StunAnimatorTrigger = "StunTrigger";




    BossCtrl _bossCtrl;
    BossHealthStatement _BossHealthStatement;

    bool GetHit = false;




    void Awake()
    {
        GetComponentFunction();
    }


    void GetComponentFunction()
    {
        if (_bossCtrl == null) _bossCtrl = GetComponentInParent<BossCtrl>();
        if (_BossHealthStatement == null) _BossHealthStatement = GetComponentInParent<BossHealthStatement>();
    }

    void Start()
    {
        SettingBasicInformation();
    }

    void SettingBasicInformation()
    {
        GetHit = false;
    }


    void WeakHitInStunnedMode()
    {
        DecreaseHealthStatement(15, 20);
      
        if (_BossHealthStatement.CrtHealth <= 0)
        {
            _bossCtrl.isDead = true;
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_DeadStatement;
        }
        else if(_BossHealthStatement.CrtHealth > 0)
        {
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_GetHitStatement;
        }
        StartCoroutine(GETHITTurnBackToFalse(0.2f));
    }


    void WeakHitNormalMode()
    {

        DecreaseHealthStatement(10, 15);
        if (_BossHealthStatement.CrtHealth > 0)
        {
            _bossCtrl._anim.SetTrigger(GetHitAnimatorTrigger);
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_GetHitStatement;
        }
        else
        {
            _bossCtrl.isDead = true;
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_DeadStatement;
        }

        StartCoroutine(GETHITTurnBackToFalse(1.5f));
    }

    void StrongHit()
    {

        DecreaseHealthStatement(15,35);
        if (_BossHealthStatement.CrtHealth > 0)
        {
            _bossCtrl._anim.SetTrigger(StunAnimatorTrigger);
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_StunnedStatement;
           
        }
        else
        {
            _bossCtrl.isDead = true;
            _bossCtrl._bossStatement = BossCtrl.BOSSStatement.Boss_DeadStatement;
        }
        float Rand = Random.Range(0.1f, 0.15f);
        StartCoroutine(GETHITTurnBackToFalse(0.4f));
    }

    void DecreaseHealthStatement(int MinHealthMinus,int MaxHealthMinus)
    {
        _BossHealthStatement.CrtHealth -= Random.Range(MinHealthMinus, MaxHealthMinus);
    }




    private void OnTriggerEnter(Collider other)
    {
      

            if(gameObject.tag == FrontHitTriggerTags && other.gameObject.tag == PlayerWeaponStringTags)
            {
                if (!GetHit)
                {
                    GetHit = true;

                    if (_bossCtrl.isStunned)
                    {
                    Debug.Log("Boss IN Stunned Mode Has Been Hit ");
                        WeakHitInStunnedMode();
                    }
                    else
                    {
                        WeakHitNormalMode();
                    }
                }
            }

         else   if(gameObject.tag == FrontHitTriggerTags && other.gameObject.tag == PlayerWeaponStrongTags)
        {

            if (!GetHit)
            {
                GetHit = true;

                StrongHit();






            }



        }
    }


    IEnumerator GETHITTurnBackToFalse(float Second)
    {
        yield return new WaitForSeconds(Second);
        GetHit = false;
    }





}
