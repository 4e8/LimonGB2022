using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{    
    public class Fire : MonoBehaviour
    {
        [SerializeField] WeaponGunConfig _config;

        [SerializeField] Transform _spawnPoint;
        [SerializeField] Bullet _bulletPrefab;
        [SerializeField] float _impulse;
        [SerializeField] float _AttackDelay = 1;

        [SerializeField] Animator _animator;
        public float Impulse => _impulse;

        float _AttackTime;
        PlayerTurretDirection _turretDirection;
        private void Start()
        {
            if (TryGetComponent<PlayerTurretDirection>(out PlayerTurretDirection turretDirection)) this._turretDirection = turretDirection;
            _bulletPrefab = _config.bulletPrefab;
            _impulse = _config.impulse;
            _AttackDelay = _config.shootDelay;
        }
        private void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            if (bullet.TryGetComponent(out Rigidbody body))
            {
                body.velocity = Vector3.zero;
                body.AddForce(_impulse * _spawnPoint.transform.forward, ForceMode.Impulse);
            }
            if (_animator) _animator.Play("minigunFire");
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && _turretDirection.ReadyToFire && (Time.time > _AttackTime))
            {
                _AttackTime = Time.time + _AttackDelay;
                Shoot();
            }
        }
    }
}
