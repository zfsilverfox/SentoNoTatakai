using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody _rgbd;
    MeshCollider _MeshCol;

    public float Spd = 1000f;




    private void Awake()
    {
        GetComponentFunction();
    }

    void GetComponentFunction()
    {
        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();
        if (_MeshCol == null) _MeshCol = GetComponent<MeshCollider>();
    }

    private void Start()
    {

        Destroy(this.gameObject, 10f);
        _rgbd.AddForce(transform.forward * Spd);
    }




    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject)
        {
            Destroy(this.gameObject);
        }


    }








}
