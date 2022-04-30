using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

namespace Game
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] Stats MyStats;
        //[SerializeField] private float surfaceOffset = 1f;
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private Image targetHpBar;
        [SerializeField] Camera camera;

        Transform defaultTarget;
        CinemachineComposer composer;
        private void Start()
        {
            //cam = GetComponent<CinemachineVirtualCamera>();
            defaultTarget = vcam.LookAt;
            camera = Camera.main;
            composer = vcam.GetCinemachineComponent<CinemachineComposer>();
        }
        private void Update()
        {
            //if (!Input.GetMouseButtonDown(0))
            //{
            //    return;
            //}
            
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            transform.position = hit.point; //- (hit.normal * surfaceOffset);
            if (Input.GetKey(KeyCode.Mouse1))
            {
                vcam.LookAt = hit.transform;
                //remove after stats variant
                //if (hit.collider.gameObject.TryGetComponent<Stats>(out Stats targetStats)) targetStats.SetTargetHpBar(targetHpBar); 
                if (hit.collider.gameObject.TryGetComponent<CollisionChild>(out CollisionChild target) && target.Parent != MyStats) target.SetTargetHpBar(targetHpBar);
                composer.m_DeadZoneWidth = 0.1f;
                composer.m_DeadZoneHeight = 0.1f;

            }
            if (Input.GetKey(KeyCode.Mouse2))
            {
                vcam.LookAt = defaultTarget;
                composer.m_DeadZoneWidth = 0.5f;
                composer.m_DeadZoneHeight = 0.5f;
            }
            if (!vcam.LookAt.gameObject.active)
            {
                vcam.LookAt = defaultTarget;
                composer.m_DeadZoneWidth = 0.5f;
                composer.m_DeadZoneHeight = 0.5f;
            }
                //if (setTargetOn != null)
                //{
                //    setTargetOn.SendMessage("SetTarget", transform);
                //}
            }

    }
}