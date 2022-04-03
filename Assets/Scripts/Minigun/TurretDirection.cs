using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TurretDirection : MonoBehaviour
    {
        //apply script to gun barrel

        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private Transform turretX;
        [SerializeField] private Transform turretY;

        private void Start()
        {

        }
        void Update()
        {
            var direction = target.position - turretX.position;
            var stepX = Vector3.RotateTowards(turretX.forward,Vector3.ProjectOnPlane(direction,turretX.up), rotationSpeed * Time.fixedDeltaTime, 0f);
            turretX.rotation = Quaternion.LookRotation(stepX);
            var stepY = Vector3.RotateTowards(turretY.forward, direction, rotationSpeed * Time.fixedDeltaTime, 0f);
            turretY.rotation = Quaternion.LookRotation(stepY);

        }
    }
}
