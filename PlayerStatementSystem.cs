using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatementSystem : MonoBehaviour
{
    PlayerCtrl _playerCtrl;


    private static PlayerStatementSystem _instance;

    public static PlayerStatementSystem _INSTANCE
    {
        get { return _instance; }
    }



    public int MaxHealth = 1500;
   
   public int CrtHealth;

    public float StrongAttackMpMaxStatement = 100;
 
   public float StrongAttackCrtMpSystem;


    private void Awake()
    {
        if (_instance == null) _instance = this;
        if (_playerCtrl == null) _playerCtrl = GetComponent<PlayerCtrl>();
    }


    private void Start()
    {
        CrtHealth = MaxHealth;
        StrongAttackCrtMpSystem = StrongAttackMpMaxStatement;

    }


    private void Update()
    {
        

        if(StrongAttackCrtMpSystem <= StrongAttackMpMaxStatement)
        {

            StrongAttackCrtMpSystem += Time.deltaTime;

        }


        if(StrongAttackCrtMpSystem > StrongAttackMpMaxStatement)
        {
            StrongAttackCrtMpSystem = StrongAttackMpMaxStatement;
        }


        if(CrtHealth > 0)
        {
            _playerCtrl.IsDead = false;
        }
        else if (CrtHealth <= 0)
        {
            _playerCtrl.IsDead = true;
        }

        if (CrtHealth > MaxHealth)
                CrtHealth = MaxHealth;

    }

}
