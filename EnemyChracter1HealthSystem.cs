using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChracter1HealthSystem : MonoBehaviour
{


    public int MaxHealth = 25;
 [HideInInspector]   public int CrtHealth ;

    EnemyChracter1Ctrl _EnemyChracter1Ctrl;

    private void Awake()
    {
        if (_EnemyChracter1Ctrl == null) _EnemyChracter1Ctrl = GetComponent<EnemyChracter1Ctrl>();

    }


    private void Start()
    {
        MaxHealth = Random.Range(15, 30);
        CrtHealth = MaxHealth;
    }

    private void Update()
    {
        
        if(CrtHealth <= 0)
        {
            _EnemyChracter1Ctrl.IsDead = true;
        }

        else if(CrtHealth > 0)
        {
            _EnemyChracter1Ctrl.IsDead =false;
        }

    }


}
