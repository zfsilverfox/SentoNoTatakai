using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySoilderStatus
{
    Enemy_IldeState,
    Enemy_WalkingState,
    Enemy_FoundPlayerState,
    Enemy_WalkingToPlayerState,
    Enemy_AttackPlayerState,
    Enemy_GetHurtState,
    Enemy_DeadState,
}





public class EnemyChracter2Ctrl : MonoBehaviour
{

    private static string DeathAnimationClip = "Death";
    private static string Attack1TriggerAnimator = "AttackTrigger1";
    private static string Attack2TriggerAnimator = "AttackTrigger2";
    private static string Attack3TriggerAnimator = "AttackTrigger3";



    [HideInInspector]
   public Rigidbody _rgbd;
    [HideInInspector]
   public CapsuleCollider _cap;

    [HideInInspector]
  public  Animator _anim;

  
public    bool IsDead = false;

    public EnemySoilderStatus _Enemy2SoilderState;


    public GameObject _Model;
    public GameObject _ArrowModel;


    bool CanAttack;
    bool DoAttack;

    bool HasBeenCountedBf = false;

    public float CanAttackCrtTimer ;
    public float CanAttackMaxTimer;


    public float GetHurtMaxTimer;
    public float GetHurtCrtTimer;



    private Vector3 _ThrustVector;




    private void Awake()
    {
        GetComponentFunction();
    }
    //Function :GetComponentFunction
    // Method : This Function is Mainly used For GetThe Component which is NULL
    void GetComponentFunction()
    {
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_cap == null) _cap = GetComponent<CapsuleCollider>();

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
        _Enemy2SoilderState = EnemySoilderStatus.Enemy_IldeState;
        CanAttack = false;
        DoAttack = false;
        HasBeenCountedBf = false;
        CanAttackMaxTimer = Random.Range(3f,4.5f);
        CanAttackCrtTimer = CanAttackMaxTimer;
        GetHurtMaxTimer = Random.Range(0.6f, 1.4f);
        GetHurtCrtTimer = GetHurtMaxTimer;

    }
         




    private void Update()
    {
        UpdateAIStatementInfo();
    }


    private void FixedUpdate()
    {
        _rgbd.velocity = new Vector3(_rgbd.velocity.x, _rgbd.velocity.y, _rgbd.velocity.z);
        _ThrustVector = Vector3.zero;
    }


    //Function :UpdateAIStatementInfo
    // Method : This Function is Mainly to used For Update The Ai Statement Function
    void UpdateAIStatementInfo()
    {
        switch (_Enemy2SoilderState)
        {
            case EnemySoilderStatus.Enemy_IldeState:

                EnemyIldeState();
                break;
            case EnemySoilderStatus.Enemy_AttackPlayerState:

                EnemyAttackPlayerState();
                break;
            case EnemySoilderStatus.Enemy_GetHurtState:
                GetHurtFunction();


                break;
            case EnemySoilderStatus.Enemy_DeadState:
                DeadFunction();


                break;
        }
    }


    // Function : EnemyIldeState
    // Method : This Function is Use for Update When Enemy is At The Ilde Statement
    private void EnemyIldeState()
    {
        if (!IsDead)
        {
            if (PlayerCtrl._INSTANCE != null)
            {
                float WithPlayerDistance = Vector3.Distance(transform.position, PlayerCtrl._INSTANCE.transform.position);


                if (WithPlayerDistance <= 15)
                {
                    _Enemy2SoilderState = EnemySoilderStatus.Enemy_AttackPlayerState;
                }
            }
        }
        else
        {
            _Enemy2SoilderState = EnemySoilderStatus.Enemy_DeadState;
        }
    }


    // Function : EnemyAttackPlayerState
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Attack Player Statement
    void EnemyAttackPlayerState()
    {
        if (!IsDead)
        {
            CanAttackCrtTimer -= Time.deltaTime;

            if (PlayerCtrl._INSTANCE != null)
            {



                if (!PlayerCtrl._INSTANCE.IsDead)
                {
                    float WithPlayerDistance = Vector3.Distance(transform.position, PlayerCtrl._INSTANCE.transform.position);
                    if (WithPlayerDistance <= 25f)
                    {
                        if (CanAttackCrtTimer <= 0)
                        {
                            CanAttack = true;
                        }

                        if (CanAttack)
                        {
                            int rand = Random.Range(0, 10);

                            if (rand >= 0 && rand <= 2)
                            {

                                CanAttackMaxTimer = Random.Range(4.5f, 6f);
                                CanAttackCrtTimer = CanAttackMaxTimer;
                                CanAttack = false;
                                _anim.SetTrigger(Attack1TriggerAnimator);
                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                                Debug.Log("Attack1 ");
                            }
                            else if (rand >= 3 && rand <= 5)
                            {

                                CanAttackMaxTimer = Random.Range(4.5f, 6f);
                                CanAttackCrtTimer = CanAttackMaxTimer;
                                CanAttack = false;
                                _anim.SetTrigger(Attack2TriggerAnimator);
                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);


                               
                            }
                            else if (rand >= 6)
                            {

                                CanAttackMaxTimer = Random.Range(4.5f, 6f);
                                CanAttackCrtTimer = CanAttackMaxTimer;
                                CanAttack = false;
                                _anim.SetTrigger(Attack3TriggerAnimator);
                                transform.LookAt(PlayerCtrl._INSTANCE.transform.position, Vector3.up);
                                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
                                
                            }
                        }
                    }
                    else if (WithPlayerDistance > 25f)
                    {
                        _Enemy2SoilderState = EnemySoilderStatus.Enemy_IldeState;
                    }



                }
                else
                {
                    _Enemy2SoilderState = EnemySoilderStatus.Enemy_IldeState;
                }


           



            }


        }
        else
        {
            _Enemy2SoilderState = EnemySoilderStatus.Enemy_DeadState;
        }





    }



    // Function :GetHurtFunction
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Get Hurt  Statement
    void GetHurtFunction()
    {
        if (!IsDead)
        {
            GetHurtCrtTimer -= Time.deltaTime;

            if (GetHurtCrtTimer <= 0)
            {
                if(PlayerCtrl._INSTANCE != null)
                {
                    if (!PlayerCtrl._INSTANCE.IsDead)
                    {
                        float WithPlayerDistance = Vector3.Distance(transform.position, PlayerCtrl._INSTANCE.transform.position);

                        if(WithPlayerDistance <= 25f)
                        {
                            _Enemy2SoilderState = EnemySoilderStatus.Enemy_AttackPlayerState;
                            GetHurtMaxTimer = Random.Range(0.8f, 1.6f);
                            GetHurtCrtTimer = GetHurtMaxTimer;
                        }
                        else if(WithPlayerDistance >= 25f)
                        {
                            _Enemy2SoilderState = EnemySoilderStatus.Enemy_IldeState;
                            GetHurtMaxTimer = Random.Range(0.8f, 1.6f);
                            GetHurtCrtTimer = GetHurtMaxTimer;
                        }
                    }
                    else
                    {
                        _Enemy2SoilderState = EnemySoilderStatus.Enemy_IldeState;
                    }

                }
            }
        }
        else
        {
            _Enemy2SoilderState = EnemySoilderStatus.Enemy_DeadState;
        }
    }




    // Function :DeadFunction
    // Method : This Function is Mainly used For Update The Statement
    // When The AI Is At The  Dead  Statement
    void DeadFunction()
    {
        if (IsDead)
        {
         _anim.Play("Death");
        _rgbd.constraints = RigidbodyConstraints.FreezeAll;
        _cap.isTrigger = true;
        _rgbd.useGravity = false;
        Destroy(this.gameObject, 5f);
            if(!HasBeenCountedBf)
            {
                HasBeenCountedBf = true;
                if(GameManager1._INSTANCE != null)
                {
                    GameManager1._INSTANCE.EnemyKillCounter++;
                }

            }





        }
      
    }






    public void GetHitForwardUpdate()
    {
        _ThrustVector = _Model.transform.forward * _anim.GetFloat("FrontHit") * 0.335f;
    }

    public void GetHitBackUpdate()
    {
        _ThrustVector = _Model.transform.forward * _anim.GetFloat("BackHit") * -0.335f;
       
    }

    public void Attack1ExitState()
    {
        _ArrowModel.SetActive(true);
        _anim.ResetTrigger("AttackTrigger1");
    }

    public void Attack2ExitState()
    {
        _ArrowModel.SetActive(true);
        _anim.ResetTrigger("AttackTrigger2");
    }

    public void Attack3ExitState()
    {
        _ArrowModel.SetActive(true);
        _anim.ResetTrigger("AttackTrigger3");
    }







}
