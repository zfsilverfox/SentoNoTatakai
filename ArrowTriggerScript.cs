using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTriggerScript : MonoBehaviour
{
    private static string FrontHitStringTags = "FrontHit";
    private static string BackHitStringTags = "BackHit";






    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == FrontHitStringTags)
        {
            Debug.Log("HasHitPlayer");
            Destroy(transform.parent.gameObject);
        }
        else if(other.gameObject.tag == BackHitStringTags)
        {
            Debug.Log("hasHitPlayer");
            Destroy(transform.parent.gameObject);
        }


    }





}
