using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChracter1HitCollider : MonoBehaviour
{
    private static string FrontHitTriggerTags = "EnemyFrontHit";
    private static string BackHitTriggerTags = "EnemyBackHit";
    private static string PlayerWeaponStringTags = "PlayerWeapon";
    private static string PlayerWeaponStrongTags = "PlayerWeaponStrong";

   private static string GetHitFrontTriggerString = "GetHitFront";
    private static string GetHitBackTriggerString = "GetHitBack";




    EnemyChracter1Ctrl _EnemyChracter1Ctrl;
    EnemyChracter1HealthSystem _EnemyChracter1HealthSys;


    public GameObject _FrontHitObj, _BackHitObj;

    bool HasBeenAttackBf = false;


    private void Awake()
    {
        getComponentFunction();
    }

    void getComponentFunction()
    {
        if (_EnemyChracter1Ctrl == null) _EnemyChracter1Ctrl = GetComponentInParent<EnemyChracter1Ctrl>();
        if (_EnemyChracter1HealthSys == null) _EnemyChracter1HealthSys = GetComponentInParent<EnemyChracter1HealthSystem>();
    }


    private void Start()
    {
        SettingBasicInformation();
    }

    void SettingBasicInformation()
    {
        HasBeenAttackBf = false;
    }



    void WeakHitFront()
    {
        if (!HasBeenAttackBf)
        {

            DamageEarnFunction(GetHitFrontTriggerString, 3, 5,_BackHitObj);
            StartCoroutine(AttackHitIEmurator(_BackHitObj, 1.5f));
        }
    }

        void WeakHitBack()
    {
        if (!HasBeenAttackBf)
        {

            DamageEarnFunction(GetHitBackTriggerString, 3, 5,_FrontHitObj);
            StartCoroutine(AttackHitIEmurator(_FrontHitObj, 1.5f));
        }
    }

    void StrongAttackFront()
    {
        if (!HasBeenAttackBf)
        {
            Debug.Log("GET Strong Hit Front  ");
            DamageEarnFunction(GetHitFrontTriggerString,5, 10,_BackHitObj);
         StartCoroutine(   AttackHitIEmurator(_BackHitObj, 3f));
        }
    }

    void StrongAttackBack()
    {
        if (!HasBeenAttackBf)
        {
            Debug.Log("GET Strong Hit Back  ");
            DamageEarnFunction(GetHitBackTriggerString, 5, 10,_FrontHitObj);
        StartCoroutine(    AttackHitIEmurator(_BackHitObj, 3f));
        }
    }

    void DamageEarnFunction(string HitTriggerName,int MinHeatlhHurt,int MaxHealthHurt , GameObject _HitTriggerObj)
    {
       
            _EnemyChracter1Ctrl._anim.SetTrigger(HitTriggerName);

            HasBeenAttackBf = true;

        _HitTriggerObj.SetActive(false);

            _EnemyChracter1HealthSys.CrtHealth -= Random.Range(MinHeatlhHurt , MaxHealthHurt);
   
        if(_EnemyChracter1HealthSys.CrtHealth > 0)
        {
            _EnemyChracter1Ctrl._EnemySoilderStatus = EnemySoilderStatus.Enemy_GetHurtState;
        }
        else if(_EnemyChracter1HealthSys.CrtHealth <= 0)
        {
            _EnemyChracter1Ctrl.IsDead = true;
            _EnemyChracter1Ctrl._EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PlayerWeaponStringTags)
        {
            if (this.gameObject.activeInHierarchy)
            {
                if(this.gameObject.tag == FrontHitTriggerTags && other.gameObject)
                {
                  //  Debug.Log("GET Hit Front ");
                    WeakHitFront();
                }
                else if(this.gameObject.tag == BackHitTriggerTags && other.gameObject)
                {
                 //   Debug.Log("GET Hit Back ");
                    WeakHitBack();
                }
            }
        }
        if (other.gameObject.tag == PlayerWeaponStrongTags)
        {
            if (this.gameObject.tag == FrontHitTriggerTags && other.gameObject)
            {
               // Debug.Log("GET Strong Hit Front  ");
                StrongAttackFront();
            }
            else if (this.gameObject.tag == BackHitTriggerTags && other.gameObject)
            {
            //    Debug.Log("GET Strong Hit Back ");
                StrongAttackBack();

            }
        }
    }

    IEnumerator AttackHitIEmurator( GameObject _HitTrigger,float Timer)
    {
        yield return new WaitForSeconds(Timer);
        HasBeenAttackBf = false;
        _HitTrigger.SetActive(true);
        StopCoroutine(AttackHitIEmurator(_HitTrigger, Timer));
    }

    
}
