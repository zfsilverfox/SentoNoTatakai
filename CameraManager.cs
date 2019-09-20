using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform _TargetFollow;

   



    public Vector3 Dist;

    Vector3 RefVector3Velocity;


    private void Start()
    {

        Dist = transform.position - _TargetFollow.position;
       

    }

    private void Update()
    {

      
            Vector3 MovingCameraDistance = _TargetFollow.position + Dist;
        transform.position = Vector3.Lerp(transform.position, MovingCameraDistance, 1.25f * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, MovingCameraDistance, ref RefVector3Velocity, 0.5f * Time.deltaTime);
     
     
      


    }




}
