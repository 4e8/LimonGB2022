using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rpmtrack : MonoBehaviour
{
    [SerializeField] WheelCollider wc;
    Text txt;

    private void Start()
    {
        Text txt = GetComponent<Text>();
    }
    void Update()
    {
        txt.text = wc.rpm.ToString();
    }
}
