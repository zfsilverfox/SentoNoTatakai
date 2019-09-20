using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
    public bool HasFoundKey = false;


    public GameObject[] FirstDisActiveObj;


    private void Start()
    {
        HasFoundKey = false;
    }

    private void Update()
    {
        SettingHasFoundKeyOrNotObj();
    }


    void SettingHasFoundKeyOrNotObj()
    {
        if(GameManager1._INSTANCE != null)
        {
            
                HasFoundKey = GameManager1._INSTANCE.HasKey;
          
        }




        if (HasFoundKey)
        {
            SettingTheFirstObjHasActiveOrNot(false);
        }
        else
        {
            SettingTheFirstObjHasActiveOrNot(true);
        }

    }

    void SettingTheFirstObjHasActiveOrNot(bool active)
    {
        foreach (GameObject Ds in FirstDisActiveObj)
        {
            Ds.SetActive(active);
        }
    }


}
