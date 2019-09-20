using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{

    private static string FrontHitStringTags = "FrontHit";
    private static string BackHitStringTags = "BackHit";
    private static string  EnemyWeaponStringTags =  "EnemyWeapon";
    private static string EnemyStronggAttackTags = "EnemyStrongAttack";


    


    private static string FrontHitTriggerAnimation = "FrontHitTrigger";
    private static string BackHitTriggerAnimation = "BackHitTrigger";
    private static string StrongAttackTriggerAnimation = "StrongHurtTrigger";



    public PlayerCtrl _PlayerCtrl;
    PlayerStatementSystem _PlayerStatementSystem;
    PlayerAudioManager _PlayerAudioManager;

    bool HasGetHitBF = false;



    public GameObject _FrontHit,_BackHit;




    private void Awake()
    {
        AvoidNULLProblem();
    }

    void AvoidNULLProblem()
    {
        if (_PlayerStatementSystem == null) _PlayerStatementSystem = GetComponentInParent<PlayerStatementSystem>();
        if (_PlayerAudioManager == null) _PlayerAudioManager = GetComponentInParent<PlayerAudioManager>();
        if (_PlayerCtrl == null) _PlayerCtrl = GetComponentInParent<PlayerCtrl>();
    }

    private void Start()
    {
        HasGetHitBF = false;
    }




    void WeakFrontHitAttack()
    {
        if (!HasGetHitBF)
        {
            HasGetHitBF = true;
            StartCoroutine(FrontGETHITTurnBackTOTrue());
            _BackHit.SetActive(false);
        //    Debug.Log("FrontHit");

            _PlayerStatementSystem.CrtHealth -= Random.Range(1, 6);
            if(_PlayerStatementSystem.CrtHealth > 0)
            {
                _PlayerCtrl._anim.SetTrigger(FrontHitTriggerAnimation);
                int randSound = Random.Range(0, _PlayerAudioManager._HurtSound.Length);
                AudioSource.PlayClipAtPoint(_PlayerAudioManager._HurtSound[randSound], transform.position);

            }
            else if(_PlayerStatementSystem.CrtHealth <= 0)
            {
               // Debug.Log("Player Is Dead");
                   _PlayerCtrl.IsDead = true;





            }

        }
    }

    void WeakBackHitAttack()
    {
        if (!HasGetHitBF)
        {
            HasGetHitBF = true;
            StartCoroutine(BackHitTurnBackToTrue());
            _FrontHit.SetActive(false);
        //    Debug.Log("BackHit");
            _PlayerStatementSystem.CrtHealth -= Random.Range(1, 6);
            if (_PlayerStatementSystem.CrtHealth > 0)
            {
                _PlayerCtrl._anim.SetTrigger(BackHitTriggerAnimation);
                int randSound = Random.Range(0, _PlayerAudioManager._HurtSound.Length);
                AudioSource.PlayClipAtPoint(_PlayerAudioManager._HurtSound[randSound], transform.position);
            }
            else if (_PlayerStatementSystem.CrtHealth <= 0)
            {
                Debug.Log("Player Is Dead");
                _PlayerCtrl.IsDead = true;
            }
        }
    }
    
    
    void StrongHitAttackTrigger()
    {
        if (!HasGetHitBF)
        {
         //   Debug.Log("Has Enter The Strong Attack Area");
            HasGetHitBF = true;
            _PlayerStatementSystem.CrtHealth -= Random.Range(11,15);
            if (_PlayerStatementSystem.CrtHealth > 0)
            {
                _PlayerCtrl._anim.SetTrigger(StrongAttackTriggerAnimation);
                int randSound = Random.Range(0, _PlayerAudioManager._StrongHurtSound.Length);
                AudioSource.PlayClipAtPoint(_PlayerAudioManager._StrongHurtSound[randSound], transform.position);
            }
            else if (_PlayerStatementSystem.CrtHealth <= 0)
            {
                Debug.Log("Player Is Dead");
                _PlayerCtrl.IsDead = true;
            }



        }








    }



    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == EnemyWeaponStringTags)
        {
            if(other.gameObject && this.gameObject.tag == FrontHitStringTags)
            {
              
                if (this.gameObject.activeInHierarchy)
                {
                    if (!_PlayerCtrl.IsDead)
                    {
                        if (_PlayerCtrl.CanJump)
                        {
                            if (!_PlayerCtrl.isDash)
                            {
                                if (_PlayerCtrl.CanDoStrongAttackMP)
                                {
                                    if (!_PlayerCtrl.isAttack)
                                    {
                               
                                    Debug.Log("FrontHiTTrigger");
                                    WeakFrontHitAttack();
                                    }
                                }
                            }

                         
                        }
                          
                    }
                          
                }  
            }
            else if(other.gameObject && this.gameObject.tag == BackHitStringTags)
            {
                if (this.gameObject.activeInHierarchy)
                {
                    Debug.Log("BackHiTTrigger");
                    if (!_PlayerCtrl.IsDead)
                        {

                        if (_PlayerCtrl.CanJump)
                            {
                            if (!_PlayerCtrl.isDash)
                            {
                                if (_PlayerCtrl.CanDoStrongAttackMP)
                                {
                                    if (!_PlayerCtrl.isAttack)
                                    {

                                        WeakBackHitAttack();
                                    }
                                }
                            }

                            }
                        }
                }
            }
        }


        if(other.gameObject.tag == EnemyStronggAttackTags)
        {
            if (other.gameObject && this.gameObject.tag == FrontHitStringTags)
            {

                if (this.gameObject.activeInHierarchy)
                {
                    
                    if (_PlayerCtrl.CanDoStrongAttackMP)
                    {
                        if (!_PlayerCtrl.isDash)
                        {
                            Debug.Log("Has Enter The Strong Attack Area");
                            StrongHitAttackTrigger();
                            _BackHit.SetActive(false);
                            StartCoroutine(StrongFrontGETHITTurnBackTOTrue());
                        }   
                    }

                }
            }
            else if (other.gameObject && this.gameObject.tag == BackHitStringTags)
            {
                if (this.gameObject.activeInHierarchy)
                {
                    if (_PlayerCtrl.CanDoStrongAttackMP)
                    {
                        if (!_PlayerCtrl.isDash)
                        {
                            Debug.Log("Has Enter The Strong Attack Area");
                        StrongHitAttackTrigger();
                        _FrontHit.SetActive(false);
                        StartCoroutine(StrongBackGETHITTurnBackTOTrue());

                        }
                        
                    }
                }
            }
        }
    }

    IEnumerator FrontGETHITTurnBackTOTrue()
    {
        yield return new WaitForSeconds(0.25f);
        HasGetHitBF = false;
        _BackHit.SetActive(true);
        StopCoroutine(FrontGETHITTurnBackTOTrue());
    }
    IEnumerator BackHitTurnBackToTrue()
    {
        yield return new WaitForSeconds(0.25f);
        HasGetHitBF = false;
        _FrontHit.SetActive(true);
        StopCoroutine(BackHitTurnBackToTrue());
    }

    IEnumerator StrongFrontGETHITTurnBackTOTrue()
    {
        yield return new WaitForSeconds(3f);
        HasGetHitBF = false;
        _BackHit.SetActive(true);
        StopCoroutine(StrongFrontGETHITTurnBackTOTrue());
    }

    IEnumerator StrongBackGETHITTurnBackTOTrue()
    {
        yield return new WaitForSeconds(3f);
        HasGetHitBF = false;
        _FrontHit.SetActive(true);
        StopCoroutine(StrongBackGETHITTurnBackTOTrue());
    }






}
