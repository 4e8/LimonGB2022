using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Fire))]
    public class PlayerTurretDirection : MonoBehaviour
    {

        [SerializeField] Transform _target;
        [SerializeField] float _rotationSpeed = 2f;
        [SerializeField] Transform _turretX;
        [SerializeField] Transform _turretY;
        [SerializeField] float _requiredAngleToFire = 10;
        [SerializeField] GunType _gunType;
        [SerializeField] bool _predictFire = false;

        
        bool _readyToFire = false;
        public bool ReadyToFire => _readyToFire;

        float _angX;
        Quaternion _qatX;

        float _angY;
        Quaternion _qatY;
        float _angleCannon;

        Fire _rangeAttack;
        public enum GunType
        {
            gun,
            cannon,
        }
        private void Start()
        {
            _rangeAttack = GetComponent<Fire>();
            if (_target == null) _target = GameObject.FindGameObjectWithTag("AimTarget").transform;
        }
        private void Update()
        {

            var direction = _target.position - _turretX.position;
            if (_predictFire && TryGetComponent<Rigidbody>(out Rigidbody rigidbody)) direction += rigidbody.velocity;

            _angX = Vector3.SignedAngle(_turretX.forward, transform.forward, transform.up) - Vector3.SignedAngle(direction, transform.forward, transform.up);
            if (_angX > 180 || _angX < -180) _angX = -_angX / 5; //need cus gun will rotate around every time your target cross 180, "5" like smoothness koef
            _qatX = Quaternion.AngleAxis(_angX * _rotationSpeed * Time.deltaTime, Vector3.up); //can delete "* rotationSpeed * Time.deltaTime" and "if" if you dont need smooth rotation
            _turretX.rotation = _turretX.rotation * _qatX;

            direction = _target.position - _turretY.position;
            if (_gunType == GunType.cannon)
            {
                _angleCannon = (Mathf.Asin((direction.magnitude * 9.81f) / (Mathf.Pow(_rangeAttack.Impulse, 2))) * Mathf.Rad2Deg) / 2;
                direction = Vector3.RotateTowards(direction, Vector3.up, _angleCannon * Mathf.Deg2Rad, 0);
                _angY = Vector3.Angle(Vector3.up, _turretY.forward) - Vector3.Angle(Vector3.up, direction);
            }
            else _angY = Vector3.Angle(Vector3.up, _turretY.forward) - Vector3.Angle(Vector3.up, direction);
            _qatY = Quaternion.AngleAxis(-_angY * _rotationSpeed * Time.deltaTime, Vector3.right);
            _turretY.rotation = _turretY.rotation * _qatY;

            if (Mathf.Abs(_angX) < _requiredAngleToFire && Mathf.Abs(_angY) < _requiredAngleToFire) _readyToFire = true; else _readyToFire = false;
        }
    }
}
