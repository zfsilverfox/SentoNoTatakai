using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChrater2HurtCollider : MonoBehaviour
{
    private static string FrontHitTriggerTags = "EnemyFrontHit";
    private static string BackHitTriggerTags = "EnemyBackHit";
    private static string PlayerWeaponStringTrigger = "PlayerWeapon";
    private static string PlayerWeaponStrongTags = "PlayerWeaponStrong";

    private static string GetHitFrontAnimationTrigger = "GetHitFront";
    private static string GetHitBackAnimationTrigger = "GetHitBack";


    Enemy2HealthSystem _EnemyHealthSys;
    EnemyChracter2Ctrl _EnemyChracter2Ctrl;

    public GameObject FrontHitTriggerObj, BackHitTriggerObj;


    bool HasGetHitBf = false;


    private void Awake()
    {
        GetComponentFunction();
    }

    void GetComponentFunction()
    {
        if (_EnemyChracter2Ctrl == null) _EnemyChracter2Ctrl = GetComponentInParent<EnemyChracter2Ctrl>();
        if (_EnemyHealthSys == null) _EnemyHealthSys = GetComponentInParent<Enemy2HealthSystem>();
    }


    private void Start()
    {
        HasGetHitBf = false;
    }



    void FrontHitTrigger()
    {
        HasGetHitBf = true;
        BackHitTriggerObj.SetActive(false);

        _EnemyChracter2Ctrl._Enemy2SoilderState = EnemySoilderStatus.Enemy_GetHurtState;
        _EnemyChracter2Ctrl.GetHurtMaxTimer = Random.Range(0.8f, 1.4f);
        _EnemyChracter2Ctrl.GetHurtCrtTimer = _EnemyChracter2Ctrl.GetHurtMaxTimer;
        _EnemyHealthSys.CrtHealth -= Random.Range(3, 5);

        if(_EnemyHealthSys.CrtHealth > 0)
        {
            _EnemyChracter2Ctrl._anim.SetTrigger(GetHitFrontAnimationTrigger);
        }
      else if(_EnemyHealthSys.CrtHealth <= 0)
        {
            _EnemyChracter2Ctrl.IsDead = true;
           
        }
        StartCoroutine(FrontHitTriggerIEmurator());
    }

    void BackHitTrigger()
    {
        HasGetHitBf = true;
        FrontHitTriggerObj.SetActive(false);


        _EnemyChracter2Ctrl._Enemy2SoilderState = EnemySoilderStatus.Enemy_GetHurtState;

        _EnemyChracter2Ctrl.GetHurtMaxTimer = Random.Range(0.8f, 1.4f);
        _EnemyChracter2Ctrl.GetHurtCrtTimer = _EnemyChracter2Ctrl.GetHurtMaxTimer;
        _EnemyHealthSys.CrtHealth -= Random.Range(3, 5);
        if (_EnemyHealthSys.CrtHealth > 0)
        {
            _EnemyChracter2Ctrl._anim.SetTrigger(GetHitBackAnimationTrigger);
        }
        else if (_EnemyHealthSys.CrtHealth <= 0)
        {
            _EnemyChracter2Ctrl.IsDead = true;

            _EnemyChracter2Ctrl._anim.Play("Death");



        }
        StartCoroutine(BackHitTriggerIEmurator());
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == PlayerWeaponStringTrigger)
        {

            if (this.gameObject.activeInHierarchy)
            {
                  if(this.gameObject.tag == FrontHitTriggerTags && other.gameObject)
                  {

                        if (!HasGetHitBf)
                        {
                            if (!_EnemyChracter2Ctrl.IsDead)
                            {
                                FrontHitTrigger();
                            }
                        }
                  }
                else if(this.gameObject.tag == BackHitTriggerTags && other.gameObject)
                        {
                                if (!HasGetHitBf)
                                {
                                    if (!_EnemyChracter2Ctrl.IsDead)
                                    {
                                                BackHitTrigger();
                                    } 
                                }
                        }
            }
        }


        if(other.gameObject.tag == PlayerWeaponStrongTags)
        {
            if (this.gameObject.activeInHierarchy)
            {
                if (this.gameObject.tag == FrontHitTriggerTags && other.gameObject)
                {

                    if (!HasGetHitBf)
                    {
                        if (!_EnemyChracter2Ctrl.IsDead)
                        {
                            FrontHitTrigger();
                        }
                    }
                }
                else if (this.gameObject.tag == BackHitTriggerTags && other.gameObject)
                {
                    if (!HasGetHitBf)
                    {
                        if (!_EnemyChracter2Ctrl.IsDead)
                        {
                            BackHitTrigger();
                        }
                    }
                }
            }
        }

    }

    IEnumerator FrontHitTriggerIEmurator()
    {

        yield return new WaitForSeconds(0.25f);
        BackHitTriggerObj.SetActive(true);
        HasGetHitBf = false;

       StopCoroutine(FrontHitTriggerIEmurator());
    }
    IEnumerator BackHitTriggerIEmurator()
    {
        yield return new WaitForSeconds(0.25f);
        FrontHitTriggerObj.SetActive(true);
        HasGetHitBf = false;
        StopCoroutine(BackHitTriggerIEmurator());
    }
}
