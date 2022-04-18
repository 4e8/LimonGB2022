using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(FindTarget))]
    [RequireComponent(typeof(RangeAttack))]
    public class TurretDirection : MonoBehaviour
    {

        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private Transform turretX;
        [SerializeField] private Transform turretY;
        private Transform turretYSP;
        [SerializeField] private float requiredAngleToFire = 10;
        [SerializeField] private GunType gunType;
        [SerializeField] private bool predictFire = true;

        float test;
        [HideInInspector] public bool readyToFire = false;

        float angX;
        Quaternion qatX;

        float angY;
        Quaternion qatY;
        float angleCannon;
        public enum GunType
        {
            gun,
            cannon,
        }
        private FindTarget findTarget;
        private RangeAttack rangeAttack;


        private void Start()
        {
            findTarget = GetComponent<FindTarget>();
            rangeAttack = GetComponent<RangeAttack>();
            turretYSP = rangeAttack.Spawnpoint;
        }
        void Update()
        {
            if (!findTarget.HasTarget) return;

            target = findTarget.Target;

            var direction = target.position - turretX.position;
            if (predictFire && TryGetComponent<Rigidbody> (out Rigidbody rigidbody)) direction = direction + rigidbody.velocity;

            angX = Vector3.SignedAngle(turretX.forward, transform.forward, transform.up) - Vector3.SignedAngle(direction, transform.forward, transform.up);
            if (angX > 180 || angX < -180) angX = -angX / 5; //need cus gun will rotate around every time your target cross 180, "5" like smoothness koef
            qatX = Quaternion.AngleAxis(angX * rotationSpeed * Time.deltaTime, Vector3.up); //can delete "* rotationSpeed * Time.deltaTime" and "if" if you dont need smooth rotation
            turretX.rotation = turretX.rotation * qatX;

            direction = target.position - turretYSP.position;
            test = direction.magnitude;
            if (gunType == GunType.cannon)
            {
                angleCannon = (Mathf.Asin((direction.magnitude * 9.81f) / (Mathf.Pow(rangeAttack.Impulse,2))) * Mathf.Rad2Deg) / 2;
                direction = Vector3.RotateTowards(direction, Vector3.up,angleCannon*Mathf.Deg2Rad,0);
                angY = Vector3.Angle(Vector3.up, turretY.forward) - Vector3.Angle(Vector3.up, direction);
            }
            else angY = Vector3.Angle(Vector3.up, turretY.forward) - Vector3.Angle(Vector3.up, direction);
            qatY = Quaternion.AngleAxis(-angY * rotationSpeed * Time.deltaTime, Vector3.right);
            turretY.rotation = turretY.rotation * qatY;

            if (angX < requiredAngleToFire && angY < requiredAngleToFire) readyToFire = true; else readyToFire = false;  
        }
    }
}
