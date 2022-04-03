using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] bool Forward;



    void OnCollisionStay()
    {
        float thortle = Input.GetAxis("Vertical");

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,thortle);
    }
}
