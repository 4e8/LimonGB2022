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

        [SerializeField] float maxRPM = 100;
        [SerializeField] float acceleration = 500f;
        [SerializeField] float breakingForce = 300f;

        [SerializeField] float angle;

        [SerializeField] bool patrol = false;
        [SerializeField] Transform[] waypoints;
        [SerializeField] List<Transform> waypointsList;
        Transform currentWaypoint;
        int m_CurrentWaypointIndex;
        [SerializeField] float rangeToNextWaypoint  = 2;


        [SerializeField] private Transform centerOfMass;
        private Rigidbody body;
        private float currentAcceleration = 0f;
        private float currentBreakingForce = 0f;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
            body.centerOfMass = centerOfMass.transform.localPosition;

            m_CurrentWaypointIndex = 0;
            currentWaypoint = waypointsList[m_CurrentWaypointIndex];
        }
        private void FixedUpdate()
        {
            if (!currentWaypoint)
            {
                currentBreakingForce = breakingForce;
            }
            else currentBreakingForce = 0f;

            var waypointVector = currentWaypoint.position - transform.position;
            angle = Vector3.SignedAngle(transform.forward, waypointVector, transform.up);

            if ((waypointVector).magnitude < rangeToNextWaypoint)
            {
                if (patrol)
                {
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypointsList.Count;
                    currentWaypoint = waypointsList[m_CurrentWaypointIndex];
                    
                }
                else
                {
                    waypointsList.Remove(currentWaypoint);
                    currentWaypoint = waypointsList[m_CurrentWaypointIndex];
                }
            }
            
            FRCollider.brakeTorque = currentBreakingForce;
            FLCollider.brakeTorque = currentBreakingForce;
            BRCollider.brakeTorque = currentBreakingForce;
            BLCollider.brakeTorque = currentBreakingForce;

            if (angle < -2 && angle >= -22) TurnRight(acceleration);
            if (angle > 2 && angle <= 22) TurnLeft(acceleration);
            if (angle < -22) RotateLeft();
            if (angle > 22) RotateRight();
            if (angle >= -2 && angle <= 2) GoForward(acceleration);

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
        void TurnLeft(float a)
        {
            if (BRCollider.rpm < maxRPM && BRCollider.rpm > -maxRPM)
            {
                FRCollider.motorTorque = a;
                BRCollider.motorTorque = a;
            }
            else if (a * BRCollider.rpm < 0)
            {
                FRCollider.brakeTorque = BRCollider.brakeTorque = breakingForce;
            }
            else
            {
                FRCollider.motorTorque = 0;
                BRCollider.motorTorque = 0f;
                //FRCollider.brakeTorque = BRCollider.brakeTorque = 0;
            }
        }
        void TurnRight(float a)
        {
            if (BLCollider.rpm < maxRPM && BLCollider.rpm > -maxRPM)
            {
                FLCollider.motorTorque = BLCollider.motorTorque = a;
            }
            else if (a * BLCollider.rpm < 0)
            {
                FLCollider.brakeTorque = BLCollider.brakeTorque = breakingForce;
            }
            else
            {
                FLCollider.motorTorque = 0f;
                BLCollider.motorTorque = 0f;
                //FLCollider.brakeTorque = BLCollider.brakeTorque = 0;
            }
        }
        void RotateLeft()
        {
            TurnLeft(acceleration);
            TurnRight(-acceleration);
        }
        void RotateRight()
        {
            TurnRight(acceleration);
            TurnLeft(-acceleration);
        }
        private void GoForward(float a)
        {
            FRCollider.motorTorque = a;
            BRCollider.motorTorque = a;
            FLCollider.motorTorque = a;
            BLCollider.motorTorque = a;
        }
        public void ClearTarget()
        {
            waypointsList.Remove(currentWaypoint);
            currentWaypoint = waypointsList[m_CurrentWaypointIndex];
        }
        public void SetWaypoint(Transform transform)
        {
            if (!waypointsList.Contains(transform))
            {
            waypointsList.Insert(0, transform);
            currentWaypoint = transform;
            }
        }
        public void SetWaypoint(int index)
        {
            m_CurrentWaypointIndex = index;
            currentWaypoint = waypointsList[index];
        }
    }
}
