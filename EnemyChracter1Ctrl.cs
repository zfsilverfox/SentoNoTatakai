using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyChracter1Ctrl : MonoBehaviour
{
    private static string PlayerTagsString = "Player";
    private static string EnemyLookAtTagsString = "EnemyLookAt";


    private static string DeadAnimationClip = "Death";
    private static string WalkingSpeedFloatAnimator = "Spd";
    private static string Attack1TriggerAnimator = "Attack1Trigger";
    private static string Attack2TriggerAnimator = "Attack2Trigger";
    private static string Attack3TriggerAnimator = "Attack3Trigger";



    [HideInInspector]
  public  Animator _anim;
    Rigidbody _rgbd;
    CapsuleCollider _Cap;
    NavMeshAgent _nav;



 public   EnemySoilderStatus _EnemySoilderStatus;

    public bool HasFoundPlayer = false;
    public bool IsDead = false;
    bool HasBeenCountBf = false;

    



    public BoxCollider _PlayerLookAtBoxCollider;


    float WaitingCrtTimer = 0;
    public float MaxCrtWaitingTimer;

    float AttackCrtTimer = 0;
    public float MaxAttackTimer;
    bool DoAttack = false;


    public GameObject FoundLight;
    public GameObject EnemyNormalLight;
    public GameObject _SwordTrigger;

    public Transform[] _WayPoints;
    int WayPointCount = 0;







    private void Awake()
    {
        GetComponentFunction();
    }
    //Function :GetComponentFunction
    // Method : This Function is Mainly used For GetThe Component which is NULL
    void GetComponentFunction()
    {
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
        if (_Cap == null) _Cap = GetComponent<CapsuleCollider>();
        if (_nav == null) _nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SettingBasicInformation();
    }


    // Function : SettingBasicInformation
    // Method : This Function is Mainly used For Setting The Basic Statement 
    void SettingBasicInformation()
    {
        IsDead = false;
        HasFoundPlayer = false;
        HasBeenCountBf = false;


        MaxCrtWaitingTimer = Random.Range(3.5f,5f);
        WaitingCrtTimer = MaxCrtWaitingTimer;
        MaxAttackTimer = Random.Range(0.8f, 1.2f);
        AttackCrtTimer = MaxAttackTimer;
        DoAttack = false;
        WayPointCount = 0;
        FoundLight.SetActive(false);
    }



    private void Update()
    {
        UpdateAIStatementInfo();


        if (HasFoundPlayer)

        {
            FoundLight.SetActive(true);
        }
        else
        {
            FoundLight.SetActive(false);
        }
    }

    //Function :UpdateAIStatementInfo
    // Method : This Function is Mainly to used For Update The Ai Statement Function
    void UpdateAIStatementInfo()
    {
        switch (_EnemySoilderStatus)
        {
            case EnemySoilderStatus.Enemy_IldeState:
                EnemyIldeStateFunction();
                break;
            case EnemySoilderStatus.Enemy_WalkingState:
                EnemyWalkingStateFunction();
                break;
            case EnemySoilderStatus.Enemy_FoundPlayerState:
                EnemyFoundPlayerState();
                break;
            case EnemySoilderStatus.Enemy_AttackPlayerState:
                EnemyAttackPlayerState();
                break;
            case EnemySoilderStatus.Enemy_GetHurtState:
                EnemyGetHurtStatement();
                break;
            case EnemySoilderStatus.Enemy_DeadState:
                EnemyDeadStatement();
                break;
        }
    }


    // Function : EnemyIldeStateFunction
    // Method : This Function is Use for Update When Enemy is At The Ilde Statement
    void EnemyIldeStateFunction()
    {
        if (!IsDead)
        {
            if (!HasFoundPlayer)
            {
                _nav.stoppingDistance = 0.0f;
                _anim.SetFloat(WalkingSpeedFloatAnimator, 0f);
                WaitingCrtTimer -= Time.deltaTime;
                if (WaitingCrtTimer <= 0)
                {
                    _nav.SetDestination(_WayPoints[WayPointCount].position);
                    _EnemySoilderStatus = EnemySoilderStatus.Enemy_WalkingState;
                }

            }
            else
            {
                _EnemySoilderStatus = EnemySoilderStatus.Enemy_FoundPlayerState;
            }
        }
        else
        {
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }
    }


    // Function : EnemyWalkingStateFunction
    // Method : This Function is Mainly used For Update The Statement
   // When The AI Is At The Walking Statement
    void EnemyWalkingStateFunction()
    {
        if (!IsDead)
        {
            if (!HasFoundPlayer)
            {
                _nav.stoppingDistance = 0.0f;

                if(_nav.remainingDistance >=0.05f)
                {
                    if (_nav.remainingDistance >= 2f)
                    {
                        _nav.speed = 5f;
                        _anim.SetFloat(WalkingSpeedFloatAnimator, Mathf.Lerp(_anim.GetFloat(WalkingSpeedFloatAnimator), 1f, 0.5f));
                    }
                    else if (_nav.remainingDistance < 2f)
                    {
                        _nav.speed = 3f;
                        _anim.SetFloat(WalkingSpeedFloatAnimator, Mathf.Lerp(_anim.GetFloat(WalkingSpeedFloatAnimator), 0.5f, 0.5f));
                    }
                }
                else if(_nav.remainingDistance <= 0.05f)
                {

                    MaxCrtWaitingTimer = Random.Range(2.5f, 4f);
                    WaitingCrtTimer = MaxCrtWaitingTimer;
                    _EnemySoilderStatus = EnemySoilderStatus.Enemy_IldeState;
                    if (WayPointCount < _WayPoints.Length)
                    {
                        WayPointCount++;
                        if (WayPointCount >= _WayPoints.Length)
                        {
                            WayPointCount = 0;
                        }
                    }
                   
                }
            }
            else
            {
                _EnemySoilderStatus = EnemySoilderStatus.Enemy_FoundPlayerState;
            }
        }
        else
        {
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }
    }


    // Function : EnemyFoundPlayerState
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Found Player Statement
    void EnemyFoundPlayerState()
    {
      

        if (!IsDead)
        {
            if (HasFoundPlayer)
            {
              _nav.stoppingDistance = 1.65f;
                if(PlayerCtrl._INSTANCE != null)
                {
                    float WithPlayerDistance = Vector3.Distance(this.transform.position, PlayerCtrl._INSTANCE.transform.position);
                
                    _nav.SetDestination(PlayerCtrl._INSTANCE.transform.position);
                    //  _EnemySoilderStatus = EnemySoilderStatus.Enemy_WalkingState;
                    if (_nav.remainingDistance >= 1.65f)
                    {
                        if (_nav.remainingDistance >= 2f)
                        {
                            _nav.speed = 5f;
                            _anim.SetFloat(WalkingSpeedFloatAnimator, Mathf.Lerp(_anim.GetFloat(WalkingSpeedFloatAnimator), 1f, 0.5f));
                        }
                        else if (_nav.remainingDistance < 2f)
                        {
                            _nav.speed = 3f;
                            _anim.SetFloat(WalkingSpeedFloatAnimator, Mathf.Lerp(_anim.GetFloat(WalkingSpeedFloatAnimator), 0.5f, 0.5f));
                        }
                    }
                    else
                    {
                        _anim.SetFloat(WalkingSpeedFloatAnimator, 0f);
                        _EnemySoilderStatus = EnemySoilderStatus.Enemy_AttackPlayerState; ;
                    }
                } 
            }
            else
            {
                _EnemySoilderStatus = EnemySoilderStatus.Enemy_IldeState;
            }
        }
        else
        {
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }
    }

    // Function : EnemyAttackPlayerState
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Attack Player Statement
    void EnemyAttackPlayerState()
    {
        if (!IsDead)
        {
            if (!PlayerCtrl._INSTANCE.IsDead)
            {
                if (HasFoundPlayer)
                {
                    if (_nav.remainingDistance >= 1.65f)
                    {
                        _EnemySoilderStatus = EnemySoilderStatus.Enemy_FoundPlayerState;
                    }
                    else if(_nav.remainingDistance <= 1.65f)
                    {
                        AttackCrtTimer -= Time.deltaTime;
                        if(AttackCrtTimer <= 0)
                        {
                            DoAttack = true;
                        }

                        if (DoAttack)
                        {
                            int Rand = Random.Range(0,9);

                            if(Rand >=0 && Rand <= 2)
                            {
                                _anim.SetTrigger(Attack1TriggerAnimator);
                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);


                                DoAttack = false;
                                MaxAttackTimer = Random.Range(1.25f, 2f);
                                AttackCrtTimer = MaxAttackTimer;
                                
                            }
                            else if(Rand >= 3 && Rand <= 5)
                            {
                                _anim.SetTrigger(Attack2TriggerAnimator);
                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);


                                DoAttack = false;
                                MaxAttackTimer = Random.Range(1.25f, 2f);
                                AttackCrtTimer = MaxAttackTimer;
                            }
                            else if(Rand >=6)
                            {
                                _anim.SetTrigger(Attack3TriggerAnimator);

                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

                                DoAttack = false;
                                MaxAttackTimer = Random.Range(1.25f, 2f);
                                AttackCrtTimer = MaxAttackTimer;
                            }
                        }
                    }
                }
            else
            {
                _EnemySoilderStatus = EnemySoilderStatus.Enemy_IldeState;
            }
            }
            else
            {
                HasFoundPlayer = false;
            }

          
        }
        else
        {
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }


    }

    // Function :EnemyGetHurtStatement
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Get Hurt  Statement
    void EnemyGetHurtStatement()
    {
        if (!IsDead)
        {
            HasFoundPlayer = true;
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_FoundPlayerState;
        }
        else
        {
            _EnemySoilderStatus = EnemySoilderStatus.Enemy_DeadState;
        }
    }

    // Function :EnemyDeadStatement
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Dead  Statement
    void EnemyDeadStatement()
    {
        if (IsDead)
        {
            _anim.Play(DeadAnimationClip);
            _rgbd.useGravity = false;
            _Cap.isTrigger = true;
            _nav.isStopped = true;
            _anim.SetFloat(WalkingSpeedFloatAnimator,0f);
            _SwordTrigger.SetActive(false);
            Destroy(this.gameObject, 15f);
            FoundLight.SetActive(false);

            if (!HasBeenCountBf)
            {
                HasBeenCountBf = true;
                if (GameManager1._INSTANCE != null)
                {
                    GameManager1._INSTANCE.EnemyKillCounter ++;
                }

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == PlayerTagsString && _PlayerLookAtBoxCollider.gameObject.tag == EnemyLookAtTagsString)
        {
            HasFoundPlayer = true;
        }


    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == PlayerTagsString && _PlayerLookAtBoxCollider.gameObject.tag == EnemyLookAtTagsString)
        {
            HasFoundPlayer = true;
        }


    }






    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == PlayerTagsString && _PlayerLookAtBoxCollider.gameObject.tag == EnemyLookAtTagsString)
        {
             HasFoundPlayer = false;
           
        }
    }

  
    public void Attack1Exitstate()
    {
        _anim.ResetTrigger(Attack1TriggerAnimator);
        _SwordTrigger.SetActive(false);
    }

    public void Attack2Exitstate()
    {
        _anim.ResetTrigger(Attack2TriggerAnimator);
        _SwordTrigger.SetActive(false);
    }

    public void Attack3Exitstate()
    {
        _anim.ResetTrigger(Attack3TriggerAnimator);
        _SwordTrigger.SetActive(false);
    }



}
