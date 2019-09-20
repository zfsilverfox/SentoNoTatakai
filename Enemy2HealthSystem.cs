using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2HealthSystem : MonoBehaviour
{
    EnemyChracter2Ctrl _EnemyChracter2Ctrl;
    public int MaxHealth = 100;
    
    public int CrtHealth;


    private void Awake()
    {
        _EnemyChracter2Ctrl = GetComponent<EnemyChracter2Ctrl>();
    }

    private void Start()
    {
        MaxHealth = Random.Range(20, 25);
        CrtHealth = MaxHealth;
    }


    private void Update()
    {
        if(CrtHealth <= 0)
        {
            _EnemyChracter2Ctrl.IsDead = true;
          
        }
        else if(CrtHealth > 0)
        {
            _EnemyChracter2Ctrl.IsDead = false;
        }
    }







}
