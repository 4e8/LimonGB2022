using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public struct Gear
    {
        public Gear(float downShiftRpm, float upShiftRpm, float rate)
        {
            DownShiftRpm = downShiftRpm;
            UpShiftRpm = upShiftRpm;
            Rate = rate;
        }
        public float DownShiftRpm { get; }
        public float UpShiftRpm { get; set;}
        public float Rate { get; }
    }

    [RequireComponent(typeof(Fuel))]
    [RequireComponent(typeof(LightsController))]
    [RequireComponent(typeof (CarSoundController))]
    public class CC : MonoBehaviour
    {
        [SerializeField] GameObject fogTrail;
        [SerializeField] ParticleSystem fogParticle;

        public WheelCollider[] Wheels => wheelColliders;
        [SerializeField] WheelCollider[] wheelColliders;
        [SerializeField] Transform[] wheelMeshes;
        [SerializeField] ParticleSystem[] wheelParticles;
        int wheelCount;
        [SerializeField] int steeringWheelsCount = 2;
        int driveWheelsCount;

        [SerializeField] float maxSpeed = 20;

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

        Gear[] transmission = new Gear[]
        {
            new Gear(-50, 170,  2f),
            new Gear(150, 240, 1.5f),
            new Gear(220, 320, 1.1f),
            new Gear(300, 430, 0.7f),
            new Gear(400, 500, 0.3f) 
        };
        Gear currentGear;
        int currentGearIndex;
        int awd = 2;
        float avgRpm;


        [SerializeField] Transform centerOfMass;
        Rigidbody body;
        bool[] drift;
        float[] driftDelay;
        [SerializeField] Transform skidTrailPrefab;

        [SerializeField] LightsController lightsController;
        [SerializeField] CarSoundController carSoundController;
        
        
        float test;
        private void Start()
        {
            fuel = GetComponent<Fuel>();
            body = GetComponent<Rigidbody>();
            body.centerOfMass = centerOfMass.transform.localPosition;
            transmission[transmission.Length-1].UpShiftRpm = maxSpeed / ((wheelColliders[1].radius) * 2 * 0.1885f);
            wheelParticles = new ParticleSystem[wheelColliders.Length];
            wheelCount = wheelColliders.Length;
            for (int i = 0; i < wheelCount; i++)
            {
                wheelParticles[i] = wheelColliders[i].GetComponent<ParticleSystem>();
            }
            lightsController = GetComponent<LightsController>();
            carSoundController = GetComponent<CarSoundController>();
            driftDelay = new float[wheelCount];
            drift = new bool[wheelCount];
            awd = driveWheelsCount = wheelColliders.Length - steeringWheelsCount;
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) lightsController.SwitchLights();

            if (Input.GetKeyDown(KeyCode.Q)) SwitchAWD();

            Breaks();
            Steering();
            CalculateRpm();
            SwitchGear();
            Acceleration();
        }
        private void Steering()
        {
            SetTurnAngle = ((maxTurnAngle + (Vector3.Angle(gameObject.transform.forward, body.velocity))) * 200) / (Mathf.Abs(wheelColliders[2].rpm) + 200);
            currentTurnAngle = SetTurnAngle * Input.GetAxis("Horizontal");

            for (int i = wheelCount-steeringWheelsCount; i < wheelCount; i++)
                wheelColliders[i].steerAngle = currentTurnAngle;

            WheelHit wheelHit;

            for (int i = 0; i < wheelCount; i++)
            {
                UpdateWheel(wheelColliders[i], wheelMeshes[i]);
                wheelColliders[i].GetGroundHit(out wheelHit);
                drift[i] = (Mathf.Abs(wheelHit.sidewaysSlip) >= 0.3 || Mathf.Abs(wheelHit.forwardSlip) >= 0.3) ? true : false;//Vector3.Angle(wheelColliders[i].transform.forward, body.velocity) > SetTurnAngle
                if (drift[i])
                {
                    if (wheelColliders[i].transform.childCount <= 0)
                    {
                        Instantiate(fogTrail, wheelHit.point, Quaternion.LookRotation(-wheelHit.normal), wheelColliders[i].transform);//(fog, wheelHit.point, Quaternion.identity);//(fog,wheelColliders[i].transform.position,Quaternion.identity);
                        wheelParticles[i].Play();
                        driftDelay[i] = Time.time + 0.2f;
                    }
                    if (driftDelay[i] < Time.time) carSoundController.PlaySlip(i);
                }
                else if (wheelColliders[i].transform.childCount > 0 && (!drift[i] || !wheelColliders[i].isGrounded))
                {
                    wheelParticles[i].Stop();
                    wheelColliders[i].transform.DetachChildren();
                    carSoundController.StopSlip(i);
                }
            }

        }
        private void Breaks()
        {
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

        }
        private void Acceleration()
        {
            currentAcceleration = fuel.Empty ? 0 : acceleration * Input.GetAxis("Vertical");
            test = currentAcceleration / awd * currentGear.Rate;
            for (int i = 0; i < awd; i++)
            {
                //if (wheelColliders[i].isGrounded && wheelColliders[i].rpm < maxRPM && wheelColliders[i].rpm > -gearRate)
                if (wheelColliders[i].rpm < avgRpm+10 && avgRpm < currentGear.UpShiftRpm + 10 && avgRpm > transmission[0].DownShiftRpm)// && !drift[i])
                {
                    //wheelColliders[i].motorTorque = currentAcceleration / awd / (Mathf.Round(Mathf.Abs(wheelColliders[i].rpm) / gearRate) + 1);
                    wheelColliders[i].motorTorque = currentAcceleration / awd * currentGear.Rate;
                }
                else wheelColliders[i].motorTorque = 0f;
            }
            //carSoundController.EngineSoundSpeed(((avgRpm) / gearRate) + 1 - (Mathf.Round(wheelColliders[1].rpm / gearRate)));
            carSoundController.EngineSoundSpeed(((Mathf.Abs(avgRpm) - currentGear.DownShiftRpm) / (currentGear.UpShiftRpm - currentGear.DownShiftRpm)) + 0.5f);


            //if (wheelColliders[1].rpm < maxRPM && wheelColliders[1].rpm > -maxRPM/3)
            //    wheelColliders[1].motorTorque = currentAcceleration;
            //else wheelColliders[1].motorTorque = 0f;
            //if (wheelColliders[0].rpm < maxRPM && wheelColliders[0].rpm > -maxRPM/3)
            //    wheelColliders[0].motorTorque = currentAcceleration;
            //else wheelColliders[0].motorTorque = 0f;
        }
        private void UpdateWheel(WheelCollider col, Transform trans)
        {
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            trans.position = position;
            trans.rotation = rotation;
        }
        void SwitchAWD()
        {
            awd = awd == driveWheelsCount ? wheelCount : driveWheelsCount;
            foreach (WheelCollider col in wheelColliders) col.motorTorque = 0f;
        }
        private void SwitchGear()
        {
            if (avgRpm < currentGear.DownShiftRpm && currentGearIndex > 0)
            {
                currentGearIndex--;
                currentGear = transmission[currentGearIndex];
            }

            if (avgRpm > currentGear.UpShiftRpm && currentGearIndex < transmission.Length)
            {
                currentGearIndex++;
                currentGear = transmission[currentGearIndex];
            }
        }
        private void CalculateRpm()
        {
            avgRpm = 0f;
            for (int i = 0; i < awd; i++) avgRpm += wheelColliders[i].rpm;
            avgRpm /= awd;
        }
    }
}
