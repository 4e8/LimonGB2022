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
        [SerializeField] private float surfaceOffset = 1f;
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private Image targetHpBar;

        Transform defaultTarget;

        private void Start()
        {
            //cam = GetComponent<CinemachineVirtualCamera>();
            defaultTarget = cam.LookAt;
        }
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
            transform.position = hit.point; //- (hit.normal * surfaceOffset);
            if (Input.GetKey(KeyCode.Mouse1))
            {
                cam.LookAt = hit.transform;
                //remove after stats variant
                if (hit.collider.gameObject.TryGetComponent<Stats>(out Stats targetStats)) targetStats.SetTargetHpBar(targetHpBar); 
                if (hit.collider.gameObject.TryGetComponent<CollisionChild>(out CollisionChild target) && target.Parent != MyStats) target.SetTargetHpBar(targetHpBar);
            }
            if (Input.GetKey(KeyCode.Mouse2))
            {
                cam.LookAt = defaultTarget;
            }
            if (!cam.LookAt.gameObject.active) cam.LookAt = defaultTarget;
            //if (setTargetOn != null)
            //{
            //    setTargetOn.SendMessage("SetTarget", transform);
            //}
        }

    }
}