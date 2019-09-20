using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour
{
  



    public void PressPauseBtn()
    {
      

        if (UIManager._INSTANCE != null)
        {
            UIManager._INSTANCE._PausePanelObj.SetActive(true);
            UIManager._INSTANCE.SettingObjActive(false);

            if(!UIManager._INSTANCE._PauseObj.activeInHierarchy && UIManager._INSTANCE._PausePanelObj.activeInHierarchy)
            {
                Time.timeScale = 0;
            }


        }







    }

    public void PressContinueBtn()
    {
        if (UIManager._INSTANCE != null)
        {
            UIManager._INSTANCE._PausePanelObj.SetActive(false);
            UIManager._INSTANCE.SettingObjActive(true);

            if (UIManager._INSTANCE._PauseObj.activeInHierarchy && !UIManager._INSTANCE._PausePanelObj.activeInHierarchy)
            {
                Time.timeScale = 1;
            }

        }
    }

    public void PressRestartBtn()
    {

        Time.timeScale = 1;
        if(GameManager1._INSTANCE != null)
        {
            SceneManager.LoadScene("GameMain1");
        }
    }

    public void PressQuitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameTitle");
    }


    public void RestartGame()
    {
        if (GameManager1._INSTANCE != null)
        {
            SceneManager.LoadScene("GameMain1");
        }
    }

    public void QuitGame()
    {
      
        SceneManager.LoadScene("GameTitle");
    }




}
