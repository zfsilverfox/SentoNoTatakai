using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestColliderCheck : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
    if(other.gameObject.tag == "Player")
        {
            Debug.Log("Has Been EnterThe Found Player Statement");
            other.gameObject.GetComponent<PlayerStatementSystem>().CrtHealth += Random.Range(5,35);

            if(UIManager._INSTANCE != null)
            {

                if(!UIManager._INSTANCE.HasGetTheFirstTeasure 
                    &&
                    !UIManager._INSTANCE.HasGetTwoTeasure
                    && !UIManager._INSTANCE.HasGetThreeTeasure
                    && !UIManager._INSTANCE.HasGetTheFourTeasure)
                {
                    UIManager._INSTANCE.HasGetTheFirstTeasure = true;
                }
                else if (UIManager._INSTANCE.HasGetTheFirstTeasure
                    &&
                    !UIManager._INSTANCE.HasGetTwoTeasure
                    && !UIManager._INSTANCE.HasGetThreeTeasure
                    && !UIManager._INSTANCE.HasGetTheFourTeasure)
                {
                    UIManager._INSTANCE.HasGetTwoTeasure = true;
                }
                else if (UIManager._INSTANCE.HasGetTheFirstTeasure
                    &&
                    UIManager._INSTANCE.HasGetTwoTeasure
                    && !UIManager._INSTANCE.HasGetThreeTeasure
                    && !UIManager._INSTANCE.HasGetTheFourTeasure)
                {
                    UIManager._INSTANCE.HasGetThreeTeasure = true;
                }
                else if(UIManager._INSTANCE.HasGetTheFirstTeasure
                    &&
                    UIManager._INSTANCE.HasGetTwoTeasure
                    && UIManager._INSTANCE.HasGetThreeTeasure
                    && !UIManager._INSTANCE.HasGetTheFourTeasure)
                {
                    UIManager._INSTANCE.HasGetTheFourTeasure = true;
                }



            }

            if(GameManager1._INSTANCE != null)
            {
                GameManager1._INSTANCE.CountingTeasureNum++;
            }




            Destroy(this.gameObject);
        }    





    }




}
