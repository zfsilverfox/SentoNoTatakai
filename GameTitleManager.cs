using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class GameTitleManager : MonoBehaviour
{

    public GameObject _CtrlPanel;

    public Text _KillNum;

    public Image _FirstTeasureImage,_SecondTeasureImage,_ThreeTeasureImage,_FourTeasureImage;
    public Sprite _OpenSprite,_CloseSprite;



    public GameObject _LoadingPanel;
    public Image _LoadingProcess;

    public bool PressStartGame = false;
    bool HasEnterTheGameBf = false;

    private void Awake()
    {
        SettingResolutionProblem();
    }

    void SettingResolutionProblem()
    {
        Screen.SetResolution(1024, 768, false);

    }

    private void Start()
    {
        PlayerPrefSetting();
        PressStartGame = false;
        HasEnterTheGameBf = false;
    }

    void PlayerPrefSetting()
    {
        _KillNum.text = "BestKillNum:" + PlayerPrefs.GetInt("KillEnemyScore").ToString();

        if (PlayerPrefs.GetInt("TeasureNum") == 0)
        {
            _FirstTeasureImage.sprite = _CloseSprite;
            _SecondTeasureImage.sprite = _CloseSprite;
            _ThreeTeasureImage.sprite = _CloseSprite;
            _FourTeasureImage.sprite = _CloseSprite;
        }
        else if (PlayerPrefs.GetInt("TeasureNum") == 1)
        {
            _FirstTeasureImage.sprite = _OpenSprite;
            _SecondTeasureImage.sprite = _CloseSprite;
            _ThreeTeasureImage.sprite = _CloseSprite;
            _FourTeasureImage.sprite = _CloseSprite;
        }
        else if (PlayerPrefs.GetInt("TeasureNum") == 2)
        {
            _FirstTeasureImage.sprite = _OpenSprite;
            _SecondTeasureImage.sprite = _OpenSprite;
            _ThreeTeasureImage.sprite = _CloseSprite;
            _FourTeasureImage.sprite = _CloseSprite;
        }
        else if (PlayerPrefs.GetInt("TeasureNum") == 3)
        {
            _FirstTeasureImage.sprite = _OpenSprite;
            _SecondTeasureImage.sprite = _OpenSprite;
            _ThreeTeasureImage.sprite = _OpenSprite;
            _FourTeasureImage.sprite = _CloseSprite;
        }
        else if (PlayerPrefs.GetInt("TeasureNum") == 4)
        {
            _FirstTeasureImage.sprite = _OpenSprite;
            _SecondTeasureImage.sprite = _OpenSprite;
            _ThreeTeasureImage.sprite = _OpenSprite;
            _FourTeasureImage.sprite = _OpenSprite;
        }
    }


    private void Update()
    {
        if (PressStartGame)
        {
            _LoadingPanel.SetActive(true);
            if (!HasEnterTheGameBf)
            {
                StartCoroutine(HasStartGameBF());
                HasEnterTheGameBf = true;
            }

        }

    }

    public void CtrlPanelSetActiveAsOn()
    {
        _CtrlPanel.SetActive(true);
    }

    
    public void CtrlPanelSetActiveAsOff()
    {
        _CtrlPanel.SetActive(false);
    }

    public void StartTheGame()
    {
        PressStartGame = true;
    }

    IEnumerator HasStartGameBF()
    {
        yield return new WaitForSeconds(5f);

        AsyncOperation scene = SceneManager.LoadSceneAsync("GameMain1");


        while(!scene.isDone)
        {

            _LoadingProcess.fillAmount = scene.progress;

            if(scene.progress >= 0.9f)
            {
               _LoadingProcess.fillAmount = 1.0f;
            }




          yield   return null;
        }



    }


}
