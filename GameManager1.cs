using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    private static GameManager1 _instance;

    public static GameManager1 _INSTANCE
    {
        get { return _instance; }

    }

    
 


    public bool GameOver = false;
    public bool GameClear = false;


    public int CountingTeasureNum = 0;
    public int EnemyKillCounter = 0;

    public bool HasKey = false;
    bool HasPlayTheDoorSoundEffectBf = false;
    public AudioClip _OpenDoorSoundEffect;
    public Transform _DoorPosition;


     void Awake()
    {
        if (_instance == null) _instance = this;

        SettingResolutionProblem();
    }


    void SettingResolutionProblem()
    {
        Screen.SetResolution(1024, 768, false);
    }


    private void Start()
    {
        SettingBasicInformation();
    }

    void SettingBasicInformation()
    {
        GameOver = false;
        GameClear = false;
        CountingTeasureNum = 0;
        EnemyKillCounter = 0;
        HasPlayTheDoorSoundEffectBf = false;
        CountingTeasureNum = 0;
    }



    private void Update()
    {
        CheckingTeasureProblem();
        CheckingGameClearOrGameOverFunction();
        GameClearOrGameOverFunction();
    }

 void CheckingTeasureProblem()
    {
        if(UIManager._INSTANCE != null)
        {
            if (UIManager._INSTANCE.HasGetTheFirstTeasure 
                && !UIManager._INSTANCE.HasGetTwoTeasure
                && !UIManager._INSTANCE.HasGetThreeTeasure
                && !UIManager._INSTANCE.HasGetTheFourTeasure
                )
            {
                CountingTeasureNum = 1;
            }
            else if (UIManager._INSTANCE.HasGetTheFirstTeasure
               && UIManager._INSTANCE.HasGetTwoTeasure
               && !UIManager._INSTANCE.HasGetThreeTeasure
               && !UIManager._INSTANCE.HasGetTheFourTeasure
               )
            {
                CountingTeasureNum = 2;
            }
            else if (UIManager._INSTANCE.HasGetTheFirstTeasure
              && UIManager._INSTANCE.HasGetTwoTeasure
              &&UIManager._INSTANCE.HasGetThreeTeasure
              && !UIManager._INSTANCE.HasGetTheFourTeasure
              )
            {
                CountingTeasureNum = 3;
            }
            else if (UIManager._INSTANCE.HasGetTheFirstTeasure
             && UIManager._INSTANCE.HasGetTwoTeasure
             && UIManager._INSTANCE.HasGetThreeTeasure
             && UIManager._INSTANCE.HasGetTheFourTeasure
             )
            {
                CountingTeasureNum = 4;
            }

        }

    }

    void CheckingGameClearOrGameOverFunction()
    {
         if(PlayerCtrl._INSTANCE != null)
                {
                    if (PlayerCtrl._INSTANCE.IsDead)
                    {
                        GameOver = true;
                    }
                }


         if(BossCtrl._INSTANCE != null)
        {
            if (BossCtrl._INSTANCE.isDead)
            {
                GameClear = true;
            }

        }



    }

    //This Function is Mainly is used For The 

    void GameClearOrGameOverFunction()
    {
        if(GameOver && !GameClear)
        {

            if(UIManager._INSTANCE != null)
            {
                UIManager._INSTANCE.SettingObjActive(false);
                if(UIManager._INSTANCE._Lvl1GameOverPanel != null)
                    UIManager._INSTANCE._Lvl1GameOverPanel.SetActive(true);
            }



        }
        else if(!GameOver && GameClear)
        {
            if (UIManager._INSTANCE != null)
            {
                UIManager._INSTANCE.SettingObjActive(false);

                if (UIManager._INSTANCE._Lvl1GameClearPanel != null)
                        UIManager._INSTANCE._Lvl1GameClearPanel.SetActive(true);
            }
            if(PlayerCtrl._INSTANCE != null)
            {
                PlayerCtrl._INSTANCE.HasGameClear = true;
            }


            if (EnemyKillCounter > PlayerPrefs.GetInt("KillEnemyScore"))
            {
                PlayerPrefs.SetInt("KillEnemyScore", EnemyKillCounter);
            }

            if(CountingTeasureNum > PlayerPrefs.GetInt("TeasureNum"))
            {
                PlayerPrefs.SetInt("TeasureNum", CountingTeasureNum);

            }
        }
        else if(GameOver && GameClear)
        {
            Debug.LogError("That's Something You need To Check");
        }


    }


}
