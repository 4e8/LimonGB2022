using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Fuel))]
    [RequireComponent(typeof(LightsController))]
    [RequireComponent(typeof (CarSoundController))]
    public class CC : MonoBehaviour
    {
        [SerializeField] GameObject fogTrail;
        [SerializeField] ParticleSystem fogParticle;
        ParticleSystem part;

        public WheelCollider[] Wheels => wheelColliders;
        [SerializeField] WheelCollider[] wheelColliders;
        [SerializeField] Transform[] wheelMeshes;
        [SerializeField] ParticleSystem[] wheelParticles;
        

        [SerializeField] float maxSpeed = 20;
        float maxRPM;

        [SerializeField] Fuel fuel;

        [SerializeField] float acceleration = 1000f;
        [SerializeField] float breakingForce = 1500f;
        [SerializeField] float maxTurnAngle = 30f;

        float currentAcceleration = 0f;
        float currentBreakingForce = 0f;
        float currentTurnAngle = 0f;
        float setTurnAngle;
        float SetTurnAngle
        {
            get { return setTurnAngle; }
            set { if (value > maxTurnAngle) setTurnAngle = maxTurnAngle; else setTurnAngle = value; }
        }

        int awd = 2;
        float gearRate = 100;
        float avgRpm;

        float test;
        [SerializeField] Transform centerOfMass;
        Rigidbody body;
        bool drift;
        float[] driftDelay;

        [SerializeField] Transform skidTrailPrefab;
        Transform m_SkidTrail;
        bool skidding;

        [SerializeField] LightsController lightsController;
        [SerializeField] CarSoundController carSoundController;
        private void Start()
        {
            fuel = GetComponent<Fuel>();
            body = GetComponent<Rigidbody>();
            body.centerOfMass = centerOfMass.transform.localPosition;
            maxRPM = maxSpeed / ((wheelColliders[1].radius) * 2 * 0.1885f);
            wheelParticles = new ParticleSystem[wheelColliders.Length];
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelParticles[i] = wheelColliders[i].GetComponent<ParticleSystem>();
            }
            lightsController = GetComponent<LightsController>();
            carSoundController = GetComponent<CarSoundController>();
            driftDelay = new float[wheelColliders.Length];
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) lightsController.SwitchLights();

            if (Input.GetKeyDown(KeyCode.Q)) SwitchAWD();

            if (Input.GetKey(KeyCode.Space) || (Input.GetAxis("Vertical") * wheelColliders[1].rpm) < 0)
            {
                currentBreakingForce = breakingForce;
                lightsController.SwitchBreakLights(true);
            }
            else
            {
                currentBreakingForce = 0f;
                lightsController.SwitchBreakLights(false);
            }

            foreach (WheelCollider wheel in wheelColliders) wheel.brakeTorque = currentBreakingForce;


            
            SetTurnAngle = ((maxTurnAngle + Vector3.Angle(gameObject.transform.forward, body.velocity)) * 100) / (Mathf.Abs(wheelColliders[2].rpm) + 100);
            currentTurnAngle = SetTurnAngle * Input.GetAxis("Horizontal");
            
            
            wheelColliders[2].steerAngle = currentTurnAngle;
            wheelColliders[3].steerAngle = currentTurnAngle;

            WheelHit wheelHit;

            for (int i = 0; i < wheelColliders.Length; i++)
            {
                UpdateWheel(wheelColliders[i], wheelMeshes[i]);
                wheelColliders[i].GetGroundHit(out wheelHit);
                drift = (Mathf.Abs(wheelHit.sidewaysSlip) >= 0.3 || Mathf.Abs(wheelHit.forwardSlip) >= 0.3) ? true : false;//Vector3.Angle(wheelColliders[i].transform.forward, body.velocity) > SetTurnAngle
                if (drift)
                {
                    if (wheelColliders[i].transform.childCount <= 0)
                    {
                        Instantiate(fogTrail, wheelHit.point, Quaternion.LookRotation(-wheelHit.normal), wheelColliders[i].transform);//(fog, wheelHit.point, Quaternion.identity);//(fog,wheelColliders[i].transform.position,Quaternion.identity);
                        wheelParticles[i].Play();
                        driftDelay[i] = Time.time + 0.2f;
                    }
                    if (driftDelay[i] < Time.time) carSoundController.PlaySlip(i);
                }
                else if (wheelColliders[i].transform.childCount > 0 && (!drift || !wheelColliders[i].isGrounded))
                {
                    wheelParticles[i].Stop();
                    wheelColliders[i].transform.DetachChildren();
                    carSoundController.StopSlip(i);
                }
            }
            
            currentAcceleration = fuel.Empty ? 0 : acceleration * Input.GetAxis("Vertical");
            test = (Mathf.Round(Mathf.Abs(wheelColliders[1].rpm) / gearRate));
            avgRpm = 0f;
            for (int i = 0; i < awd; i++)
            {
                if (wheelColliders[i].isGrounded && wheelColliders[i].rpm < maxRPM && wheelColliders[i].rpm > -gearRate)
                {
                    wheelColliders[i].motorTorque = currentAcceleration / awd / (Mathf.Round(Mathf.Abs(wheelColliders[i].rpm) / gearRate) + 1);
                    avgRpm += wheelColliders[i].rpm;
                }
                else wheelColliders[i].motorTorque = 0f;
            }
            avgRpm /= awd;
            carSoundController.EngineSoundSpeed(((avgRpm) / gearRate) + 1 - (Mathf.Round(wheelColliders[1].rpm / gearRate)));


            //if (wheelColliders[1].rpm < maxRPM && wheelColliders[1].rpm > -maxRPM/3)
            //    wheelColliders[1].motorTorque = currentAcceleration;
            //else wheelColliders[1].motorTorque = 0f;
            //if (wheelColliders[0].rpm < maxRPM && wheelColliders[0].rpm > -maxRPM/3)
            //    wheelColliders[0].motorTorque = currentAcceleration;
            //else wheelColliders[0].motorTorque = 0f;
            
        }
        void UpdateWheel(WheelCollider col, Transform trans)
        {
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            trans.position = position;
            trans.rotation = rotation;
        }
        void SwitchAWD()
        {
            awd = awd == 2 ? wheelColliders.Length : 2;
            foreach (WheelCollider col in wheelColliders) col.motorTorque = 0f;
        }

    }
}
