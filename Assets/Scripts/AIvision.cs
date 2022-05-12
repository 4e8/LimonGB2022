using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SphereCollider))]
    public class AIvision : MonoBehaviour
    {

        [SerializeField] float rangeOfSight;
        [SerializeField] [Range (0f, 180f)] float angleXOfSight;
        //[SerializeField] float angleXOfSight;
        [SerializeField] [Range (0f,90f)]float angleYOfSight;
        [SerializeField] string targetTag = "Player";
        [SerializeField] float loseTargetTime;

        float a;
        float b;

        CCAI ccai;
        bool hasCCAI = false;
        bool hasTarget;
        
        Transform target;
        SphereCollider rangeTrigger;

        public Transform Target => target;
        private void Start()
        {
            rangeTrigger = GetComponent<SphereCollider>();
            rangeTrigger.radius = rangeOfSight;
            if (TryGetComponent<CCAI>(out CCAI ccai)) this.ccai = ccai;
        }
        
        private void OnTriggerStay(Collider other)
        {
            //debug\test lines
            //a = Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(other.transform.position - transform.position, transform.up));
            //b = Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(other.transform.position - transform.position,transform.right));
            if (other.CompareTag(targetTag)
                && Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(other.transform.position - transform.position, transform.up)) < angleXOfSight
                && Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(other.transform.position - transform.position, transform.right)) < angleYOfSight)
            {
                target = other.transform;
                loseTargetTime = Time.time + 2;
                hasTarget = true;

                ccai.SetWaypoint(target);
            }
            
        }
        private void FixedUpdate()
        {
            //while(true)
            if (hasTarget && Time.time > loseTargetTime)
            {
                hasTarget = false;
                target = null;
                ccai.ClearTarget();
            }
        }
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.tag.Equals(targetTag)) target = null;
        //}
    }
}