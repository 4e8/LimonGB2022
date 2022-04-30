using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Fire))]
    public class PlayerTurretDirection : MonoBehaviour
    {

        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private Transform turretX;
        [SerializeField] private Transform turretY;
        [SerializeField] private float requiredAngleToFire = 10;
        [SerializeField] private GunType gunType;
        [SerializeField] private bool predictFire = false;

        
        bool readyToFire = false;
        public bool ReadyToFire => readyToFire;

        float angX;
        Quaternion qatX;

        float angY;
        Quaternion qatY;
        float angleCannon;

        Fire rangeAttack;
        public enum GunType
        {
            gun,
            cannon,
        }
        private void Start()
        {
            rangeAttack = GetComponent<Fire>();
            if (target == null) target = GameObject.FindGameObjectWithTag("AimTarget").transform;
        }
        void Update()
        {

            var direction = target.position - turretX.position;
            if (predictFire && TryGetComponent<Rigidbody>(out Rigidbody rigidbody)) direction += rigidbody.velocity;

            angX = Vector3.SignedAngle(turretX.forward, transform.forward, transform.up) - Vector3.SignedAngle(direction, transform.forward, transform.up);
            if (angX > 180 || angX < -180) angX = -angX / 5; //need cus gun will rotate around every time your target cross 180, "5" like smoothness koef
            qatX = Quaternion.AngleAxis(angX * rotationSpeed * Time.deltaTime, Vector3.up); //can delete "* rotationSpeed * Time.deltaTime" and "if" if you dont need smooth rotation
            turretX.rotation = turretX.rotation * qatX;

            direction = target.position - turretY.position;
            if (gunType == GunType.cannon)
            {
                angleCannon = (Mathf.Asin((direction.magnitude * 9.81f) / (Mathf.Pow(rangeAttack.Impulse, 2))) * Mathf.Rad2Deg) / 2;
                direction = Vector3.RotateTowards(direction, Vector3.up, angleCannon * Mathf.Deg2Rad, 0);
                angY = Vector3.Angle(Vector3.up, turretY.forward) - Vector3.Angle(Vector3.up, direction);
            }
            else angY = Vector3.Angle(Vector3.up, turretY.forward) - Vector3.Angle(Vector3.up, direction); // RIGHT FOR MINIGUN MODEL
            qatY = Quaternion.AngleAxis(-angY * rotationSpeed * Time.deltaTime, Vector3.right);
            turretY.rotation = turretY.rotation * qatY;

            if (Mathf.Abs(angX) < requiredAngleToFire && Mathf.Abs(angY) < requiredAngleToFire) readyToFire = true; else readyToFire = false;
        }
    }
}
