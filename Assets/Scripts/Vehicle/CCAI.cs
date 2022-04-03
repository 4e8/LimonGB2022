using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CCAI : MonoBehaviour
    {
        [SerializeField] WheelCollider FRCollider;
        [SerializeField] WheelCollider FLCollider;
        [SerializeField] WheelCollider BRCollider;
        [SerializeField] WheelCollider BLCollider;

        [SerializeField] Transform FRTransform;
        [SerializeField] Transform FLTransform;
        [SerializeField] Transform BRTransform;
        [SerializeField] Transform BLTransform;

        public Transform[] waypoints;
        public Transform currentWaypoint;
        int m_CurrentWaypointIndex;

        [SerializeField] float RPM;


        public float acceleration = 500f;
        public float breakingForce = 300f;


        private float currentAcceleration = 0f;
        private float currentBreakingForce = 0f;

        private void Start()
        {
            m_CurrentWaypointIndex = 0;
            currentWaypoint = waypoints[m_CurrentWaypointIndex];
        }
        private void FixedUpdate()
        {
            if ((transform.position - currentWaypoint.position).magnitude < 2)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                currentWaypoint = waypoints[m_CurrentWaypointIndex];
            }
            //var direction = waypointPatrol.currentWaypoint.position - transform.position;
            //currentAcceleration = acceleration;

            //turn right to target
            if (transform.forward.y < (transform.position - currentWaypoint.position).y)
            {
                //if (FLCollider.rpm < 50 && FLCollider.rpm > -50)
                //    FLCollider.motorTorque = acceleration;
                //else FLCollider.motorTorque = 0f;
                if (BLCollider.rpm < 50 && BLCollider.rpm > -50)
                    BLCollider.motorTorque = acceleration;
                else BLCollider.motorTorque = 0f;
                //FRCollider.brakeTorque = breakingForce;
                //BRCollider.brakeTorque = breakingForce;
            }
            //turn left to target
            if (transform.forward.y > (transform.position - currentWaypoint.position).y)
            {
                //if (FRCollider.rpm < 50 && FRCollider.rpm > -50)
                //    FRCollider.motorTorque = acceleration;
                //else FRCollider.motorTorque = 0f;
                if (BRCollider.rpm < 50 && BRCollider.rpm > -50)
                    BRCollider.motorTorque = acceleration;
                else BRCollider.motorTorque = 0f;
                //FLCollider.brakeTorque = breakingForce;
                //BLCollider.brakeTorque = breakingForce;
            }

            //if (Input.GetKey(KeyCode.Space) || (Input.GetAxis("Vertical") * BRCollider.rpm) < 0) currentBreakingForce = breakingForce;
            //else currentBreakingForce = 0f;


            RPM = BLCollider.rpm;

            UpdateWheel(FRCollider, FRTransform);
            UpdateWheel(FLCollider, FLTransform);
            UpdateWheel(BLCollider, BLTransform);
            UpdateWheel(BRCollider, BRTransform);

        }
        void UpdateWheel(WheelCollider col, Transform trans)
        {
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            trans.position = position;
            trans.rotation = rotation;
        }
    }
}
