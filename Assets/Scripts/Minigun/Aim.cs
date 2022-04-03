using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public float surfaceOffset = 0.0f;

    private void Update()
    {
        //if (!Input.GetMouseButtonDown(0))
        //{
        //    return;
        //}
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        transform.position = hit.point;//+ hit.normal * surfaceOffset;
        //if (setTargetOn != null)
        //{
        //    setTargetOn.SendMessage("SetTarget", transform);
        //}
    }

}
