using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;


public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager _INSTANCE
    {
        get { return _instance; }
    }


    public Image _CrtHealth;

    public Image _CrtMp;

    public Text CountingKillNumText;



    public Image[] _TeasureBox;
    public Sprite NotOpenSprite, OpenSprite;

    public GameObject _PauseObj, _CounterKillNumObj, _TeasurePanelObj, _Bars;

    public GameObject _PausePanelObj,_Lvl1GameOverPanel,_Lvl1GameClearPanel;

    public Text _Lvl1CountingTxtGameClear;

    public Image[] _Lvl1GameClearTeasureImage;


    public float BigNumAfterEarnTeasure = 135f;



   
 public    bool HasGetTheFirstTeasure, HasGetTwoTeasure,HasGetThreeTeasure,HasGetTheFourTeasure;
    bool HasPlayTheFirstTeasureAnimBf, HasPlayTheTwoTeasureAnimBf, HasPlayTheThreeTeasureAnimBf,HasPlayTheFourTeasureAnimBf;


    public Image _StarImage;
    public Sprite _HastFoundKey,_HasFoundKey;

    public bool HasFoundKey = false;
    bool HasDoFoundKeyAnimationBf = false;


    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    void Start()
    {
        foreach(Image imgs in _TeasureBox)
        {
            if (imgs != null)
                imgs.sprite = NotOpenSprite;
            else Debug.Log("All Of the Image  Are Null");
        }

    }


    void SettingBasicInformation()
    {
        BooleanBasicSetting();

    }

    private void BooleanBasicSetting()
    {
        HasGetTheFirstTeasure = false;
        HasGetTwoTeasure = false;
        HasGetThreeTeasure = false;
        HasGetTheFourTeasure = false;
        HasPlayTheFirstTeasureAnimBf = false;
        HasPlayTheTwoTeasureAnimBf = false;
        HasPlayTheThreeTeasureAnimBf = false;
        HasPlayTheFourTeasureAnimBf = false;
    }

    void Update()
    {
        if (PlayerStatementSystem._INSTANCE != null)
        {
            UpdatePlayerStatement();
        }



        UpdateGetTheTeasureFunction();

        UpdateKillingNumberTxtFunction();

        Lvl1GameClearUISetting();


        DisableObjWhenGameOverFunction();
    }


    //Function : UpdateGetTEasureInformation
    // Method :This Function is mainly used For Get The First Teasure Bf 
    void UpdateGetTheTeasureFunction()
    {
        if (HasGetTheFirstTeasure)
        {
            if (!HasPlayTheFirstTeasureAnimBf)
            {
                StartCoroutine(PlayTheTeasureAnimBf(0.25f,0));
                HasPlayTheFirstTeasureAnimBf = true;
            }
        }

        if (HasGetTwoTeasure)
        {
            if (!HasPlayTheTwoTeasureAnimBf)
            {
                StartCoroutine(PlayTheTeasureAnimBf(0.25f, 1));
                HasPlayTheTwoTeasureAnimBf = true;
            }
        }

        if (HasGetThreeTeasure)
        {
            if (!HasPlayTheThreeTeasureAnimBf)
            {
                StartCoroutine(PlayTheTeasureAnimBf(0.25f, 2));
                HasPlayTheThreeTeasureAnimBf = true;
            }
        }

        if (HasGetTheFourTeasure)
        {
            if (!HasPlayTheFourTeasureAnimBf)
            {
                StartCoroutine(PlayTheTeasureAnimBf(0.25f, 3));
                HasPlayTheFourTeasureAnimBf = true;
            }
        }

    }


    // Function:  UpdateKillingNumberTxtFunction
    // Method : This Function is Mainly used For Update KillingNumberFunction
    void UpdateKillingNumberTxtFunction()
    {
        if(GameManager1._INSTANCE != null)
        {
            CountingKillNumText.text = GameManager1._INSTANCE.EnemyKillCounter.ToString();
            HasFoundKey = GameManager1._INSTANCE.HasKey;
        }

        SpriteOpenOrNotGameClearFunction(HasGetTheFirstTeasure, _Lvl1GameClearTeasureImage, 0);
        SpriteOpenOrNotGameClearFunction(HasGetTwoTeasure, _Lvl1GameClearTeasureImage, 1);
        SpriteOpenOrNotGameClearFunction(HasGetThreeTeasure, _Lvl1GameClearTeasureImage, 2);
        SpriteOpenOrNotGameClearFunction(HasGetTheFourTeasure, _Lvl1GameClearTeasureImage, 3);

        if (HasFoundKey)
        {
            if (!HasDoFoundKeyAnimationBf)
            {
                StartCoroutine(PlayTheKeyAnimBf());
                HasDoFoundKeyAnimationBf = true;
            }
        }



    }


    // This Function is mainly used For Checking Open The Sprite Or Not
    void SpriteOpenOrNotGameClearFunction(bool GameClearGetTeasure 
        ,Image[] _GameClearTeasureImage,int num)
    {
        if (GameClearGetTeasure)
        {
            _GameClearTeasureImage[num].sprite = OpenSprite;
        }
        else
        {
            _GameClearTeasureImage[num].sprite = NotOpenSprite;
        }
    }




    // Function : UpdatePlayerStatement
    // Method : This Function is Mainly used For Update Player Crt Statement
    void UpdatePlayerStatement()
    {
        UpdateCrtHealthStatement((float)PlayerStatementSystem._INSTANCE.CrtHealth,(float) PlayerStatementSystem._INSTANCE.MaxHealth);
        UpdateCrtMpStatement(PlayerStatementSystem._INSTANCE.StrongAttackCrtMpSystem, PlayerStatementSystem._INSTANCE.StrongAttackMpMaxStatement);
    }

    //Function: UpdateCrtHealthStatement
    // Method : Update PlayerCurrent Health Statement 
    void UpdateCrtHealthStatement(float CrtHealth,float MaxHealth)
    {
        _CrtHealth.fillAmount = CrtHealth / MaxHealth;
    }

    // Function : UpdateCrtMpStatement
    // Method : Update Player Current Mp Statement 
    void UpdateCrtMpStatement(float CrtMP,float MaxMp)
    {
        _CrtMp.fillAmount = CrtMP / MaxMp;
    }

    // Function:DisableObjWhenGameOverFunction
        // Method : This Function is Mainly Dissable Obj When
    void DisableObjWhenGameOverFunction()
    {
        if(GameManager1._INSTANCE != null)
        {
            if (GameManager1._INSTANCE.GameOver)
            {
                SettingObjActive(false);
            }

        }
    }


    void Lvl1GameClearUISetting()
    {
        if(GameManager1._INSTANCE != null)
        {
            _Lvl1CountingTxtGameClear.text = GameManager1._INSTANCE.EnemyKillCounter.ToString();
        }
           




    }



  

    //Function : SettingObjActive
    //Method : This Function is Mainly used For set The Object Active as True Or False
    public void SettingObjActive(bool active)
    {
                _PauseObj.SetActive(active);
                _TeasurePanelObj.SetActive(active);
                _CounterKillNumObj.SetActive(active);
                _Bars.SetActive(active);
    }



    //Function : TeasureAction 
    // Method : This Function is Mainly to use 
    void TeasureAction(Image teasureImg)
    {
        teasureImg.rectTransform.sizeDelta = new Vector2(BigNumAfterEarnTeasure, BigNumAfterEarnTeasure);
        teasureImg.sprite = OpenSprite;
        RectTransform t = teasureImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f),0.95f,false);
    }

    void KeyAction(Image teasureImg)
    {
        teasureImg.rectTransform.sizeDelta = new Vector2(BigNumAfterEarnTeasure, BigNumAfterEarnTeasure);
        teasureImg.sprite = _HasFoundKey;
        RectTransform t = teasureImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.95f, false);
    }





    // IEumerator : PlayTheAnimCoroutine
    // Method : This Function is Mainly to Play The Animation 
    IEnumerator PlayTheTeasureAnimBf(float deltaTime = 0.4f,int TeasureNum =0)
    {
        yield return new WaitForSeconds(0.4f);
       
            TeasureAction(_TeasureBox[TeasureNum]);
    }

    IEnumerator PlayTheKeyAnimBf()
    {
        yield return new WaitForSeconds(0.4f);

        KeyAction(_StarImage);
    }


}
