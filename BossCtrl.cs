using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossCtrl : MonoBehaviour
{
    private static BossCtrl _instance;
    public static BossCtrl _INSTANCE { get { return _instance; } }


    private static string PlayerTagsString = "Player";
    private static string EnemyLookAtTagsString = "EnemyLookAt";


    private static string DeadAnimationClip = "Dead";
    private static string Attack1TriggerAnimationClip = "AttackTrigger1";
    private static string Attack2TriggerAnimationClip = "AttackTrigger2";
    private static string Attack3TriggerAnimationClip = "AttackTrigger3";
    private static string GetHitAnimationClip = "GetHit";

    [HideInInspector]
  public  Animator _anim;
    CapsuleCollider[] _caps;
    Rigidbody _Rgbd;
    BossHealthStatement _BossHealthStatement;


 public   bool isDead = false;
    [HideInInspector]
    public  bool isStunned = false;
    public bool FoundPlayer = false;
 public   bool DoAttack = false;


    public float AttackCrtTimer;
    public float AttackMaxTimer;









    public GameObject _StrongAttackTrigger;
    public GameObject _AttackTrigger;
    public BoxCollider _LookAtPlayerBoxCollider;

  public  enum BOSSStatement
    {
        Boss_Ilde_Statement,
        Boss_Attack_Statement,
     Boss_StunnedStatement,
     Boss_GetHitStatement,
     Boss_DeadStatement,
    }


    public BOSSStatement _bossStatement;





    private void Awake()
    {
        GetComponentFunction();
    }
    //Function :GetComponentFunction
    // Method : This Function is Mainly used For GetThe Component which is NULL
    void GetComponentFunction()
    {
        if (_instance == null) _instance = this;

        if (_Rgbd == null) _Rgbd = GetComponent<Rigidbody>();
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_caps == null) _caps = GetComponents<CapsuleCollider>();
        if (_BossHealthStatement == null) _BossHealthStatement = GetComponent<BossHealthStatement>();
    }

    private void Start()
    {
        SettingBasicInformation();
    }
    // Function : SettingBasicInformation
    //Method : This Function is Mainly Used For Setting The Basic Information
    void SettingBasicInformation()
    {
        isDead = false;
        isStunned = false;
        DoAttack = false;
        AttackMaxTimer = Random.Range(1.4f, 2.6f);
        AttackCrtTimer = AttackMaxTimer;
        _bossStatement = BOSSStatement.Boss_Ilde_Statement;
    }

    private void Update()
    {
        UpdateBossStatementFunction();
    }

    void UpdateBossStatementFunction()
    {
        switch (_bossStatement)
        {
            case BOSSStatement.Boss_Ilde_Statement:
                SettingIldeUpdateMode();
                break;

            case BOSSStatement.Boss_Attack_Statement:
                SettingBossAttackStatement();
                break;

            case BOSSStatement.Boss_GetHitStatement:
                GetHitStatement();
                break;

            case BOSSStatement.Boss_StunnedStatement:
                SettingStunnedStatement();
                break;
            case BOSSStatement.Boss_DeadStatement:
                SettingDeadStatement();
                break;
        }
    }





    void SettingIldeUpdateMode()
    {
        if (!isDead)
        {
            if (!isStunned)
            {
                if (FoundPlayer)
                {
                    _bossStatement = BOSSStatement.Boss_Attack_Statement;
                }
            }
            else
            {
                _bossStatement = BOSSStatement.Boss_StunnedStatement;
            }
        }
        else
        {
            _bossStatement = BOSSStatement.Boss_DeadStatement;
        }
    }


    void SettingBossAttackStatement()
    {
        if (!isDead)
        {
            if (!isStunned)
            {
                if (FoundPlayer)
                {


                            AttackCrtTimer -= Time.deltaTime;

                        if(AttackCrtTimer <= 0)
                        {
                            DoAttack = true;
                        }



                    if(_BossHealthStatement.CrtHealth >= 250)
                    {

                        if (DoAttack)
                        {
                            int rand = Random.Range(0, 11);

                            if(rand >=0 && rand <= 8)
                            {
                                SettingAttackAnimationClip(Attack1TriggerAnimationClip, 3.5f, 5.4f);
                            }
                            else if(rand >= 9)
                            {
                                int randStrongAttack = Random.Range(0, 11);
                                if(randStrongAttack >=0 && randStrongAttack <= 5)
                                {
                                    SettingAttackAnimationClip(Attack2TriggerAnimationClip, 3.5f, 5.4f);
                                }
                                else if(randStrongAttack >= 6)
                                {
                                    SettingAttackAnimationClip(Attack3TriggerAnimationClip, 3.5f, 5.4f);
                                }
                            }
                        }

                    }
                    else if (_BossHealthStatement.CrtHealth < 250)
                    {
                            if (DoAttack)
                            {
                                int rand = Random.Range(0,11);
                            
                            if(rand >= 0 && rand <= 3)
                            {
                                SettingAttackAnimationClip(Attack1TriggerAnimationClip, 2.4f, 4.6f);
                            }

                            else if(rand >=4 && rand <= 7)
                            {
                                SettingAttackAnimationClip(Attack2TriggerAnimationClip, 2.7f, 4.8f);
                            }

                            else if(rand >= 8)
                            {
                                SettingAttackAnimationClip(Attack2TriggerAnimationClip, 2.7f, 5.4f);
                            }

                            }

                    }

                }
                else
                {

                    _bossStatement = BOSSStatement.Boss_Ilde_Statement;

                }
            }
            else
            {
                _bossStatement = BOSSStatement.Boss_StunnedStatement;
            }
        }
        else
        {
            _bossStatement = BOSSStatement.Boss_DeadStatement;
        }
    }

    void GetHitStatement()
    {
        if (!isDead)
        {
            if (!isStunned)
            {
                if (FoundPlayer)
                {
                    _bossStatement = BOSSStatement.Boss_Attack_Statement;
                }
                else
                {
                    _bossStatement = BOSSStatement.Boss_Ilde_Statement;
                }

            }
            else
            {
                _bossStatement = BOSSStatement.Boss_StunnedStatement;
            }
        }
        else
        {
            _bossStatement = BOSSStatement.Boss_DeadStatement;
        }
    }

    void SettingStunnedStatement()
    {
        if (!isDead)
        {
            if (!isStunned)
            {
                if (FoundPlayer)
                {
                    _bossStatement = BOSSStatement.Boss_Attack_Statement;
                }
                else
                {
                    _bossStatement = BOSSStatement.Boss_Ilde_Statement;
                }
            }
            
        }
        else
        {
            _bossStatement = BOSSStatement.Boss_DeadStatement;
        }
    }

    void SettingDeadStatement()
    {
        if (isDead)
        {
            _anim.Play(DeadAnimationClip);
            
            foreach(var CCollider in _caps)
            {
                CCollider.isTrigger = true;
            }
            _Rgbd.useGravity = false;
            Destroy(this.gameObject, 5f);

        }
    }


    void SettingAttackAnimationClip(string AttackTrigger,float RandMinAttackCounter,float RandMaxAttackCounter)
    {
        _anim.SetTrigger(AttackTrigger);
        DoAttack = false;
        AttackMaxTimer = Random.Range(RandMinAttackCounter, RandMaxAttackCounter);
        AttackCrtTimer = AttackMaxTimer;
    }







    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == PlayerTagsString && _LookAtPlayerBoxCollider.gameObject.tag == EnemyLookAtTagsString) 
        {
            FoundPlayer = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == PlayerTagsString && _LookAtPlayerBoxCollider.gameObject.tag == EnemyLookAtTagsString)
        {
            FoundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == PlayerTagsString && _LookAtPlayerBoxCollider.gameObject.tag == EnemyLookAtTagsString)
        {
            FoundPlayer = false;
        }
    }


    public void StunnedEnterState()
    {
        _anim.ResetTrigger(GetHitAnimationClip);
        _anim.ResetTrigger(Attack1TriggerAnimationClip);
        _anim.ResetTrigger(Attack2TriggerAnimationClip);
        _anim.ResetTrigger(Attack3TriggerAnimationClip);

    }


    public void StunnedUpdateState()
    {
        isStunned = true;
        _StrongAttackTrigger.SetActive(false);
        _AttackTrigger.SetActive(false);


    }

    public void IldeUpdateState()
    {
        isStunned = false;

    }

    public void Attack1ExitState()
    {
        _AttackTrigger.SetActive(false);
        _anim.ResetTrigger(Attack1TriggerAnimationClip);
    }


    public void StrongAttack2ExitState()
    {
        _StrongAttackTrigger.SetActive(false);
        _anim.ResetTrigger(Attack2TriggerAnimationClip);
    }

    public void StrongAttack3ExitState()
    {
        _StrongAttackTrigger.SetActive(false);
        _anim.ResetTrigger(Attack3TriggerAnimationClip);
    }



}
