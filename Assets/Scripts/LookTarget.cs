using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(FindTarget))]

    public class LookTarget : MonoBehaviour
    {

        //[SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 2f;
        private FindTarget findTarget;
        private void Start()
        {
            findTarget = GetComponent<FindTarget>();
        }
        void Update()
        {
            if (!findTarget.HasTarget) return;
            var direction = findTarget.Target.position - transform.position;
            var step = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.fixedDeltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(step);
        }
    }
}