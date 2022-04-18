using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CC : MonoBehaviour
    {
        [SerializeField] WheelCollider FRCollider;
        [SerializeField] WheelCollider FLCollider;
        [SerializeField] WheelCollider BRCollider;
        [SerializeField] WheelCollider BLCollider;

        [SerializeField] Transform FRTransform;
        [SerializeField] Transform FLTransform;
        [SerializeField] Transform BRTransform;
        [SerializeField] Transform BLTransform;

        [SerializeField] float MaxRPM = 200;


        public float acceleration = 500f;
        public float breakingForce = 300f;
        public float maxTurnAngle = 30f;

        private float currentAcceleration = 0f;
        private float currentBreakingForce = 0f;
        private float currentTurnAngle = 0f;
        private float setTurnAngle;

        [SerializeField]private Transform centerOfMass;
        private Rigidbody body;
        private void Start()
        {
            body = GetComponent<Rigidbody>();
            body.centerOfMass = centerOfMass.transform.localPosition;
        }
        private void Update()
        {
            currentAcceleration = acceleration * Input.GetAxis("Vertical");
            

            if (Input.GetKey(KeyCode.Space) || (Input.GetAxis("Vertical") * BRCollider.rpm) < 0) currentBreakingForce = breakingForce;
            else currentBreakingForce = 0f;
            if (BRCollider.rpm < MaxRPM && BRCollider.rpm > -MaxRPM/3)
                BRCollider.motorTorque = currentAcceleration;
            else BRCollider.motorTorque = 0f;
            if (BLCollider.rpm < MaxRPM && BLCollider.rpm > -MaxRPM/3)
                BLCollider.motorTorque = currentAcceleration;
            else BLCollider.motorTorque = 0f;
            
            FRCollider.brakeTorque = currentBreakingForce;
            FLCollider.brakeTorque = currentBreakingForce;
            BRCollider.brakeTorque = currentBreakingForce;
            BLCollider.brakeTorque = currentBreakingForce;

            setTurnAngle = (maxTurnAngle * 100) / (Mathf.Abs(FRCollider.rpm) + 100);
            currentTurnAngle = setTurnAngle * Input.GetAxis("Horizontal");
            FRCollider.steerAngle = currentTurnAngle;
            FLCollider.steerAngle = currentTurnAngle;

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
