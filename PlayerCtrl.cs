using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static string SpdStringAnimator = "Spd";
    private static string GroundStringAnimator = "Ground";
    private static string JumpTriggerAnimator = "JumpTrigger";
    private static string DashTriggerAnimator = "DashTrigger";
    private static string DashCurveAnimatorString = "DashCurve";
    private static string AttackAnimatorIntegerString = "AttackCounter";
    private static string StrongAttackAnimationTrigger= "StrongAttackTrigger";
    private static string FrontHitTriggerAnimationClip = "FrontHitTrigger";
    private static string BackHitTriggerAnimationClip = "BackHitTrigger";






    private static string TheAniamtorFirstLayer = "Base Layer";
    private static string GroundName = "Ground";
    private static string Attack1AnimationClip = "Attack1";
    private static string Attack2AnimationClip = "Attack2";
    private static string Attack3AnimationClip = "Attack3";


    private static PlayerCtrl _instance;
    
    public static PlayerCtrl _INSTANCE
    {
        get
        {
            return _instance;
        }
    }







    PlayerInput _pi;
    PlayerAudioManager _PlayerAudioManager;
    PlayerStatementSystem _PlayerStatementSys;
 [HideInInspector] public  Animator _anim;
    AnimatorStateInfo _animStateInfo;
    CapsuleCollider _cap;




    Rigidbody _rgbd;




   
    private GameObject _RRefence;
    public GameObject _RightShoulder;

    public GameObject _WeaponPosition;

    public GameObject _PlayerModel;

    public GameObject[] HitTrigger;
    public GameObject[] StrongHitTrigger;


    public float MovSpd= 0.0f;
    public float WalkingSpd = 1.0f;
    public float RunningSpd = 2.0f;
    public float JumpForce = 5.5f;
    //public float offset = 0.5f;
    public Vector3 JumpOffset;


    public LayerMask _GroundLayer;

    public float RayCastJumpDir = 1.2f;
    public float DashVec = 0.25f;
    public float StrongAttackThrust = 1.5f;



    bool FlipBoolean = false;
    bool PlayOnceSoundIfdead = false;




    public bool CanJump = true;
    public bool isDash = false;
    public bool isGround = true;
    public bool isJumpLanding = true;
    public bool isAttack = false;
    public bool IsGetHurt = false;
    public bool IsDead = false;
    public bool CanDoStrongAttackMP = true;
    public bool CANDOStrongAttackTrigger = true;
    public bool CanAttack = true;
    public bool BeingHurtWithStrongAttack = false;
    public bool HasGameClear = false;
    bool HasPlayTheGameClearSoundBf = false;


    public int AttackBtnCount = 0;



    private Vector3 ThrustVector;


    public float DownOffset = 4.0f;

    private void Awake()
    {
        AvoidNullProblem();
    }

    //Function : AvoidNullProblem
    // Method : This Function is Mainly used For GetThe Component which is NULL
    void AvoidNullProblem()
    {
        if (_instance == null) _instance = this;

        if (_pi == null) _pi = GetComponent<PlayerInput>();
        if (_PlayerAudioManager == null) _PlayerAudioManager = GetComponent<PlayerAudioManager>();
        if (_anim == null) _anim = GetComponentInChildren<Animator>();
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
        if (_PlayerStatementSys == null) _PlayerStatementSys = GetComponent<PlayerStatementSystem>();
        if (_cap == null) _cap = GetComponent<CapsuleCollider>();
        if (HitTrigger == null) Debug.Log("Please Add The Object To The Inspector");

    }


     void Start()
    {
        SettingBasicInformation();
    }

    // Function : SettingBasicInformation
    //Method : This Function is Mainly Used For Setting The Basic Information
    private void SettingBasicInformation()
    {
        _RRefence = new GameObject("RRRefence");
        FlipBoolean = false;
        CanJump = true;
        isGround = true;
        isAttack = false;
        IsGetHurt = false;
        IsDead = false;
        isDash = false;
        isJumpLanding = true;
        CanDoStrongAttackMP = true;
        CanAttack = true;
        CANDOStrongAttackTrigger = true;
        AttackBtnCount = 0;
        PlayOnceSoundIfdead = false;
        HasGameClear = false;
        HasPlayTheGameClearSoundBf = false;
        AudioSource.PlayClipAtPoint(_PlayerAudioManager._StartSound, transform.position, 1f);
    }

    private void Update()
    {
        if (!HasGameClear)
        {
            if (!IsDead)
        {
            _anim.SetFloat(SpdStringAnimator, Mathf.Abs(_pi.HorizontalSpd));
        isGround = CanJumpBoolean();
        if (_pi.JumpKeyPressed && isGround && !isAttack && !IsGetHurt)
        {
            if (CanJump)
            {
                    if (CanDoStrongAttackMP)
                    {
                    _rgbd.AddForce(new Vector3(0f, JumpForce , 0f));
                    _anim.SetTrigger(JumpTriggerAnimator);
                    _anim.ResetTrigger(DashTriggerAnimator);
                    }
            }
        }
        if (_pi.DashKeyPressed&& isGround && !isAttack && !IsGetHurt)
        {
                if (CanJump)
                {
                    if (CanDoStrongAttackMP)
                    {
                        _anim.SetTrigger(DashTriggerAnimator);
                        _anim.ResetTrigger(JumpTriggerAnimator);
                    }
                }
        }


            if (_pi.WeakAttack)
            {


                if (CanAttack)
                {
                    if (AttackBtnCount == 0 && _animStateInfo.IsName(GroundName))
                    {
                        AttackBtnCount = 1;

                        int RandSound = Random.Range(0, _PlayerAudioManager._AttackSound.Length);
                        AudioSource.PlayClipAtPoint(_PlayerAudioManager._AttackSound[RandSound], transform.position);

                    }
                    else if (AttackBtnCount == 1 && _animStateInfo.IsName(Attack1AnimationClip) && _animStateInfo.normalizedTime >= 0.75f)
                    {
                        AttackBtnCount = 2;
                        int RandSound = Random.Range(0, _PlayerAudioManager._AttackSound.Length);
                        AudioSource.PlayClipAtPoint(_PlayerAudioManager._AttackSound[RandSound], transform.position);
                    }
                    else if (AttackBtnCount == 2 && _animStateInfo.IsName(Attack2AnimationClip) && _animStateInfo.normalizedTime >= 0.75f)
                    {
                        AttackBtnCount = 3;
                        int RandSound = Random.Range(0, _PlayerAudioManager._AttackSound.Length);
                        AudioSource.PlayClipAtPoint(_PlayerAudioManager._AttackSound[RandSound], transform.position);
                    }
                }
            }

            if (_pi.StrongAttack)
            {
                if (CANDOStrongAttackTrigger)
                {
                    if(CanDoStrongAttackMP)
                    {
                        if (CanJump)
                        {
                    
                            if (_PlayerStatementSys.StrongAttackCrtMpSystem >= 50)
                            {
                                _anim.SetTrigger(StrongAttackAnimationTrigger);
                                _PlayerStatementSys.StrongAttackCrtMpSystem -= 50;
                                CanDoStrongAttackMP = false;
                            }
                        }
                    }
                }
            }

            if (!_animStateInfo.IsName(GroundName) && _animStateInfo.normalizedTime >= 0.95f)
            {
                AttackBtnCount = 0;
            }
        }
            else
        {
            //This is Mainly to used The Function which is Dead Function
            _anim.Play("Death");
            _rgbd.useGravity = false;
            _cap.isTrigger = true;
            _rgbd.constraints = RigidbodyConstraints.FreezeAll;


            foreach(GameObject st in HitTrigger)
            {
                st.SetActive(false);
            }

            foreach(GameObject st in StrongHitTrigger)
            {
                st.SetActive(false);
            }


            if(!PlayOnceSoundIfdead)
            {
                PlayOnceSoundIfdead = true;
                AudioSource.PlayClipAtPoint(_PlayerAudioManager.DeadSound, transform.position);
            }
        }

        _animStateInfo = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex(TheAniamtorFirstLayer));

        _anim.SetInteger(AttackAnimatorIntegerString, AttackBtnCount);
        _anim.SetBool(GroundStringAnimator, isGround);

        }
        else
        {
            if (!HasPlayTheGameClearSoundBf)
            {
                AudioSource.PlayClipAtPoint(_PlayerAudioManager._VictorySound, transform.position);
                HasPlayTheGameClearSoundBf = true;
            }



            _anim.SetFloat(SpdStringAnimator, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (!HasGameClear)
        {
 if (!IsDead)
        {
   if (Mathf.Abs(_pi.HorizontalSpd) >= 0.5f && Mathf.Abs(_pi.HorizontalSpd) <= 0.8f)
        {
            MovSpd = WalkingSpd;
        }
        else if(Mathf.Abs(_pi.HorizontalSpd) >= 0.81f)
        {
            MovSpd = RunningSpd;
        }

        HandlePlayerDashMovmentProblem();





        if (_pi.HorizontalSpd >= 0.5f && FlipBoolean)
        {
                    FlipFunction();
                    JumpOffset = new Vector3(-0.33f, 0f, 0f);
        }
        else if(_pi.HorizontalSpd <= -0.5f && !FlipBoolean)
        {
                    FlipFunction();

                    JumpOffset = new Vector3(0.33f, 0f, 0f);
        }
        if (!IsGetHurt )
        {
                if (!isAttack)
                {
                    if (!BeingHurtWithStrongAttack)
                    {
                        if (isJumpLanding)
                        {
                            _rgbd.velocity = new Vector3( _pi.HorizontalSpd* MovSpd * Time.fixedDeltaTime,_rgbd.velocity.y, 0f);
                        }
                    }
                    
                }
      
        }
        ShoulderHandle();
        }
        }


       
    }


    // Function :HandlePlayerDashMovmentProblem
    //This Function mainly used For Update The PlayerDashMovment
    void HandlePlayerDashMovmentProblem()
    {
        _rgbd.velocity = new Vector3(_rgbd.velocity.x, _rgbd.velocity.y, _rgbd.velocity.z) + ThrustVector;
        ThrustVector = Vector3.zero;

    }



    void ShoulderHandle()
    {
        Vector3 RightShoulderRefenrence = _RightShoulder.transform.TransformPoint(Vector3.zero);

        _RRefence.transform.position = RightShoulderRefenrence;

        _RRefence.transform.parent = transform;

        _WeaponPosition.transform.position = _RRefence.transform.position;

    }


    void FlipFunction()
    {
        FlipBoolean = !FlipBoolean;
        Vector3 YEularAngle = transform.eulerAngles;
        YEularAngle.y *= -1;
        transform.eulerAngles = YEularAngle;
    }


    //Boolean: CanJumpBoolean
    // This Function is Mainly Used For Checking Player Can  Jump Or Not
    bool CanJumpBoolean()
    {
        Vector3 originPos= transform.position + JumpOffset;
        Vector3 DownPosition = Vector3.down * DownOffset;
        RaycastHit hit;
            Debug.DrawRay(originPos, DownPosition);
        if(Physics.Raycast(originPos,DownPosition,out hit, RayCastJumpDir, _GroundLayer))
        {
            return true;
        }

        return false;
    }





    //Function : JumpStartAnimStartState
    // This Function is Mainly Used For When Player Enter The Jump Start Statement
    public void JumpStartAnimStartState()
    {
        CanJump = false;
        AudioSource.PlayClipAtPoint(_PlayerAudioManager.JumpSound, transform.position, 0.75f);

    }



    //Function : JumpStartAnimUpdateState
    // This Function is Mainly Used For When Player Update The Jump Start Statement
    public void JumpStartAnimUpdateState()
    {
        CanJump = false;
        _anim.ResetTrigger(JumpTriggerAnimator);
    }



    public void JumpLandStartState()
    {
        isJumpLanding = false;
    }


    public void JumpLandAnimExitState()
    {
        CanJump = true;
        isJumpLanding = true;
    }
    public void IldeWalkRunningStateUpdateState()
    {
        CanJump = true;
        isAttack = false;
        IsGetHurt = false;
        CanAttack = true;
        isDash = false;
        CANDOStrongAttackTrigger = true;
        BeingHurtWithStrongAttack = false;
    }

    public void DashAnimEnterState()
    {
        CanJump = false;
        isDash = true;
        AudioSource.PlayClipAtPoint(_PlayerAudioManager.JumpSound, transform.position, 0.75f);
    }

    public void DashAnimUpdateState()
    {
        CanJump = false;
        _anim.ResetTrigger(DashTriggerAnimator);
        ThrustVector = _PlayerModel.transform.forward * DashVec * _anim.GetFloat(DashCurveAnimatorString);
    }


    public void Attack1pdateState()
    {
        isAttack = true;
        CanJump = false;
    }

    public void Attack1ExitState()
    {
        foreach(GameObject st in HitTrigger)
        {
            st.SetActive(false);
        }
    }

    public void Attack2UpdateState()
    {
        isAttack = true;
        CanJump = false;
    }

    public void Attack2ExitState()
    {
        foreach (GameObject st in HitTrigger)
        {
            st.SetActive(false);
        }
    }

    public void Attack3UpdateState()
    {
        isAttack = true;
        CanJump = false;
    }

    public void Attack3ExitState()
    {
        foreach (GameObject st in HitTrigger)
        {
            st.SetActive(false);
        }


    }

    public void StrongAttackStartState()
    {
        AudioSource.PlayClipAtPoint(_PlayerAudioManager._StrongAttackSound, transform.position);
        isAttack = true;
        CanJump = false;
    }
    
    public void StrongAttackExitState()
    {
        foreach(GameObject ST in StrongHitTrigger)
        {
            ST.SetActive(false);
        }
        CanDoStrongAttackMP =true;
    }




    public void HitBackUpdateState()
    {
        IsGetHurt = true;

        ThrustVector = _PlayerModel.transform.forward * _anim.GetFloat("HitBackValue") * 0.0625f;
    }

    public void HitFrontUpdateState()
    {
        IsGetHurt = true;

        ThrustVector = _PlayerModel.transform.forward * _anim.GetFloat("HitFrontValue") *-0.0625f;
    }
     

    public void HitFrontStartState()
    {
        AttackBtnCount = 0;
    }

    public void HitBackStartState()
    {
        AttackBtnCount = 0;
    }


    public void StorngHitFallDownEnterStartState()
    {
        CanJump = false;
        CanAttack = false;
        CANDOStrongAttackTrigger = false;
        BeingHurtWithStrongAttack = true;
    }

    public void StorngHitFallDownEnterUpdateState()
    {
        ThrustVector = _PlayerModel.transform.forward * -StrongAttackThrust;
        BeingHurtWithStrongAttack = true;
    }


    public void DeadEnterStatement()
    {
        _anim.ResetTrigger(Attack1AnimationClip);
        _anim.ResetTrigger(Attack2AnimationClip);
        _anim.ResetTrigger(Attack3AnimationClip);
        _anim.ResetTrigger(FrontHitTriggerAnimationClip);
        _anim.ResetTrigger(BackHitTriggerAnimationClip);
    }






}
